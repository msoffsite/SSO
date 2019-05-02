using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSOService.Helpers
{
    /// <REMARKS>  
    /// 1) The XML Credential is Serailized, Defalated, and Encrypted in 100 byte segments to comply with RSA using X509 (1024 bit) Private keys
    /// 2) The XML Credential is Serailized, Defalated, and Encrypted in 200 byte segments to comply with RSA using X509 (2048 bit) Private keys
    /// 3) RSA Asynchronous encryption, using X509 (2048 bit) Keys, 200 byte Segments less than Key size (256 bytes (2048/8))
    /// 4) RSA Asynchronous encryption, using X509 (2048) Keys, Segment Length = 200, Encrypted bytes = 344
    /// 5) Direct RSA Encryption using OAEP Padding, valid since Windows XP
    /// </REMARKS>  
    public static class Names
    {
        internal static bool LeaveCompressionStreamOpen { get; } = true;
        internal static bool WithOaepKeyPadding { get; } = true; //Direct RSA Encryption using OAEP Padding, valid since Windows XP
        internal static int Key256SegmentBytes { get; } = 200;
        internal static short Key256EncryptedBytes { get; } = 344;


        public static string PathRoot { get; } = "/";
        public static string PrincipalId { get; } = "Unique ID";
        public static string PrincipalType { get; } = "PTSSSO";
        public static string RelayState { get; } = "RelayState";
        public static string RelayStateTarget { get; } = "Target";
        public static string RelayStateReferrer { get; } = "Referrer";
        internal static string HttpBindingGet { get; } = "GET";
        internal static string HttpBindingPost { get; } = "POST";
        public static string HttpCookiePtsSso { get; } = "_PTS_SSOS_";
        public static string HttpCookieIdpSso { get; } = "_IDP_SSOS_";
        public static string HttpParameterStatusSaml { get; } = "SAMLStatus";
        public static string HttpParameterRequestSaml { get; } = "SAMLRequest";
        public static string HttpParameterResponseSaml { get; } = "SAMLResponse";

        public static string SAMLNamesStatusFailed = "urn:oasis:names:tc:SAML:2.0:status:AuthnFailed";
        public static string SAMLNamesStatusSuccess = "urn:oasis:names:tc:SAML:2.0:status:Success";

    }
}
