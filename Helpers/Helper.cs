using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SSOService.Helpers
{
    public class Helper
    {
        public static XmlDocument ReadyXmlDocument => new XmlDocument { PreserveWhitespace = true };

        public static string Utf8Base64EncodeString(string encode) {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(encode));
        }

        public static string Utf8Base64DecodeString(string decode) {
            return Encoding.UTF8.GetString(Convert.FromBase64String(decode));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202", Justification = "StringWriter Dispose is called only once; eliminate nested using statements")]
        public static XmlDocument SerializeTypeToXmlDocument<T>(T samlType) where T : class {
            XmlDocument xmlDocument = ReadyXmlDocument;
            XmlWriterSettings xmlSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, Encoding = Encoding.UTF8 };

            //Pattern complies with Microsoft Usage Code Analysis CA2202; NO nested using statements
            StringWriter stringWriter = new StringWriter();
            try {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlSettings)) {
                    XmlSerializer xmlSerializer = new XmlSerializer(samlType.GetType());
                    xmlSerializer.Serialize(xmlWriter, samlType);
                    xmlDocument.LoadXml(stringWriter.ToString());
                };
            } finally {
                stringWriter?.Dispose();
            }
            return xmlDocument;
        }

        public static T DeserializeXmlDocumentToType<T>(XmlDocument document) where T : class {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(document.OuterXml);

            XmlReader xmlReader = XmlReader.Create(stringReader);
            T oData = (T)xmlSerializer.Deserialize(xmlReader);

            return oData;
        }

        public static string GuidAsIdString(string id = null) {
            return "_" + ((string.IsNullOrEmpty(id)) ? Guid.NewGuid().ToString().Replace("-", string.Empty) : id);
        }

        /// <REMARKS>  
        /// The PTS SSO Service method "XmlDocumentDeflateEncrypt" is dependent on the X509 Encrypting certificate; this certificate is not sharable
        /// </REMARKS>  
        public static string XmlDocumentDeflateEncrypt(XmlDocument xmlDocument) {
            StringBuilder builderEncrypted = new StringBuilder();

            //Deflate XML Document
            MemoryStream compressedStream = new MemoryStream();
            MemoryStream uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlDocument.OuterXml));
            using (DeflateStream compressorStream = new DeflateStream(compressedStream, CompressionMode.Compress, Names.LeaveCompressionStreamOpen)) {
                uncompressedStream.CopyTo(compressorStream);
            }
            string documentDeflated = Convert.ToBase64String(compressedStream.ToArray());

            //Encrypt deflated XML Document...
            RSACryptoServiceProvider rsaEncryptor = (RSACryptoServiceProvider)Saml.Helper.EncryptingCert.PrivateKey;
            IEnumerable<string> encryptSegments = documentDeflated.Select((c, index) => new { c, index }).GroupBy(x => x.index / Names.Key256SegmentBytes).Select(xg => string.Join(string.Empty, xg.Select(x => x.c)));
            foreach (string segment in encryptSegments)
            {
                string segmentEncrypted = Convert.ToBase64String(rsaEncryptor.Encrypt(Encoding.UTF8.GetBytes(segment), Names.WithOaepKeyPadding));
                builderEncrypted.Append(segmentEncrypted);
            }
            string documentDeflatedEncrypted = builderEncrypted.ToString();

            return documentDeflatedEncrypted;
        }

        /// <REMARKS>  
        /// The PTS SSO Service Method "XmlDocumentDecryptInflate" is dependent on the X509 Encrypting certificate; this certificate is not sharable
        /// </REMARKS>  
        public static XmlDocument XmlDocumentDecryptInflate(string documentDeflatedEncrypted) {
            StringBuilder builderDecrypted = new StringBuilder();

            //Decrypt deflated document...
            RSACryptoServiceProvider rsaDecryptor = (RSACryptoServiceProvider)Saml.Helper.EncryptingCert.PrivateKey;
            IEnumerable<string> decryptSegments = documentDeflatedEncrypted.Select((c, index) => new { c, index }).GroupBy(x => x.index / Names.Key256EncryptedBytes).Select(xg => string.Join(string.Empty, xg.Select(x => x.c)));
            foreach (string segment in decryptSegments)
            {
                string segmentDecrypted = Encoding.UTF8.GetString(rsaDecryptor.Decrypt(Convert.FromBase64String(segment), Names.WithOaepKeyPadding));
                builderDecrypted.Append(segmentDecrypted);
            }
            string documentDeflatedDecrypted = builderDecrypted.ToString();

            //Inflate document
            MemoryStream decompressedStream = new MemoryStream();
            MemoryStream compressedStream = new MemoryStream(Convert.FromBase64String(documentDeflatedDecrypted));
            using (DeflateStream decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress)) {
                decompressorStream.CopyTo(decompressedStream);
            }
            string documentInflated = Encoding.UTF8.GetString(decompressedStream.ToArray());

            //Load XML Document
            XmlDocument xmlDocument = ReadyXmlDocument;
            xmlDocument.LoadXml(documentInflated);

            return xmlDocument;
        }
    }
}
