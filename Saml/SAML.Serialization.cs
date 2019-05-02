//-----------------------------------------------------------------------------------------------------------------------------
// <code-history>
//      Created on: 02/09/2018
//      Created by: timc@pts1.com
//      This code was migrated from Template XSDSAMLSerializationTemplate.V4.6.cs
//      All contained classes are SAML 2.0 DTOs migrated and modified from the original XSD Template
//      These classes are used to Serialize and De-Serialize SAML 2.0 schemas to SSO Data Transfer Objects
//      The XSDSAMLSerializationTemplate.V4.6 was generated from the original W3C OASIS SAML 2.0 Schema library
//      For more information see: 
//          http://saml.xml.org/saml-specifications
//          https://www.oasis-open.org/standards#samlv2.0
//          http://www.w3.org/2002/ws/databinding/edcopy/collection/detected/OASIS-SAML-Protocol-2_0.html
//
//      Last update: 02/09/2019 - Original migration from V4.6.1055.0 class templates
// </code-history>
//-----------------------------------------------------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace SSOService.Saml {
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("CipherData", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class CipherDataType
    {

        private object itemField;

        /// <remarks/>
        [XmlElement("CipherReference", typeof(CipherReferenceType))]
        [XmlElement("CipherValue", typeof(byte[]), DataType = "base64Binary")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }

    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("CipherReference", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class CipherReferenceType
    {

        private TransformsType itemField;

        private string uRIField;

        /// <remarks/>
        [XmlElement("Transforms")]
        public TransformsType Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(DataType = "anyURI")]
        public string URI {
            get {
                return this.uRIField;
            }
            set {
                this.uRIField = value;
            }
        }
    }

    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("SPKIData", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SPKIDataType {
        
        private byte[][] sPKISexpField;
        
        private System.Xml.XmlElement anyField;
        
        
        [XmlElement("SPKISexp", DataType="base64Binary")]
        public byte[][] SPKISexp {
            get {
                return this.sPKISexpField;
            }
            set {
                this.sPKISexpField = value;
            }
        }
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }

    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("PGPData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class PGPDataType
    {

        private object[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;


        [XmlAnyElement()]
        [XmlElement("PGPKeyID", typeof(byte[]), DataType = "base64Binary")]
        [XmlElement("PGPKeyPacket", typeof(byte[]), DataType = "base64Binary")]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }


        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceType1[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
    }

    [System.Serializable()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType1 {
        
        
        [XmlEnum("##any:")]
        Item,
        
        
        PGPKeyID,
        
        
        PGPKeyPacket,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509IssuerSerialType {
        
        private string x509IssuerNameField;
        
        private string x509SerialNumberField;
        
        
        public string X509IssuerName {
            get {
                return this.x509IssuerNameField;
            }
            set {
                this.x509IssuerNameField = value;
            }
        }
        
        
        [XmlElement(DataType="integer")]
        public string X509SerialNumber {
            get {
                return this.x509SerialNumberField;
            }
            set {
                this.x509SerialNumberField = value;
            }
        }
    }
            
    [System.Serializable()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType {
        
        
        [XmlEnum("##any:")]
        Item,
        
        
        X509CRL,
        
        
        X509Certificate,
        
        
        X509IssuerSerial,
        
        
        X509SKI,
        
        
        X509SubjectName,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("RetrievalMethod", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class RetrievalMethodType {
        
        private TransformType[] transformsField;
        
        private string uRIField;
        
        private string typeField;
        
        
        [XmlArrayItem("Transform", IsNullable=false)]
        public TransformType[] Transforms {
            get {
                return this.transformsField;
            }
            set {
                this.transformsField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string URI {
            get {
                return this.uRIField;
            }
            set {
                this.uRIField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("RSAKeyValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class RSAKeyValueType {
        
        private byte[] modulusField;
        
        private byte[] exponentField;
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Modulus {
            get {
                return this.modulusField;
            }
            set {
                this.modulusField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Exponent {
            get {
                return this.exponentField;
            }
            set {
                this.exponentField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("DSAKeyValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class DSAKeyValueType {
        
        private byte[] pField;
        
        private byte[] qField;
        
        private byte[] gField;
        
        private byte[] yField;
        
        private byte[] jField;
        
        private byte[] seedField;
        
        private byte[] pgenCounterField;
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] P {
            get {
                return this.pField;
            }
            set {
                this.pField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Q {
            get {
                return this.qField;
            }
            set {
                this.qField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] G {
            get {
                return this.gField;
            }
            set {
                this.gField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Y {
            get {
                return this.yField;
            }
            set {
                this.yField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] J {
            get {
                return this.jField;
            }
            set {
                this.jField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Seed {
            get {
                return this.seedField;
            }
            set {
                this.seedField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] PgenCounter {
            get {
                return this.pgenCounterField;
            }
            set {
                this.pgenCounterField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("KeyValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class KeyValueType {
        
        private object itemField;
        
        private string[] textField;
        
        
        [XmlAnyElement()]
        [XmlElement("DSAKeyValue", typeof(DSAKeyValueType))]
        [XmlElement("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        
        [XmlText()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
    }
                
    [System.Serializable()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType2 {
        
        
        [XmlEnum("##any:")]
        Item,
        
        
        KeyName,
        
        
        KeyValue,
        
        
        MgmtData,
        
        
        PGPData,
        
        
        RetrievalMethod,
        
        
        SPKIData,
        
        
        X509Data,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("EncryptionMethod", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class EncryptionMethodType {
        
        private string keySizeField;
        
        private byte[] oAEPparamsField;
        
        private System.Xml.XmlNode[] anyField;
        
        private string algorithmField;
        
        
        [XmlElement(DataType="integer")]
        public string KeySize {
            get {
                return this.keySizeField;
            }
            set {
                this.keySizeField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] OAEPparams {
            get {
                return this.oAEPparamsField;
            }
            set {
                this.oAEPparamsField = value;
            }
        }
        
        
        [XmlText()]
        [XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return this.algorithmField;
            }
            set {
                this.algorithmField = value;
            }
        }
    }
    
    [XmlInclude(typeof(EncryptedKeyType))]
    [XmlInclude(typeof(EncryptedDataType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    public abstract partial class EncryptedType {
        
        private EncryptionMethodType encryptionMethodField;
        
        private KeyInfoType keyInfoField;
        
        private CipherDataType cipherDataField;
        
        private EncryptionPropertiesType encryptionPropertiesField;
        
        private string idField;
        
        private string typeField;
        
        private string mimeTypeField;
        
        private string encodingField;
        
        
        public EncryptionMethodType EncryptionMethod {
            get {
                return this.encryptionMethodField;
            }
            set {
                this.encryptionMethodField = value;
            }
        }
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public KeyInfoType KeyInfo {
            get {
                return this.keyInfoField;
            }
            set {
                this.keyInfoField = value;
            }
        }
        
        
        public CipherDataType CipherData {
            get {
                return this.cipherDataField;
            }
            set {
                this.cipherDataField = value;
            }
        }
        
        
        public EncryptionPropertiesType EncryptionProperties {
            get {
                return this.encryptionPropertiesField;
            }
            set {
                this.encryptionPropertiesField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string MimeType {
            get {
                return this.mimeTypeField;
            }
            set {
                this.mimeTypeField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Encoding {
            get {
                return this.encodingField;
            }
            set {
                this.encodingField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("EncryptionProperties", Namespace="http://www.w3.org/2001/04/xmlenc#", IsNullable=false)]
    public partial class EncryptionPropertiesType {
        
        private EncryptionPropertyType[] encryptionPropertyField;
        
        private string idField;
        
        
        [XmlElement("EncryptionProperty")]
        public EncryptionPropertyType[] EncryptionProperty {
            get {
                return this.encryptionPropertyField;
            }
            set {
                this.encryptionPropertyField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("EncryptionProperty", Namespace="http://www.w3.org/2001/04/xmlenc#", IsNullable=false)]
    public partial class EncryptionPropertyType {
        
        private System.Xml.XmlElement[] itemsField;
        
        private string[] textField;
        
        private string targetField;
        
        private string idField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        [XmlText()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Target {
            get {
                return this.targetField;
            }
            set {
                this.targetField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("EncryptedData", Namespace="http://www.w3.org/2001/04/xmlenc#", IsNullable=false)]
    public partial class EncryptedDataType : EncryptedType {
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("EncryptedKey", Namespace="http://www.w3.org/2001/04/xmlenc#", IsNullable=false)]
    public partial class EncryptedKeyType : EncryptedType {
        
        private ReferenceList referenceListField;
        
        private string carriedKeyNameField;
        
        private string recipientField;
        
        
        public ReferenceList ReferenceList {
            get {
                return this.referenceListField;
            }
            set {
                this.referenceListField = value;
            }
        }
        
        
        public string CarriedKeyName {
            get {
                return this.carriedKeyNameField;
            }
            set {
                this.carriedKeyNameField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Recipient {
            get {
                return this.recipientField;
            }
            set {
                this.recipientField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(AnonymousType=true, Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot(Namespace="http://www.w3.org/2001/04/xmlenc#", IsNullable=false)]
    public partial class ReferenceList {
        
        private ReferenceType[] itemsField;
        
        private ItemsChoiceType3[] itemsElementNameField;
        
        
        [XmlElement("DataReference", typeof(ReferenceType))]
        [XmlElement("KeyReference", typeof(ReferenceType))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public ReferenceType[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceType3[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
    }
        
    [System.Serializable()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#", IncludeInSchema=false)]
    public enum ItemsChoiceType3 {
        
        
        DataReference,
        
        
        KeyReference,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2001/04/xmlenc#")]
    [XmlRoot("AgreementMethod", Namespace="http://www.w3.org/2001/04/xmlenc#", IsNullable=false)]
    public partial class AgreementMethodType {
        
        private byte[] kANonceField;
        
        private System.Xml.XmlNode[] anyField;
        
        private KeyInfoType originatorKeyInfoField;
        
        private KeyInfoType recipientKeyInfoField;
        
        private string algorithmField;
        
        
        [XmlElement("KA-Nonce", DataType="base64Binary")]
        public byte[] KANonce {
            get {
                return this.kANonceField;
            }
            set {
                this.kANonceField = value;
            }
        }
        
        
        [XmlText()]
        [XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        
        public KeyInfoType OriginatorKeyInfo {
            get {
                return this.originatorKeyInfoField;
            }
            set {
                this.originatorKeyInfoField = value;
            }
        }
        
        
        public KeyInfoType RecipientKeyInfo {
            get {
                return this.recipientKeyInfoField;
            }
            set {
                this.recipientKeyInfoField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return this.algorithmField;
            }
            set {
                this.algorithmField = value;
            }
        }
    }
                                
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(TypeName = "ReferenceType", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ReferenceType1
    {

        private TransformType[] transformsField;

        private DigestMethodType digestMethodField;

        private byte[] digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [XmlArrayItem("Transform", IsNullable = false)]
        public TransformType[] Transforms {
            get {
                return this.transformsField;
            }
            set {
                this.transformsField = value;
            }
        }

        /// <remarks/>
        public DigestMethodType DigestMethod {
            get {
                return this.digestMethodField;
            }
            set {
                this.digestMethodField = value;
            }
        }

        /// <remarks/>
        [XmlElement(DataType = "base64Binary")]
        public byte[] DigestValue {
            get {
                return this.digestValueField;
            }
            set {
                this.digestValueField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(DataType = "ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(DataType = "anyURI")]
        public string URI {
            get {
                return this.uRIField;
            }
            set {
                this.uRIField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(DataType = "anyURI")]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("SignatureValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignatureValueType {
        
        private string idField;
        
        private byte[] valueField;
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlText(DataType="base64Binary")]
        public byte[] Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("Object", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class ObjectType {
        
        private System.Xml.XmlNode[] anyField;
        
        private string idField;
        
        private string mimeTypeField;
        
        private string encodingField;
        
        
        [XmlText()]
        [XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string MimeType {
            get {
                return this.mimeTypeField;
            }
            set {
                this.mimeTypeField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Encoding {
            get {
                return this.encodingField;
            }
            set {
                this.encodingField = value;
            }
        }
    }
                
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("Manifest", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class ManifestType {
        
        private ReferenceType1[] referenceField;
        
        private string idField;
        
        
        [XmlElement("Reference")]
        public ReferenceType1[] Reference {
            get {
                return this.referenceField;
            }
            set {
                this.referenceField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("SignatureProperties", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignaturePropertiesType {
        
        private SignaturePropertyType[] signaturePropertyField;
        
        private string idField;
        
        
        [XmlElement("SignatureProperty")]
        public SignaturePropertyType[] SignatureProperty {
            get {
                return this.signaturePropertyField;
            }
            set {
                this.signaturePropertyField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("SignatureProperty", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignaturePropertyType {
        
        private System.Xml.XmlElement[] itemsField;
        
        private string[] textField;
        
        private string targetField;
        
        private string idField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        [XmlText()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Target {
            get {
                return this.targetField;
            }
            set {
                this.targetField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("BaseID", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public abstract partial class BaseIDAbstractType {
        
        private string nameQualifierField;
        
        private string sPNameQualifierField;
        
        
        [XmlAttribute()]
        public string NameQualifier {
            get {
                return this.nameQualifierField;
            }
            set {
                this.nameQualifierField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string SPNameQualifier {
            get {
                return this.sPNameQualifierField;
            }
            set {
                this.sPNameQualifierField = value;
            }
        }
    }
                
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("EncryptedID", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class EncryptedElementType {
        
        private EncryptedDataType encryptedDataField;
        
        private EncryptedKeyType[] encryptedKeyField;
        
        
        [XmlElement(Namespace="http://www.w3.org/2001/04/xmlenc#")]
        public EncryptedDataType EncryptedData {
            get {
                return this.encryptedDataField;
            }
            set {
                this.encryptedDataField = value;
            }
        }
        
        
        [XmlElement("EncryptedKey", Namespace="http://www.w3.org/2001/04/xmlenc#")]
        public EncryptedKeyType[] EncryptedKey {
            get {
                return this.encryptedKeyField;
            }
            set {
                this.encryptedKeyField = value;
            }
        }
    }
                                    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    public partial class KeyInfoConfirmationDataType : SubjectConfirmationDataType {
    }
                            
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("OneTimeUse", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class OneTimeUseType : ConditionAbstractType {
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("ProxyRestriction", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class ProxyRestrictionType : ConditionAbstractType {
        
        private string[] audienceField;
        
        private string countField;
        
        
        [XmlElement("Audience", DataType="anyURI")]
        public string[] Audience {
            get {
                return this.audienceField;
            }
            set {
                this.audienceField = value;
            }
        }
        
        
        [XmlAttribute(DataType="nonNegativeInteger")]
        public string Count {
            get {
                return this.countField;
            }
            set {
                this.countField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Advice", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class AdviceType {
        
        private object[] itemsField;
        
        private ItemsChoiceType4[] itemsElementNameField;
        
        
        [XmlAnyElement()]
        [XmlElement("Assertion", typeof(AssertionType))]
        [XmlElement("AssertionIDRef", typeof(string), DataType="NCName")]
        [XmlElement("AssertionURIRef", typeof(string), DataType="anyURI")]
        [XmlElement("EncryptedAssertion", typeof(EncryptedElementType))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceType4[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
    }
        
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IncludeInSchema=false)]
    public enum ItemsChoiceType4 {
        
        
        [XmlEnum("##any:")]
        Item,
        
        
        Assertion,
        
        
        AssertionIDRef,
        
        
        AssertionURIRef,
        
        
        EncryptedAssertion,
    }
            
    [XmlInclude(typeof(RequestedAttributeType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Attribute", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class AttributeType {
        
        private object[] attributeValueField;
        
        private string nameField;
        
        private string nameFormatField;
        
        private string friendlyNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        [XmlElement("AttributeValue", IsNullable=true)]
        public object[] AttributeValue {
            get {
                return this.attributeValueField;
            }
            set {
                this.attributeValueField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string NameFormat {
            get {
                return this.nameFormatField;
            }
            set {
                this.nameFormatField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string FriendlyName {
            get {
                return this.friendlyNameField;
            }
            set {
                this.friendlyNameField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("RequestedAttribute", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class RequestedAttributeType : AttributeType {
        
        private bool isRequiredField;
        
        private bool isRequiredFieldSpecified;
        
        
        [XmlAttribute()]
        public bool isRequired {
            get {
                return this.isRequiredField;
            }
            set {
                this.isRequiredField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool isRequiredSpecified {
            get {
                return this.isRequiredFieldSpecified;
            }
            set {
                this.isRequiredFieldSpecified = value;
            }
        }
    }
    
    [XmlInclude(typeof(AttributeStatementType))]
    [XmlInclude(typeof(AuthzDecisionStatementType))]
    [XmlInclude(typeof(AuthnStatementType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Statement", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public abstract partial class StatementAbstractType {
    }
                
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("SubjectLocality", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class SubjectLocalityType {
        
        private string addressField;
        
        private string dNSNameField;
        
        
        [XmlAttribute()]
        public string Address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string DNSName {
            get {
                return this.dNSNameField;
            }
            set {
                this.dNSNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IncludeInSchema = false)]
    public enum ItemsChoiceType5
    {

        /// <remarks/>
        AuthnContextClassRef,

        /// <remarks/>
        AuthnContextDecl,

        /// <remarks/>
        AuthnContextDeclRef,
    }

    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("AuthzDecisionStatement", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class AuthzDecisionStatementType : StatementAbstractType {
        
        private ActionType[] actionField;
        
        private EvidenceType evidenceField;
        
        private string resourceField;
        
        private DecisionType decisionField;
        
        
        [XmlElement("Action")]
        public ActionType[] Action {
            get {
                return this.actionField;
            }
            set {
                this.actionField = value;
            }
        }
        
        
        public EvidenceType Evidence {
            get {
                return this.evidenceField;
            }
            set {
                this.evidenceField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Resource {
            get {
                return this.resourceField;
            }
            set {
                this.resourceField = value;
            }
        }
        
        
        [XmlAttribute()]
        public DecisionType Decision {
            get {
                return this.decisionField;
            }
            set {
                this.decisionField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Action", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class ActionType {
        
        private string namespaceField;
        
        private string valueField;
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Namespace {
            get {
                return this.namespaceField;
            }
            set {
                this.namespaceField = value;
            }
        }
        
        
        [XmlText()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Evidence", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IsNullable=false)]
    public partial class EvidenceType {
        
        private object[] itemsField;
        
        private ItemsChoiceType6[] itemsElementNameField;
        
        
        [XmlElement("Assertion", typeof(AssertionType))]
        [XmlElement("AssertionIDRef", typeof(string), DataType="NCName")]
        [XmlElement("AssertionURIRef", typeof(string), DataType="anyURI")]
        [XmlElement("EncryptedAssertion", typeof(EncryptedElementType))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceType6[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
    }
        
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion", IncludeInSchema=false)]
    public enum ItemsChoiceType6 {
        
        
        Assertion,
        
        
        AssertionIDRef,
        
        
        AssertionURIRef,
        
        
        EncryptedAssertion,
    }
        
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
    public enum DecisionType {
        
        
        Permit,
        
        
        Deny,
        
        
        Indeterminate,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("Extensions", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class ExtensionsType {
        
        private System.Xml.XmlElement[] anyField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("EntitiesDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class EntitiesDescriptorType {
        
        private SignatureType signatureField;
        
        private ExtensionsType extensionsField;
        
        private object[] itemsField;
        
        private System.DateTime validUntilField;
        
        private bool validUntilFieldSpecified;
        
        private string cacheDurationField;
        
        private string idField;
        
        private string nameField;
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
        
        
        public ExtensionsType Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        [XmlElement("EntitiesDescriptor", typeof(EntitiesDescriptorType))]
        [XmlElement("EntityDescriptor", typeof(EntityDescriptorType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime validUntil {
            get {
                return this.validUntilField;
            }
            set {
                this.validUntilField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool validUntilSpecified {
            get {
                return this.validUntilFieldSpecified;
            }
            set {
                this.validUntilFieldSpecified = value;
            }
        }
        
        
        [XmlAttribute(DataType="duration")]
        public string cacheDuration {
            get {
                return this.cacheDurationField;
            }
            set {
                this.cacheDurationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("EntityDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class EntityDescriptorType {
        
        private SignatureType signatureField;
        
        private ExtensionsType extensionsField;
        
        private object[] itemsField;
        
        private OrganizationType organizationField;
        
        private ContactType[] contactPersonField;
        
        private AdditionalMetadataLocationType[] additionalMetadataLocationField;
        
        private string entityIDField;
        
        private System.DateTime validUntilField;
        
        private bool validUntilFieldSpecified;
        
        private string cacheDurationField;
        
        private string idField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
        
        
        public ExtensionsType Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        [XmlElement("AffiliationDescriptor", typeof(AffiliationDescriptorType))]
        [XmlElement("AttributeAuthorityDescriptor", typeof(AttributeAuthorityDescriptorType))]
        [XmlElement("AuthnAuthorityDescriptor", typeof(AuthnAuthorityDescriptorType))]
        [XmlElement("IDPSSODescriptor", typeof(IDPSSODescriptorType))]
        [XmlElement("PDPDescriptor", typeof(PDPDescriptorType))]
        [XmlElement("RoleDescriptor", typeof(RoleDescriptorType))]
        [XmlElement("SPSSODescriptor", typeof(SPSSODescriptorType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        
        public OrganizationType Organization {
            get {
                return this.organizationField;
            }
            set {
                this.organizationField = value;
            }
        }
        
        
        [XmlElement("ContactPerson")]
        public ContactType[] ContactPerson {
            get {
                return this.contactPersonField;
            }
            set {
                this.contactPersonField = value;
            }
        }
        
        
        [XmlElement("AdditionalMetadataLocation")]
        public AdditionalMetadataLocationType[] AdditionalMetadataLocation {
            get {
                return this.additionalMetadataLocationField;
            }
            set {
                this.additionalMetadataLocationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string entityID {
            get {
                return this.entityIDField;
            }
            set {
                this.entityIDField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime validUntil {
            get {
                return this.validUntilField;
            }
            set {
                this.validUntilField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool validUntilSpecified {
            get {
                return this.validUntilFieldSpecified;
            }
            set {
                this.validUntilFieldSpecified = value;
            }
        }
        
        
        [XmlAttribute(DataType="duration")]
        public string cacheDuration {
            get {
                return this.cacheDurationField;
            }
            set {
                this.cacheDurationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("AffiliationDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class AffiliationDescriptorType {
        
        private SignatureType signatureField;
        
        private ExtensionsType extensionsField;
        
        private string[] affiliateMemberField;
        
        private KeyDescriptorType[] keyDescriptorField;
        
        private string affiliationOwnerIDField;
        
        private System.DateTime validUntilField;
        
        private bool validUntilFieldSpecified;
        
        private string cacheDurationField;
        
        private string idField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
        
        
        public ExtensionsType Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        [XmlElement("AffiliateMember", DataType="anyURI")]
        public string[] AffiliateMember {
            get {
                return this.affiliateMemberField;
            }
            set {
                this.affiliateMemberField = value;
            }
        }
        
        
        [XmlElement("KeyDescriptor")]
        public KeyDescriptorType[] KeyDescriptor {
            get {
                return this.keyDescriptorField;
            }
            set {
                this.keyDescriptorField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string affiliationOwnerID {
            get {
                return this.affiliationOwnerIDField;
            }
            set {
                this.affiliationOwnerIDField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime validUntil {
            get {
                return this.validUntilField;
            }
            set {
                this.validUntilField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool validUntilSpecified {
            get {
                return this.validUntilFieldSpecified;
            }
            set {
                this.validUntilFieldSpecified = value;
            }
        }
        
        
        [XmlAttribute(DataType="duration")]
        public string cacheDuration {
            get {
                return this.cacheDurationField;
            }
            set {
                this.cacheDurationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("KeyDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class KeyDescriptorType {
        
        private KeyInfoType keyInfoField;
        
        private EncryptionMethodType[] encryptionMethodField;
        
        private KeyTypes useField;
        
        private bool useFieldSpecified;
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public KeyInfoType KeyInfo {
            get {
                return this.keyInfoField;
            }
            set {
                this.keyInfoField = value;
            }
        }
        
        
        [XmlElement("EncryptionMethod")]
        public EncryptionMethodType[] EncryptionMethod {
            get {
                return this.encryptionMethodField;
            }
            set {
                this.encryptionMethodField = value;
            }
        }
        
        
        [XmlAttribute()]
        public KeyTypes use {
            get {
                return this.useField;
            }
            set {
                this.useField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool useSpecified {
            get {
                return this.useFieldSpecified;
            }
            set {
                this.useFieldSpecified = value;
            }
        }
    }
        
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    public enum KeyTypes {
        
        
        encryption,
        
        
        signing,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("AttributeAuthorityDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class AttributeAuthorityDescriptorType : RoleDescriptorType {
        
        private EndpointType[] attributeServiceField;
        
        private EndpointType[] assertionIDRequestServiceField;
        
        private string[] nameIDFormatField;
        
        private string[] attributeProfileField;
        
        private AttributeType[] attributeField;
        
        
        [XmlElement("AttributeService")]
        public EndpointType[] AttributeService {
            get {
                return this.attributeServiceField;
            }
            set {
                this.attributeServiceField = value;
            }
        }
        
        
        [XmlElement("AssertionIDRequestService")]
        public EndpointType[] AssertionIDRequestService {
            get {
                return this.assertionIDRequestServiceField;
            }
            set {
                this.assertionIDRequestServiceField = value;
            }
        }
        
        
        [XmlElement("NameIDFormat", DataType="anyURI")]
        public string[] NameIDFormat {
            get {
                return this.nameIDFormatField;
            }
            set {
                this.nameIDFormatField = value;
            }
        }
        
        
        [XmlElement("AttributeProfile", DataType="anyURI")]
        public string[] AttributeProfile {
            get {
                return this.attributeProfileField;
            }
            set {
                this.attributeProfileField = value;
            }
        }
        
        
        [XmlElement("Attribute", Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public AttributeType[] Attribute {
            get {
                return this.attributeField;
            }
            set {
                this.attributeField = value;
            }
        }
    }
    
    [XmlInclude(typeof(IndexedEndpointType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("SingleLogoutService", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class EndpointType {
        
        private System.Xml.XmlElement[] anyField;
        
        private string bindingField;
        
        private string locationField;
        
        private string responseLocationField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Binding {
            get {
                return this.bindingField;
            }
            set {
                this.bindingField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string ResponseLocation {
            get {
                return this.responseLocationField;
            }
            set {
                this.responseLocationField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    [XmlInclude(typeof(AttributeAuthorityDescriptorType))]
    [XmlInclude(typeof(PDPDescriptorType))]
    [XmlInclude(typeof(AuthnAuthorityDescriptorType))]
    [XmlInclude(typeof(SSODescriptorType))]
    [XmlInclude(typeof(SPSSODescriptorType))]
    [XmlInclude(typeof(IDPSSODescriptorType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("RoleDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public abstract partial class RoleDescriptorType {
        
        private SignatureType signatureField;
        
        private ExtensionsType extensionsField;
        
        private KeyDescriptorType[] keyDescriptorField;
        
        private OrganizationType organizationField;
        
        private ContactType[] contactPersonField;
        
        private string idField;
        
        private System.DateTime validUntilField;
        
        private bool validUntilFieldSpecified;
        
        private string cacheDurationField;
        
        private string[] protocolSupportEnumerationField;
        
        private string errorURLField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
        
        
        public ExtensionsType Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        [XmlElement("KeyDescriptor")]
        public KeyDescriptorType[] KeyDescriptor {
            get {
                return this.keyDescriptorField;
            }
            set {
                this.keyDescriptorField = value;
            }
        }
        
        
        public OrganizationType Organization {
            get {
                return this.organizationField;
            }
            set {
                this.organizationField = value;
            }
        }
        
        
        [XmlElement("ContactPerson")]
        public ContactType[] ContactPerson {
            get {
                return this.contactPersonField;
            }
            set {
                this.contactPersonField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime validUntil {
            get {
                return this.validUntilField;
            }
            set {
                this.validUntilField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool validUntilSpecified {
            get {
                return this.validUntilFieldSpecified;
            }
            set {
                this.validUntilFieldSpecified = value;
            }
        }
        
        
        [XmlAttribute(DataType="duration")]
        public string cacheDuration {
            get {
                return this.cacheDurationField;
            }
            set {
                this.cacheDurationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string[] protocolSupportEnumeration {
            get {
                return this.protocolSupportEnumerationField;
            }
            set {
                this.protocolSupportEnumerationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string errorURL {
            get {
                return this.errorURLField;
            }
            set {
                this.errorURLField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("Organization", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class OrganizationType {
        
        private ExtensionsType extensionsField;
        
        private localizedNameType[] organizationNameField;
        
        private localizedNameType[] organizationDisplayNameField;
        
        private localizedURIType[] organizationURLField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        public ExtensionsType Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        [XmlElement("OrganizationName")]
        public localizedNameType[] OrganizationName {
            get {
                return this.organizationNameField;
            }
            set {
                this.organizationNameField = value;
            }
        }
        
        
        [XmlElement("OrganizationDisplayName")]
        public localizedNameType[] OrganizationDisplayName {
            get {
                return this.organizationDisplayNameField;
            }
            set {
                this.organizationDisplayNameField = value;
            }
        }
        
        
        [XmlElement("OrganizationURL")]
        public localizedURIType[] OrganizationURL {
            get {
                return this.organizationURLField;
            }
            set {
                this.organizationURLField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }    
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("OrganizationName", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class localizedNameType {
        
        private string langField;
        
        private string valueField;
        
        
        [XmlAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://www.w3.org/XML/1998/namespace")]
        public string lang {
            get {
                return this.langField;
            }
            set {
                this.langField = value;
            }
        }
        
        
        [XmlText()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("OrganizationURL", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class localizedURIType {
        
        private string langField;
        
        private string valueField;
        
        
        [XmlAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://www.w3.org/XML/1998/namespace")]
        public string lang {
            get {
                return this.langField;
            }
            set {
                this.langField = value;
            }
        }
        
        
        [XmlText(DataType="anyURI")]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("ContactPerson", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class ContactType {
        
        private ExtensionsType extensionsField;
        
        private string companyField;
        
        private string givenNameField;
        
        private string surNameField;
        
        private string[] emailAddressField;
        
        private string[] telephoneNumberField;
        
        private ContactTypeType contactTypeField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        
        public ExtensionsType Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        public string Company {
            get {
                return this.companyField;
            }
            set {
                this.companyField = value;
            }
        }
        
        
        public string GivenName {
            get {
                return this.givenNameField;
            }
            set {
                this.givenNameField = value;
            }
        }
        
        
        public string SurName {
            get {
                return this.surNameField;
            }
            set {
                this.surNameField = value;
            }
        }
        
        
        [XmlElement("EmailAddress", DataType="anyURI")]
        public string[] EmailAddress {
            get {
                return this.emailAddressField;
            }
            set {
                this.emailAddressField = value;
            }
        }
        
        
        [XmlElement("TelephoneNumber")]
        public string[] TelephoneNumber {
            get {
                return this.telephoneNumberField;
            }
            set {
                this.telephoneNumberField = value;
            }
        }
        
        
        [XmlAttribute()]
        public ContactTypeType contactType {
            get {
                return this.contactTypeField;
            }
            set {
                this.contactTypeField = value;
            }
        }
        
        
        [XmlAnyAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
        
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    public enum ContactTypeType {
        
        
        technical,
        
        
        support,
        
        
        administrative,
        
        
        billing,
        
        
        other,
    }
    
    [XmlInclude(typeof(SPSSODescriptorType))]
    [XmlInclude(typeof(IDPSSODescriptorType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    public abstract partial class SSODescriptorType : RoleDescriptorType {
        
        private IndexedEndpointType[] artifactResolutionServiceField;
        
        private EndpointType[] singleLogoutServiceField;
        
        private EndpointType[] manageNameIDServiceField;
        
        private string[] nameIDFormatField;
        
        
        [XmlElement("ArtifactResolutionService")]
        public IndexedEndpointType[] ArtifactResolutionService {
            get {
                return this.artifactResolutionServiceField;
            }
            set {
                this.artifactResolutionServiceField = value;
            }
        }
        
        
        [XmlElement("SingleLogoutService")]
        public EndpointType[] SingleLogoutService {
            get {
                return this.singleLogoutServiceField;
            }
            set {
                this.singleLogoutServiceField = value;
            }
        }
        
        
        [XmlElement("ManageNameIDService")]
        public EndpointType[] ManageNameIDService {
            get {
                return this.manageNameIDServiceField;
            }
            set {
                this.manageNameIDServiceField = value;
            }
        }
        
        
        [XmlElement("NameIDFormat", DataType="anyURI")]
        public string[] NameIDFormat {
            get {
                return this.nameIDFormatField;
            }
            set {
                this.nameIDFormatField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("ArtifactResolutionService", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class IndexedEndpointType : EndpointType {
        
        private ushort indexField;
        
        private bool isDefaultField;
        
        private bool isDefaultFieldSpecified;
        
        
        [XmlAttribute()]
        public ushort index {
            get {
                return this.indexField;
            }
            set {
                this.indexField = value;
            }
        }
        
        
        [XmlAttribute()]
        public bool isDefault {
            get {
                return this.isDefaultField;
            }
            set {
                this.isDefaultField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool isDefaultSpecified {
            get {
                return this.isDefaultFieldSpecified;
            }
            set {
                this.isDefaultFieldSpecified = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("AuthnAuthorityDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class AuthnAuthorityDescriptorType : RoleDescriptorType {
        
        private EndpointType[] authnQueryServiceField;
        
        private EndpointType[] assertionIDRequestServiceField;
        
        private string[] nameIDFormatField;
        
        
        [XmlElement("AuthnQueryService")]
        public EndpointType[] AuthnQueryService {
            get {
                return this.authnQueryServiceField;
            }
            set {
                this.authnQueryServiceField = value;
            }
        }
        
        
        [XmlElement("AssertionIDRequestService")]
        public EndpointType[] AssertionIDRequestService {
            get {
                return this.assertionIDRequestServiceField;
            }
            set {
                this.assertionIDRequestServiceField = value;
            }
        }
        
        
        [XmlElement("NameIDFormat", DataType="anyURI")]
        public string[] NameIDFormat {
            get {
                return this.nameIDFormatField;
            }
            set {
                this.nameIDFormatField = value;
            }
        }
    }
            
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("PDPDescriptor", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class PDPDescriptorType : RoleDescriptorType {
        
        private EndpointType[] authzServiceField;
        
        private EndpointType[] assertionIDRequestServiceField;
        
        private string[] nameIDFormatField;
        
        
        [XmlElement("AuthzService")]
        public EndpointType[] AuthzService {
            get {
                return this.authzServiceField;
            }
            set {
                this.authzServiceField = value;
            }
        }
        
        
        [XmlElement("AssertionIDRequestService")]
        public EndpointType[] AssertionIDRequestService {
            get {
                return this.assertionIDRequestServiceField;
            }
            set {
                this.assertionIDRequestServiceField = value;
            }
        }
        
        
        [XmlElement("NameIDFormat", DataType="anyURI")]
        public string[] NameIDFormat {
            get {
                return this.nameIDFormatField;
            }
            set {
                this.nameIDFormatField = value;
            }
        }
    }
                
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("AttributeConsumingService", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class AttributeConsumingServiceType {
        
        private localizedNameType[] serviceNameField;
        
        private localizedNameType[] serviceDescriptionField;
        
        private RequestedAttributeType[] requestedAttributeField;
        
        private ushort indexField;
        
        private bool isDefaultField;
        
        private bool isDefaultFieldSpecified;
        
        
        [XmlElement("ServiceName")]
        public localizedNameType[] ServiceName {
            get {
                return this.serviceNameField;
            }
            set {
                this.serviceNameField = value;
            }
        }
        
        
        [XmlElement("ServiceDescription")]
        public localizedNameType[] ServiceDescription {
            get {
                return this.serviceDescriptionField;
            }
            set {
                this.serviceDescriptionField = value;
            }
        }
        
        
        [XmlElement("RequestedAttribute")]
        public RequestedAttributeType[] RequestedAttribute {
            get {
                return this.requestedAttributeField;
            }
            set {
                this.requestedAttributeField = value;
            }
        }
        
        
        [XmlAttribute()]
        public ushort index {
            get {
                return this.indexField;
            }
            set {
                this.indexField = value;
            }
        }
        
        
        [XmlAttribute()]
        public bool isDefault {
            get {
                return this.isDefaultField;
            }
            set {
                this.isDefaultField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool isDefaultSpecified {
            get {
                return this.isDefaultFieldSpecified;
            }
            set {
                this.isDefaultFieldSpecified = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("AdditionalMetadataLocation", Namespace="urn:oasis:names:tc:SAML:2.0:metadata", IsNullable=false)]
    public partial class AdditionalMetadataLocationType {
        
        private string namespaceField;
        
        private string valueField;
        
        
        [XmlAttribute(DataType="anyURI")]
        public string @namespace {
            get {
                return this.namespaceField;
            }
            set {
                this.namespaceField = value;
            }
        }
        
        
        [XmlText(DataType="anyURI")]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(TypeName="ExtensionsType", Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("Extensions", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class ExtensionsType1 {
        
        private System.Xml.XmlElement[] anyField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
                        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("StatusDetail", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class StatusDetailType {
        
        private System.Xml.XmlElement[] anyField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("AssertionIDRequest", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class AssertionIDRequestType : RequestAbstractType {
        
        private string[] assertionIDRefField;
        
        
        [XmlElement("AssertionIDRef", Namespace="urn:oasis:names:tc:SAML:2.0:assertion", DataType="NCName")]
        public string[] AssertionIDRef {
            get {
                return this.assertionIDRefField;
            }
            set {
                this.assertionIDRefField = value;
            }
        }
    }
    
    [XmlInclude(typeof(NameIDMappingRequestType))]
    [XmlInclude(typeof(LogoutRequestType))]
    [XmlInclude(typeof(ManageNameIDRequestType))]
    [XmlInclude(typeof(ArtifactResolveType))]
    [XmlInclude(typeof(AuthnRequestType))]
    [XmlInclude(typeof(SubjectQueryAbstractType))]
    [XmlInclude(typeof(AuthzDecisionQueryType))]
    [XmlInclude(typeof(AttributeQueryType))]
    [XmlInclude(typeof(AuthnQueryType))]
    [XmlInclude(typeof(AssertionIDRequestType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    public abstract partial class RequestAbstractType {
        
        private NameIDType issuerField;
        
        private SignatureType signatureField;
        
        private ExtensionsType1 extensionsField;
        
        private string idField;
        
        private string versionField;
        
        private System.DateTime issueInstantField;
        
        private string destinationField;
        
        private string consentField;
        
        
        [XmlElement(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public NameIDType Issuer {
            get {
                return this.issuerField;
            }
            set {
                this.issuerField = value;
            }
        }
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
        
        
        public ExtensionsType1 Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime IssueInstant {
            get {
                return this.issueInstantField;
            }
            set {
                this.issueInstantField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Destination {
            get {
                return this.destinationField;
            }
            set {
                this.destinationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Consent {
            get {
                return this.consentField;
            }
            set {
                this.consentField = value;
            }
        }
    }
    
    [XmlInclude(typeof(AuthzDecisionQueryType))]
    [XmlInclude(typeof(AttributeQueryType))]
    [XmlInclude(typeof(AuthnQueryType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("SubjectQuery", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public abstract partial class SubjectQueryAbstractType : RequestAbstractType {
        
        private SubjectType subjectField;
        
        
        [XmlElement(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public SubjectType Subject {
            get {
                return this.subjectField;
            }
            set {
                this.subjectField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("AuthnQuery", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class AuthnQueryType : SubjectQueryAbstractType {
        
        private RequestedAuthnContextType requestedAuthnContextField;
        
        private string sessionIndexField;
        
        
        public RequestedAuthnContextType RequestedAuthnContext {
            get {
                return this.requestedAuthnContextField;
            }
            set {
                this.requestedAuthnContextField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string SessionIndex {
            get {
                return this.sessionIndexField;
            }
            set {
                this.sessionIndexField = value;
            }
        }
    }
                
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IncludeInSchema=false)]
    public enum ItemsChoiceType7 {
        
        
        [XmlEnum("urn:oasis:names:tc:SAML:2.0:assertion:AuthnContextClassRef")]
        AuthnContextClassRef,
        
        
        [XmlEnum("urn:oasis:names:tc:SAML:2.0:assertion:AuthnContextDeclRef")]
        AuthnContextDeclRef,
    }
        
    [System.Serializable()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    public enum AuthnContextComparisonType {
        
        
        exact,
        
        
        minimum,
        
        
        maximum,
        
        
        better,
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("AttributeQuery", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class AttributeQueryType : SubjectQueryAbstractType {
        
        private AttributeType[] attributeField;
        
        
        [XmlElement("Attribute", Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public AttributeType[] Attribute {
            get {
                return this.attributeField;
            }
            set {
                this.attributeField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("AuthzDecisionQuery", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class AuthzDecisionQueryType : SubjectQueryAbstractType {
        
        private ActionType[] actionField;
        
        private EvidenceType evidenceField;
        
        private string resourceField;
        
        
        [XmlElement("Action", Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public ActionType[] Action {
            get {
                return this.actionField;
            }
            set {
                this.actionField = value;
            }
        }
        
        
        [XmlElement(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public EvidenceType Evidence {
            get {
                return this.evidenceField;
            }
            set {
                this.evidenceField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Resource {
            get {
                return this.resourceField;
            }
            set {
                this.resourceField = value;
            }
        }
    }
                
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("NameIDPolicy", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class NameIDPolicyType {
        
        private string formatField;
        
        private string sPNameQualifierField;
        
        private bool allowCreateField;
        
        private bool allowCreateFieldSpecified;
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string SPNameQualifier {
            get {
                return this.sPNameQualifierField;
            }
            set {
                this.sPNameQualifierField = value;
            }
        }
        
        
        [XmlAttribute()]
        public bool AllowCreate {
            get {
                return this.allowCreateField;
            }
            set {
                this.allowCreateField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool AllowCreateSpecified {
            get {
                return this.allowCreateFieldSpecified;
            }
            set {
                this.allowCreateFieldSpecified = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("Scoping", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class ScopingType {
        
        private IDPListType iDPListField;
        
        private string[] requesterIDField;
        
        private string proxyCountField;
        
        
        public IDPListType IDPList {
            get {
                return this.iDPListField;
            }
            set {
                this.iDPListField = value;
            }
        }
        
        
        [XmlElement("RequesterID", DataType="anyURI")]
        public string[] RequesterID {
            get {
                return this.requesterIDField;
            }
            set {
                this.requesterIDField = value;
            }
        }
        
        
        [XmlAttribute(DataType="nonNegativeInteger")]
        public string ProxyCount {
            get {
                return this.proxyCountField;
            }
            set {
                this.proxyCountField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("IDPList", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class IDPListType {
        
        private IDPEntryType[] iDPEntryField;
        
        private string getCompleteField;
        
        
        [XmlElement("IDPEntry")]
        public IDPEntryType[] IDPEntry {
            get {
                return this.iDPEntryField;
            }
            set {
                this.iDPEntryField = value;
            }
        }
        
        
        [XmlElement(DataType="anyURI")]
        public string GetComplete {
            get {
                return this.getCompleteField;
            }
            set {
                this.getCompleteField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("IDPEntry", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class IDPEntryType {
        
        private string providerIDField;
        
        private string nameField;
        
        private string locField;
        
        
        [XmlAttribute(DataType="anyURI")]
        public string ProviderID {
            get {
                return this.providerIDField;
            }
            set {
                this.providerIDField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Loc {
            get {
                return this.locField;
            }
            set {
                this.locField = value;
            }
        }
    }
            
    [XmlInclude(typeof(NameIDMappingResponseType))]
    [XmlInclude(typeof(ArtifactResponseType))]
    [XmlInclude(typeof(ResponseType))]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("ManageNameIDResponse", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class StatusResponseType {
        
        private NameIDType issuerField;
        
        private SignatureType signatureField;
        
        private ExtensionsType1 extensionsField;
        
        private StatusType statusField;
        
        private string idField;
        
        private string inResponseToField;
        
        private string versionField;
        
        private System.DateTime issueInstantField;
        
        private string destinationField;
        
        private string consentField;
        
        
        [XmlElement(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public NameIDType Issuer {
            get {
                return this.issuerField;
            }
            set {
                this.issuerField = value;
            }
        }
        
        
        [XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
        
        
        public ExtensionsType1 Extensions {
            get {
                return this.extensionsField;
            }
            set {
                this.extensionsField = value;
            }
        }
        
        
        public StatusType Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        
        [XmlAttribute(DataType="NCName")]
        public string InResponseTo {
            get {
                return this.inResponseToField;
            }
            set {
                this.inResponseToField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime IssueInstant {
            get {
                return this.issueInstantField;
            }
            set {
                this.issueInstantField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Destination {
            get {
                return this.destinationField;
            }
            set {
                this.destinationField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Consent {
            get {
                return this.consentField;
            }
            set {
                this.consentField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("ArtifactResolve", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class ArtifactResolveType : RequestAbstractType {
        
        private string artifactField;
        
        
        public string Artifact {
            get {
                return this.artifactField;
            }
            set {
                this.artifactField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("ArtifactResponse", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class ArtifactResponseType : StatusResponseType {
        
        private System.Xml.XmlElement anyField;
        
        
        [XmlAnyElement()]
        public System.Xml.XmlElement Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("ManageNameIDRequest", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class ManageNameIDRequestType : RequestAbstractType {
        
        private object itemField;
        
        private object item1Field;
        
        
        [XmlElement("EncryptedID", typeof(EncryptedElementType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("NameID", typeof(NameIDType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        
        [XmlElement("NewEncryptedID", typeof(EncryptedElementType))]
        [XmlElement("NewID", typeof(string))]
        [XmlElement("Terminate", typeof(TerminateType))]
        public object Item1 {
            get {
                return this.item1Field;
            }
            set {
                this.item1Field = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("Terminate", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class TerminateType {
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("LogoutRequest", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class LogoutRequestType : RequestAbstractType {
        
        private object itemField;
        
        private string[] sessionIndexField;
        
        private string reasonField;
        
        private System.DateTime notOnOrAfterField;
        
        private bool notOnOrAfterFieldSpecified;
        
        
        [XmlElement("BaseID", typeof(BaseIDAbstractType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("EncryptedID", typeof(EncryptedElementType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("NameID", typeof(NameIDType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        
        [XmlElement("SessionIndex")]
        public string[] SessionIndex {
            get {
                return this.sessionIndexField;
            }
            set {
                this.sessionIndexField = value;
            }
        }
        
        
        [XmlAttribute()]
        public string Reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
            }
        }
        
        
        [XmlAttribute()]
        public System.DateTime NotOnOrAfter {
            get {
                return this.notOnOrAfterField;
            }
            set {
                this.notOnOrAfterField = value;
            }
        }
        
        
        [XmlIgnore()]
        public bool NotOnOrAfterSpecified {
            get {
                return this.notOnOrAfterFieldSpecified;
            }
            set {
                this.notOnOrAfterFieldSpecified = value;
            }
        }
    }    
    
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("NameIDMappingRequest", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class NameIDMappingRequestType : RequestAbstractType {
        
        private object itemField;
        
        private NameIDPolicyType nameIDPolicyField;
        
        
        [XmlElement("BaseID", typeof(BaseIDAbstractType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("EncryptedID", typeof(EncryptedElementType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("NameID", typeof(NameIDType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        
        public NameIDPolicyType NameIDPolicy {
            get {
                return this.nameIDPolicyField;
            }
            set {
                this.nameIDPolicyField = value;
            }
        }
    }
        
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("NameIDMappingResponse", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class NameIDMappingResponseType : StatusResponseType {
        
        private object itemField;
        
        
        [XmlElement("EncryptedID", typeof(EncryptedElementType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("NameID", typeof(NameIDType), Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
}
