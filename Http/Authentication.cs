using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Xml;
using SSOService.Helpers;
using SSOService.Models;
using SSOService.Saml;
using SSOService.Services;
using Claim = SSOService.Models.Claim;
using Names = SSOService.Saml.Names;

namespace SSOService.Http
{
    internal class Authentication : IHttpModule
    {
        public void Init(HttpApplication application) {
            application.AuthenticateRequest += (new EventHandler(this.Application_Authenticate));
        }

        /// <REMARKS>  
        /// Application_Authenticate supports: HTTP GET, HTTP POST, SAML Response
        /// All other HTTP Headers are ignored, execution passes back to IIS Pipeline
        /// 05/03 - Web.Config location filters are also supported
        /// </REMARKS>  
        private void Application_Authenticate(object sender, EventArgs e) {
            //The Default IDP Login is configured in the Host Web.Config
            string redirect = IdpDefaultLogin;

            //CANNOT DO ANYTHING when the request is the Service Provider host address
            if (IsHostRequest && IsHostServiceProvider) return;
            //IIS resloves host-address requests to the default document ELSE sends 401 or 404 error

            try {
                if (Log.Verbose) Log.WriteLine(Log.HttpLogHeader(MethodBase.GetCurrentMethod()));
                switch (this.HttpMethod) {
                    case Constants.HttpBindingGet: //Process Requestor HTTP GET
                        if (this.Authenticate)
                            redirect =
                                //1) If Url Path location filtered, pass HTTP GET Request to SP
                                (Helper.FilterHttpRequest()) ? string.Empty :
                                //2) If credential authenticated, pass HTTP GET Request to SP
                                (Helper.AuthenticateHttpRequest()) ? string.Empty :
                                //3) If not authenticated or filtered, HTTP Redirect to Default IDP: GET + RelayState
                                this.RelayStateRequest;
                        break;
                    case Constants.HttpBindingPost: //Process Requestor HTTP POST; SAML RSTR: Request Security Token Response from IDP
                        if (this.Authenticate)
                            redirect =
                                //1) If SAML Response authenticated, HTTP Redirect to RelayState
                                (Helper.AuthenticateSamlResponse()) ? RelayStateResponse :
                                //2) If Url Path location filtered, pass HTTP POST to SP
                                (Helper.FilterHttpRequest()) ? string.Empty :
                                //3) If HTTP Request authenticated, pass HTTP POST Request to SP
                                (Helper.AuthenticateHttpRequest()) ? string.Empty :
                                //4) If not authenticated or filtered, HTTP Redirect to Default IDP: GET + RelayState
                                this.RelayStateRequest;
                        break;
                    default:
                        //Ignore ALL other HTTP Headers, not authenticated, HTTP Redirect to Default IDP: GET + RelayState
                        redirect = IdpDefaultLogin;
                        break;
                }
            } catch(Exception ex) {
                //If ANY errors in authentication, log a message and redirect to the default IDP Login; setting a user error is not relavant
                Log.WriteLine($"{ex.GetType().FullName}, {Log.HttpLogHeader(MethodBase.GetCurrentMethod())} - {ex.Message}");
            }

            if (string.IsNullOrEmpty(redirect)) return;
            if (Log.Verbose) Log.WriteLine($"{Log.HttpLogHeader(MethodBase.GetCurrentMethod())}, {redirect}");
            HttpContext.Current.Response.Redirect(redirect);
        }

        #region HTTP Module Authentication Properties...
        private string RelayStateRequest {
            get {
                //The Default IDP is configured in the host Web.Config
                string redirect = IdpDefaultLogin;
                string target = $"?{Helpers.Names.RelayStateTarget}={HttpContext.Current.Request.Url.AbsoluteUri}";
                string referrer = (HttpContext.Current.Request.UrlReferrer == null) ? string.Empty : $"&{Helpers.Names.RelayStateReferrer}={HttpContext.Current.Request.UrlReferrer.AbsoluteUri}";

                return $"{redirect}?{Helpers.Names.RelayState}={Convert.ToBase64String(Encoding.UTF8.GetBytes($"{redirect}{target}{referrer}"))}";
            }
        }
        private static string RelayStateResponse {
            get {
                string relayTarget = string.Empty;
                try {
                    string parameterSamlRelayState = HttpContext.Current.Request.Form[Helpers.Names.RelayState];
                    string relayStateDecoded = Helpers.Helper.Utf8Base64DecodeString(parameterSamlRelayState);
                    NameValueCollection relayCollection = new Uri(relayStateDecoded).ParseQueryString();

                    //If the SAML Response is valid and the request is authenticated, then redirect to the original request target in the relay state
                    relayTarget = (HttpContext.Current.Request.IsAuthenticated) ? relayCollection[Helpers.Names.RelayStateTarget] : string.Empty;
                } catch { relayTarget = string.Empty; }

                return relayTarget;
            }
        }
        private string UrlRequest => HttpContext.Current.Request.Url.ToString();

        private string HttpMethod => HttpContext.Current.Request.HttpMethod;

        private string SamlRequest => HttpContext.Current.Request.Params[Helpers.Names.HttpParameterRequestSaml];

        private string SamlResponse => HttpContext.Current.Request.Params[Helpers.Names.HttpParameterResponseSaml];

        private bool Authenticate => !(HttpContext.Current.Request.IsAuthenticated);

        private bool IsHostRequest => HttpContext.Current.Request.Path.Equals(Helpers.Names.PathRoot);

        private bool IsHostServiceProvider {
            get {
                string authorityIdp = new Uri(IdpDefaultLogin).Authority;
                string authorityRequest = HttpContext.Current.Request.Url.Authority;
                string authorityReferrer = HttpContext.Current.Request.UrlReferrer?.Authority;

                //The Default IDP is configured in the Host Web.Config; is host service provider implies Host != IDP
                //If the IDP Authority = Request Authority = Referrer Authority then IDP Host else SP Host
                return !((authorityIdp == authorityRequest) || (authorityIdp == authorityReferrer));
            }
        }
        public string IdpDefaultLogin => Config.GetAppSettingsValue("defaultIDPLogin");

        #endregion

        public void Dispose() {
            //Part of System.Web.IHttpModule - Nothing to do...
        }
    }

    internal class Helper
    {
        /// <REMARKS>  
        /// 1) PTS SSO Root Web.Config supports location element filters: Example <location path="~/Login" />
        /// 2) The SSO HTTP Authentication Module-event intercept each authentication request
        /// 3) Request filters are applied when the request absolute path is found; pass HTTP POST Request to SP
        /// </REMARKS>  
        internal static bool FilterHttpRequest() {
            string configRootPath = "~";
            bool filterThisLocation = false;

            try {
                Configuration webHostConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(configRootPath);
                List<ConfigurationLocation> locationList = webHostConfiguration.Locations.Cast<ConfigurationLocation>().ToList();
                filterThisLocation = (locationList.Find(l => l.Path.ToLower().Contains(HttpContext.Current.Request.Url.AbsolutePath.ToLower())) != null);
            }
            catch { filterThisLocation = false; }

            return filterThisLocation;
        }

        /// <REMARKS>  
        /// 1) PTS SSO Cookie values must be a serializable XML Document
        /// 2) The SSO HTTP Authentication Module-event intercept each authentication request
        /// 3) CredentialAuthenticate extracts the SSO Cookie (decrypt, inflate, deserialize) and the Claims Identity
        /// </REMARKS>  
        internal static bool AuthenticateHttpRequest() {
            Credential credential = new Credential();
            string cookieDeflatedEncrypted = HttpContext.Current.Request.Cookies[Helpers.Names.HttpCookiePtsSso]?.Value;

            if (!string.IsNullOrEmpty(cookieDeflatedEncrypted)) {
                //Validate session cookie
                XmlDocument xmlCredential = Helpers.Helper.XmlDocumentDecryptInflate(cookieDeflatedEncrypted);
                credential = Helpers.Helper.DeserializeXmlDocumentToType<Credential>(xmlCredential);
            }
            if (credential.Authenticated) {
                //Build Credential from valid session cookie
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    new GenericIdentity(credential.Email, Helpers.Names.PrincipalType),
                    credential.Claims.Select(c => new System.Security.Claims.Claim($"{c.Name}:{c.FriendlyName}", c.Value, c.NameFormat, c.EntityId))
                    //Inserting a Generic Identity into the Claims Identity makes the User context both claim and role aware
                );
                //Create Authenticated principal
                HttpContext.Current.User = new GenericPrincipal(claimsIdentity, credential.Roles.Select(r => r.Name).ToArray());
            }

            return HttpContext.Current.Request.IsAuthenticated;
        }

        /// <REMARKS>  
        /// AuthenticateSAMLResponse supports SP-Initiated and IDP-Initated, POST
        /// Does not pass specific SAML status values back to the caller; caller sends SAML Failed or Success status
        /// </REMARKS>  
        internal static bool AuthenticateSamlResponse() {
            //Authenticated SAML Response will have the SAML token, transform the token into PTS Credential, create the principal, authenticated=true, create the SP cookie
            string parameterSamlResponse = HttpContext.Current.Request.Form[Helpers.Names.HttpParameterResponseSaml];

            if (string.IsNullOrEmpty(parameterSamlResponse)) return HttpContext.Current.Request.IsAuthenticated;

            //Validate SAML Response
            string decodedSamlResponse = Helpers.Helper.Utf8Base64DecodeString(parameterSamlResponse);
            XmlDocument xmlResponse = Helpers.Helper.ReadyXmlDocument;
            xmlResponse.LoadXml(decodedSamlResponse);

            ResponseType response = Helpers.Helper.DeserializeXmlDocumentToType<ResponseType>(xmlResponse);
            XmlElement xmlSignature = Saml.Helper.SelectXMLElement(xmlResponse, Names.SAMLNamesElementAssertionSignature, SignedXml.XmlDsigNamespaceUrl);

            //SAML Response objects are Federated;  if the response is verified then the subject is trusted (authenticated)
            //However, from Federated IDPs the assertions must match the PTS Endpoint Claim set registered in SQL
            if (xmlSignature == null || !Saml.Helper.VerifySignedXMLElement(xmlResponse.DocumentElement, xmlSignature))
                return HttpContext.Current.Request.IsAuthenticated;

            //From SAML Response assertions and RelayState, validate the user and build the PTS authenticated credential
            AssertionType assertion = response.Items.Where(t => t.GetType().FullName == typeof(AssertionType).FullName)?.First() as AssertionType;
            NameIDType nameId = assertion.Subject.Items.Where(t => t.GetType().FullName == typeof(NameIDType).FullName)?.First() as NameIDType;
            AttributeStatementType statement = assertion?.Items.Where(t => t.GetType().FullName == typeof(AttributeStatementType).FullName)?.First() as AttributeStatementType;
            List<AttributeType> attributeList = statement?.Items.Select(a => (AttributeType)a)?.ToList();
            string subjectNameId = nameId?.Value;

            //Response message not expired; eliminates replay attack
            if (DateTime.Now >= assertion.Conditions.NotOnOrAfter) return HttpContext.Current.Request.IsAuthenticated;
            Credential credential = new Credential { EntityId = subjectNameId, ClaimsId= assertion.Issuer.Value, Refresh = true };
            
            //Claim set valid; eliminate man-in-the-middle attack
            if (!AuthenticateSamlAssertion(attributeList, ref credential))
                return HttpContext.Current.Request.IsAuthenticated;

            XmlDocument xmlCredential = Helpers.Helper.SerializeTypeToXmlDocument(credential);
            
            //Deflate (W3C) and Encrypt (RSA) the PTS SSO XML Document credential...
            string credentialDeflatedEncrypted = Helpers.Helper.XmlDocumentDeflateEncrypt(xmlCredential);

            //Create Authenticated Principal from PTS Credential 
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                new GenericIdentity(credential.Email, Helpers.Names.PrincipalType),
                credential.Claims.Select(c => new System.Security.Claims.Claim($"{c.Name}:{c.FriendlyName}", c.Value, c.NameFormat, c.EntityId))
                //Inserting a Generic Identity into the Claims Identity makes the User context both claim and role aware...
            );
            HttpContext.Current.User = new GenericPrincipal(claimsIdentity, credential.Roles.Select(r => r.Name).ToArray());

            //Set PTS Authentication Session cookie; note the IIS Pipeline cache is not available in the application authenticaiton event
            //The only other cache choice for user credentials is another call to SQL Server with each IIS Pipeline request
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(Helpers.Names.HttpCookiePtsSso) {
                //IDP root session cookie, not persistant, not client script accessible, value is segmented, encrypted and defalted (W3C/RSA compliant)
                Expires = DateTime.MinValue, HttpOnly = true, Path = Helpers.Names.PathRoot, Value = credentialDeflatedEncrypted
            });

            return HttpContext.Current.Request.IsAuthenticated;
        }

        /// <REMARKS>  
        /// AuthenticateSAMLAssertion supports SP-Initiated and IDP-Initated, POST
        /// </REMARKS>  
        private static bool AuthenticateSamlAssertion(List<AttributeType> attributeList, ref Credential credential) {
            
            //Get authenticated credential from repository
            PtsAuthenticate service = new PtsAuthenticate();
            Session<Credential> session = new Session<Credential> { SqlKey = Config.ServiceRequestToken, Data = credential };
            session = service.AuthenticateCredential(session);

            Claim uniqueIdClaim = session.Data?.Claims?.Where(claim => claim.Unique)?.First();
            AttributeType uniqueIdAttribute = attributeList?.Where(attribute => attribute.Name == uniqueIdClaim?.Name)?.First();

            //If the unique credential claim matches the SAML Assertion, then synchronize the claim set attribute values
            //If the SAML attributes do not match the claim set, those items are removed from the result
            //IMPORTANT - registered claim sets MUST match SAML Assertion attributes
            if (uniqueIdClaim?.Value != uniqueIdAttribute?.AttributeValue?.First().ToString()) return false;
            if (session.Data == null) return false;
            List<Claim> claimList = (session.Data.Claims ?? throw new InvalidOperationException()).OrderBy(c => c.Name).Zip((attributeList ?? throw new ArgumentNullException(nameof(attributeList))).OrderBy(a => a.Name), (c, a) => 
                    (c.Name == a.Name) ? 
                        new Claim
                        {
                            Name = c.Name, FriendlyName = c.FriendlyName, NameFormat = c.NameFormat, Unique = c.Unique,
                            EntityId = c.EntityId, ClaimSet = c.ClaimSet, Required = c.Required, Value = a.AttributeValue.First().ToString() } : //if match use the attribute value
                        new Claim
                        {
                            Name = c.Name, FriendlyName = c.FriendlyName, NameFormat = c.NameFormat, Unique = c.Unique,
                            EntityId = c.EntityId, ClaimSet = c.ClaimSet, Required = c.Required, Value = c.Value } //if no match use the claim value
            ).ToList();

            //If the claim and attribute names/values match then assertion claim set is valid
            if (!session.Data.Claims.OrderBy(c => c.Name).Select(c => c.Value)
                    .SequenceEqual(claimList.OrderBy(c => c.Name).Select(c => c.Value)) ||
                !session.Data.Claims.OrderBy(c => c.Name).Select(c => c.Name)
                    .SequenceEqual(claimList.OrderBy(c => c.Name).Select(c => c.Name))) return false;

            credential = session.Data;
            return true;
        }
    }
}
