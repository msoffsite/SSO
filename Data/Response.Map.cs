using System;
using System.Data;
using System.Linq;
using System.Xml;
using SSOService.Models;
using SSOService.Saml;
using Helper = SSOService.Helpers.Helper;

namespace SSOService.Data
{
    public class ResponseMap
    {
        public void EndpointMapParameters(Endpoint endpoint, ref SqlService service) {
            //Map procedure...
            service.SqlProcedure = Names.SqlCommandAuthentication;

            //Map Endpoint to SQL DTO Table type...
            DataTable dtoDataTable = EndpointSqlDto.DataTable(endpoint);

            //Map parameters...
            SqlServiceParameter[] parameters = {
                new SqlServiceParameter(Names.ParameterDtoEndpoint, SqlDbType.Structured, Names.SqlParameterDtoTypeEndpoint, ParameterDirection.Input, dtoDataTable),
                new SqlServiceParameter(Names.SqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, Names.SqlMessageResponseEndpoint)
            }; service.SqlParameters.List = parameters;
        }

        public Endpoint EndpointMapData(DataSet dataSet) {
            DataTable dataTableClaims = dataSet.Tables[Names.DataSetTableEndpointClaims];
            DataTable dataTableEndpoint = dataSet.Tables[Names.DataSetTableEndpoint];

            //Map claims...
            var queryClaims = dataTableClaims.AsEnumerable()
                .Select(claim => new {
                    Id = claim.Field<string>(Names.MapClaimId),
                    Name = claim.Field<string>(Names.MapClaimName),
                    FriendlyName = claim.Field<string>(Names.MapClaimFriendlyName),
                    //FormatId = claim.Field<Int32>(Names.mapClaimFormatId),
                    FormatFriendlyName = claim.Field<string>(Names.MapClaimFormatFriendlyName),
                    FormatName = claim.Field<string>(Names.MapClaimFormatName),
                    Created = DateTime.Parse(claim.Field<string>(Names.MapClaimCreated)),
                    Edited = DateTime.Parse(claim.Field<string>(Names.MapClaimEdited)),
                    Unique = bool.Parse(claim.Field<string>(Names.MapClaimUnique)),
                    Required = bool.Parse(claim.Field<string>(Names.MapClaimRequired)),
                    Singular = bool.Parse(claim.Field<string>(Names.MapClaimSingular)),
                    Active = bool.Parse(claim.Field<string>(Names.MapClaimActive))
                }).Distinct();

            //Map Endpoint...
            Endpoint endpoint = new Endpoint
            {
                Authenticated = true,
                Id = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointId),
                ApplicationId = dataTableEndpoint.Rows[Names.DataSingleRow].Field<Guid>(Names.MapApplicationId).ToString(),
                Provider = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointProvider),
                Referrer = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointReferrer),
                Requestor = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointRequestor),
                Responder = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointResponder),
                Login = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointLogin),
                Logout = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointLogout),
                Organization = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointOrganization),
                Contact = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointContact),
                Description = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointDescription),
                Created = DateTime.Parse(dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointCreated)),
                Edited = DateTime.Parse(dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointEdited)),
                Active = bool.Parse(dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointActive)),
                Claims = queryClaims.Select(c => new EndpointClaim
                {
                    Id = c.Id, Name = c.Name, FriendlyName = c.FriendlyName, Format = c.FormatName, Required = c.Required, Singular = c.Singular, Unique = c.Unique }).ToList()
            };

            return endpoint;
        }

        public AttributeType[] EndpointMapClaims(DataSet dataSet) {
            DataTable dataTableClaims = dataSet.Tables[Names.DataSetTableCredentialClaims];

            //Map claims...
            var queryClaims = dataTableClaims.AsEnumerable()
                .Select(claim => new {
                    CredentialClaim = claim.Field<string>(Names.MapCredentialClaim),
                    ClaimName = claim.Field<string>(Names.MapCredentialClaimName),
                    ClaimFriendlyName = claim.Field<string>(Names.MapCredentialClaimFriendlyName),
                    ClaimFormat = claim.Field<string>(Names.MapClaimFormatName),
                    ClaimRequired = bool.Parse(claim.Field<string>(Names.MapClaimRequired))
                }).Distinct();

            AttributeType[] attributeList = queryClaims.Select(c => new AttributeType
            {
                NameFormat = c.ClaimFormat, Name = c.ClaimName, FriendlyName = c.ClaimFriendlyName, AttributeValue = new object[] { c.CredentialClaim }
            }).ToArray();

            return attributeList;
        }

        public XmlDocument EndpointMapSamlResponse(Endpoint endpoint, AttributeType[] claims ) {

            string subjectId = claims.FirstOrDefault(c => c.FriendlyName == Names.MapCredentialUniqueId)?.AttributeValue[Names.DataSingleValue]?.ToString() ?? string.Empty;

            AssertionType assertion = new AssertionType
            {
                ID = Helper.GuidAsIdString(endpoint.Id),
                Version = Saml.Names.SAMLVersion,
                IssueInstant = DateTime.UtcNow,
                Issuer = new NameIDType { Value = endpoint.Id, Format = Saml.Names.SAMLNamesFormatIssuerEntity },
                Conditions = new ConditionsType
                {
                    NotBefore = DateTime.UtcNow, NotBeforeSpecified = true,
                    NotOnOrAfter = DateTime.UtcNow.AddMinutes(Saml.Names.SAMLAssertionExpirationMinutes), NotOnOrAfterSpecified = true,
                    Items = new ConditionAbstractType[] { new AudienceRestrictionType { Audience = new[] { endpoint.Requestor } }, new OneTimeUseType {  } }
                },
                Subject = new SubjectType
                {
                    Items = new object[] {
                        new NameIDType { Value = subjectId, Format = Saml.Names.SAMLNamesFormatIssuerEntity },
                        new SubjectConfirmationType
                        {
                            Method = Saml.Names.SAMLNamesSubjectConfirmationBaerer,
                            SubjectConfirmationData = new SubjectConfirmationDataType
                            {
                                NotOnOrAfter = DateTime.UtcNow.AddMinutes(Saml.Names.SAMLAssertionExpirationMinutes),
                                Recipient = endpoint.Requestor
                            }
                        }
                    }
                },
                Items = new StatementAbstractType[] {
                    new AttributeStatementType { Items = claims },
                    new AuthnStatementType
                    {
                        AuthnInstant = DateTime.UtcNow,
                        SessionIndex = endpoint.Id,
                        AuthnContext = new AuthnContextType
                        {
                            Items = new object[] { Saml.Names.SAMLNamesContextClassPassword },
                            ItemsElementName = new[] { ItemsChoiceAuthnContext.AuthnContextClassRef }
                        }
                    }
                }
            };
            ResponseType response = new ResponseType
            {
                ID = Helper.GuidAsIdString(),
                Version = Saml.Names.SAMLVersion,
                IssueInstant = DateTime.UtcNow,
                Destination = endpoint.Requestor,
                Issuer = new NameIDType { Value = endpoint.Responder, Format = Saml.Names.SAMLNamesFormatIssuerEntity },
                Status = new StatusType { StatusCode = new StatusCodeType { Value = Saml.Names.SAMLNamesStatusSuccess } },
                Items = new[] { assertion }
            };

            XmlDocument xmlResponse = Saml.Helper.SerializeAndSignSAMLType<ResponseType>(response, response.ID);

            return xmlResponse;
        }

        public XmlDocument EndpointMapSamlResponseError(string statusMessage) {

            ResponseType response = new ResponseType
            {
                ID = Helper.GuidAsIdString(),
                Version = Saml.Names.SAMLVersion,
                IssueInstant = DateTime.UtcNow,
                Issuer = new NameIDType { Value = Saml.Names.SAMLMessageDefaultIssuer, Format = Saml.Names.SAMLNamesFormatBasic },
                Status = new StatusType { StatusCode = new StatusCodeType { Value = Saml.Names.SAMLNamesStatusFailed }, StatusMessage = statusMessage }
            };

            XmlDocument xmlResponse = Saml.Helper.SerializeAndSignSAMLType<ResponseType>(response, response.ID);

            return xmlResponse;
        }

        #region Enumeration map...

        //Database names...
        internal static class Names
        {
            //SQL commands...
            internal static string SqlCommandAuthentication { get; } = "dbo.ssoAuthenticate";
            internal static string SqlParameterDtoTypeEndpoint { get; } = "dbo.dtoTypeEndpoint";

            //SQL Command Messages...
            internal static string SqlMessageResponseEndpoint { get; } = "ResponseEndpoint";

            //SQL Command Parameters...
            internal static string SqlMessage { get; } = "message";
            internal static string ParameterDtoEndpoint { get; } = "endpoint";

            //SQL Data Map Parameters...
            internal static string MapEndpointId { get; } = "endpointId";
            internal static string MapApplicationId { get; } = "applicationId";
            internal static string MapEndpointProvider { get; } = "endpointProvider";
            internal static string MapEndpointReferrer { get; } = "endpointReferrer";
            internal static string MapEndpointRequestor { get; } = "endpointRequestor";
            internal static string MapEndpointResponder { get; } = "endpointResponder";
            internal static string MapEndpointLogin { get; } = "endpointLogin";
            internal static string MapEndpointLogout { get; } = "endpointLogout";
            internal static string MapEndpointOrganization { get; } = "endpointOrganization";
            internal static string MapEndpointContact { get; } = "endpointContact";
            internal static string MapEndpointDescription { get; } = "endpointDescription";
            internal static string MapEndpointCreated { get; } = "endpointCreated";
            internal static string MapEndpointEdited { get; } = "endpointEdited";
            internal static string MapEndpointActive { get; } = "endpointActive";

            internal static string MapClaimId { get; } = "endpointClaim";
            internal static string MapClaimName { get; } = "claimName";
            internal static string MapClaimFriendlyName { get; } = "claimFriendlyName";
            internal static string MapClaimFormatFriendlyName { get; } = "claimFormatFriendlyName";
            internal static string MapClaimFormatName { get; } = "claimFormatName";
            internal static string MapClaimCreated { get; } = "claimCreated";
            internal static string MapClaimEdited { get; } = "claimEdited";
            internal static string MapClaimUnique { get; } = "claimUnique";
            internal static string MapClaimRequired { get; } = "claimRequired";
            internal static string MapClaimSingular { get; } = "claimSingular";
            internal static string MapClaimActive { get; } = "claimActive";


            internal static string MapCredentialClaim { get; } = "credentialClaim";
            internal static string MapCredentialClaimName { get; } = "claimName";
            internal static string MapCredentialClaimFriendlyName { get; } = "claimFriendlyName";
            internal static string MapCredentialUniqueId { get; } = "Unique ID";

            //ADO Data set tables
            internal static short DataSetTableEndpoint { get; } = 0;
            internal static short DataSetTableEndpointClaims { get; } = 1;
            internal static short DataSetTableCredentialClaims { get; } = 2;
            internal static short DataSingleRow { get; } = 0;
            internal static short DataSingleValue { get; } = 0;
        }

        #endregion
    }
}
