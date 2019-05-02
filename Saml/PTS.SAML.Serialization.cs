//-----------------------------------------------------------------------------------------------------------------------------
// <code-history>
//      Created on: 02/16/2018
//      Created by: timc@pts1.com
//      These partial clases were migrated from SAML20.Serialization.cs
//      All contained classes are SAML 2.0 DTOs migrated and modified from the original XSD Template
//      These classes are used to Serialize and De-Serialize SAML 2.0 schemas to SSO Data Transfer Objects
//      The XSDSAMLSerializationTemplate.V4.6 was generated from the original W3C OASIS SAML 2.0 Schema library
//      For more information see: 
//          http://saml.xml.org/saml-specifications
//          https://www.oasis-open.org/standards#samlv2.0
//          http://www.w3.org/2002/ws/databinding/edcopy/collection/detected/OASIS-SAML-Protocol-2_0.html
//
//      Last update: 02/16/2019 - Migrate common PTS Serialization classes from SAML20.Serialization.cs master file to PTS SALM 2.0 Serialization file
// </code-history>
//-----------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SSOService.Services;

namespace SSOService.Saml
{
    #region SAML Serialization Classes
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("IDPSSODescriptor", Namespace = "urn:oasis:names:tc:SAML:2.0:metadata", IsNullable = false)]
    public partial class IDPSSODescriptorType : SSODescriptorType
    {
        #region Fields...
        private EndpointType[] singleSignOnServiceField;
        private EndpointType[] nameIDMappingServiceField;
        private EndpointType[] assertionIDRequestServiceField;
        private string[] attributeProfileField;
        private AttributeType[] attributeField;
        private bool wantAuthnRequestsSignedField;
        private bool wantAuthnRequestsSignedFieldSpecified;
        #endregion

        [XmlElement("SingleSignOnService")]
        public EndpointType[] SingleSignOnService {
            get {
                return this.singleSignOnServiceField;
            }
            set {
                this.singleSignOnServiceField = value;
            }
        }

        [XmlElement("NameIDMappingService")]
        public EndpointType[] NameIDMappingService {
            get {
                return this.nameIDMappingServiceField;
            }
            set {
                this.nameIDMappingServiceField = value;
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

        [XmlElement("AttributeProfile", DataType = "anyURI")]
        public string[] AttributeProfile {
            get {
                return this.attributeProfileField;
            }
            set {
                this.attributeProfileField = value;
            }
        }

        [XmlElement("Attribute", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
        public AttributeType[] Attribute {
            get {
                return this.attributeField;
            }
            set {
                this.attributeField = value;
            }
        }

        [XmlAttribute()]
        public bool WantAuthnRequestsSigned {
            get {
                return this.wantAuthnRequestsSignedField;
            }
            set {
                this.wantAuthnRequestsSignedField = value;
            }
        }

        [XmlIgnore()]
        public bool WantAuthnRequestsSignedSpecified {
            get {
                return this.wantAuthnRequestsSignedFieldSpecified;
            }
            set {
                this.wantAuthnRequestsSignedFieldSpecified = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:metadata")]
    [XmlRoot("SPSSODescriptor", Namespace = "urn:oasis:names:tc:SAML:2.0:metadata", IsNullable = false)]
    public partial class SPSSODescriptorType : SSODescriptorType
    {
        #region Fields...
        private IndexedEndpointType[] assertionConsumerServiceField;
        private AttributeConsumingServiceType[] attributeConsumingServiceField;
        private bool authnRequestsSignedField;
        private bool authnRequestsSignedFieldSpecified;
        private bool wantAssertionsSignedField;
        private bool wantAssertionsSignedFieldSpecified;
        #endregion

        [XmlElement("AssertionConsumerService")]
        public IndexedEndpointType[] AssertionConsumerService {
            get {
                return this.assertionConsumerServiceField;
            }
            set {
                this.assertionConsumerServiceField = value;
            }
        }

        [XmlElement("AttributeConsumingService")]
        public AttributeConsumingServiceType[] AttributeConsumingService {
            get {
                return this.attributeConsumingServiceField;
            }
            set {
                this.attributeConsumingServiceField = value;
            }
        }

        [XmlAttribute()]
        public bool AuthnRequestsSigned {
            get {
                return this.authnRequestsSignedField;
            }
            set {
                this.authnRequestsSignedField = value;
            }
        }

        [XmlIgnore()]
        public bool AuthnRequestsSignedSpecified {
            get {
                return this.authnRequestsSignedFieldSpecified;
            }
            set {
                this.authnRequestsSignedFieldSpecified = value;
            }
        }

        [XmlAttribute()]
        public bool WantAssertionsSigned {
            get {
                return this.wantAssertionsSignedField;
            }
            set {
                this.wantAssertionsSignedField = value;
            }
        }

        [XmlIgnore()]
        public bool WantAssertionsSignedSpecified {
            get {
                return this.wantAssertionsSignedFieldSpecified;
            }
            set {
                this.wantAssertionsSignedFieldSpecified = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace="urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("AuthnRequest", Namespace="urn:oasis:names:tc:SAML:2.0:protocol", IsNullable=false)]
    public partial class AuthnRequestType : RequestAbstractType {

        #region Fields...
        private SubjectType subjectField;
        private NameIDPolicyType nameIDPolicyField;
        private ConditionsType conditionsField;
        private RequestedAuthnContextType requestedAuthnContextField;
        private ScopingType scopingField;
        private bool forceAuthnField;
        private bool forceAuthnFieldSpecified;
        private bool isPassiveField;        
        private bool isPassiveFieldSpecified;
        private string protocolBindingField;
        private ushort assertionConsumerServiceIndexField;
        private bool assertionConsumerServiceIndexFieldSpecified;
        private string assertionConsumerServiceURLField;
        private ushort attributeConsumingServiceIndexField;
        private bool attributeConsumingServiceIndexFieldSpecified;
        private string providerNameField;
        #endregion

        [XmlElement(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public SubjectType Subject {
            get {
                return this.subjectField;
            }
            set {
                this.subjectField = value;
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
        
        [XmlElement(Namespace="urn:oasis:names:tc:SAML:2.0:assertion")]
        public ConditionsType Conditions {
            get {
                return this.conditionsField;
            }
            set {
                this.conditionsField = value;
            }
        }
        
        public RequestedAuthnContextType RequestedAuthnContext {
            get {
                return this.requestedAuthnContextField;
            }
            set {
                this.requestedAuthnContextField = value;
            }
        }
        
        public ScopingType Scoping {
            get {
                return this.scopingField;
            }
            set {
                this.scopingField = value;
            }
        }
        
        [XmlAttribute()]
        public bool ForceAuthn {
            get {
                return this.forceAuthnField;
            }
            set {
                this.forceAuthnField = value;
            }
        }
        
        [XmlIgnore()]
        public bool ForceAuthnSpecified {
            get {
                return this.forceAuthnFieldSpecified;
            }
            set {
                this.forceAuthnFieldSpecified = value;
            }
        }
        
        [XmlAttribute()]
        public bool IsPassive {
            get {
                return this.isPassiveField;
            }
            set {
                this.isPassiveField = value;
            }
        }
        
        [XmlIgnore()]
        public bool IsPassiveSpecified {
            get {
                return this.isPassiveFieldSpecified;
            }
            set {
                this.isPassiveFieldSpecified = value;
            }
        }
        
        [XmlAttribute(DataType="anyURI")]
        public string ProtocolBinding {
            get {
                return this.protocolBindingField;
            }
            set {
                this.protocolBindingField = value;
            }
        }
        
        [XmlAttribute()]
        public ushort AssertionConsumerServiceIndex {
            get {
                return this.assertionConsumerServiceIndexField;
            }
            set {
                this.assertionConsumerServiceIndexField = value;
            }
        }
        
        [XmlIgnore()]
        public bool AssertionConsumerServiceIndexSpecified {
            get {
                return this.assertionConsumerServiceIndexFieldSpecified;
            }
            set {
                this.assertionConsumerServiceIndexFieldSpecified = value;
            }
        }
        
        [XmlAttribute(DataType="anyURI")]
        public string AssertionConsumerServiceURL {
            get {
                return this.assertionConsumerServiceURLField;
            }
            set {
                this.assertionConsumerServiceURLField = value;
            }
        }
        
        [XmlAttribute()]
        public ushort AttributeConsumingServiceIndex {
            get {
                return this.attributeConsumingServiceIndexField;
            }
            set {
                this.attributeConsumingServiceIndexField = value;
            }
        }
        
        [XmlIgnore()]
        public bool AttributeConsumingServiceIndexSpecified {
            get {
                return this.attributeConsumingServiceIndexFieldSpecified;
            }
            set {
                this.attributeConsumingServiceIndexFieldSpecified = value;
            }
        }
        
        [XmlAttribute()]
        public string ProviderName {
            get {
                return this.providerNameField;
            }
            set {
                this.providerNameField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("RequestedAuthnContext", Namespace = "urn:oasis:names:tc:SAML:2.0:protocol", IsNullable = false)]
    public partial class RequestedAuthnContextType
    {
        #region Fields
        private string[] itemsField;
        private ItemsChoiceRequestAuthnContext[] itemsElementNameField;
        private AuthnContextComparisonType comparisonField;
        private bool comparisonFieldSpecified;
        #endregion

        [XmlElement("AuthnContextClassRef", typeof(string), Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", DataType = "anyURI")]
        [XmlElement("AuthnContextDeclRef", typeof(string), Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", DataType = "anyURI")]
        [XmlChoiceIdentifier("ItemsElementName")]
        public string[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }

        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceRequestAuthnContext[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }

        [XmlAttribute()]
        public AuthnContextComparisonType Comparison {
            get {
                return this.comparisonField;
            }
            set {
                this.comparisonField = value;
            }
        }

        [XmlIgnore()]
        public bool ComparisonSpecified {
            get {
                return this.comparisonFieldSpecified;
            }
            set {
                this.comparisonFieldSpecified = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Assertion", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class AssertionType
    {
        #region Fields
        private NameIDType issuerField;
        private SignatureType signatureField;
        private SubjectType subjectField;
        private ConditionsType conditionsField;
        private AdviceType adviceField;
        private StatementAbstractType[] itemsField;
        private string versionField;
        private string idField;
        private DateTime issueInstantField;
        #endregion

        public NameIDType Issuer {
            get {
                return this.issuerField;
            }
            set {
                this.issuerField = value;
            }
        }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }

        public SubjectType Subject {
            get {
                return this.subjectField;
            }
            set {
                this.subjectField = value;
            }
        }

        public ConditionsType Conditions {
            get {
                return this.conditionsField;
            }
            set {
                this.conditionsField = value;
            }
        }

        public AdviceType Advice {
            get {
                return this.adviceField;
            }
            set {
                this.adviceField = value;
            }
        }

        [XmlElement("AttributeStatement", typeof(AttributeStatementType))]
        [XmlElement("AuthnStatement", typeof(AuthnStatementType))]
        [XmlElement("AuthzDecisionStatement", typeof(AuthzDecisionStatementType))]
        [XmlElement("Statement", typeof(StatementAbstractType))]
        public StatementAbstractType[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
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

        [XmlAttribute(DataType = "ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }

        [XmlAttribute()]
        public DateTime IssueInstant {
            get {
                return this.issueInstantField;
            }
            set {
                this.issueInstantField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("Response", Namespace = "urn:oasis:names:tc:SAML:2.0:protocol", IsNullable = false)]
    public partial class ResponseType : StatusResponseType
    {
        private object[] itemsField;

        [XmlElement("Assertion", typeof(AssertionType), Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
        [XmlElement("EncryptedAssertion", typeof(EncryptedElementType), Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("AttributeStatement", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class AttributeStatementType : StatementAbstractType
    {
        private object[] itemsField;

        [XmlElement("Attribute", typeof(AttributeType))]
        [XmlElement("EncryptedAttribute", typeof(EncryptedElementType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureType
    {
        #region Fields
        private SignedInfoType signedInfoField;
        private SignatureValueType signatureValueField;
        private KeyInfoType keyInfoField;
        private ObjectType[] objectField;
        private string idField;
        #endregion

        public SignedInfoType SignedInfo {
            get {
                return this.signedInfoField;
            }
            set {
                this.signedInfoField = value;
            }
        }

        public SignatureValueType SignatureValue {
            get {
                return this.signatureValueField;
            }
            set {
                this.signatureValueField = value;
            }
        }

        public KeyInfoType KeyInfo {
            get {
                return this.keyInfoField;
            }
            set {
                this.keyInfoField = value;
            }
        }

        [XmlElement("Object")]
        public ObjectType[] Object {
            get {
                return this.objectField;
            }
            set {
                this.objectField = value;
            }
        }

        [XmlAttribute(DataType = "ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("NameID", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class NameIDType
    {
        #region Fields
        private string nameQualifierField;
        private string sPNameQualifierField;
        private string formatField;
        private string sPProvidedIDField;
        private string valueField;
        #endregion

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

        [XmlAttribute(DataType = "anyURI")]
        public string Format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
            }
        }

        [XmlAttribute()]
        public string SPProvidedID {
            get {
                return this.sPProvidedIDField;
            }
            set {
                this.sPProvidedIDField = value;
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

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("Status", Namespace = "urn:oasis:names:tc:SAML:2.0:protocol", IsNullable = false)]
    public partial class StatusType
    {
        #region Fields
        private StatusCodeType statusCodeField;
        private string statusMessageField;
        private StatusDetailType statusDetailField;
        #endregion

        public StatusCodeType StatusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
            }
        }

        public string StatusMessage {
            get {
                return this.statusMessageField;
            }
            set {
                this.statusMessageField = value;
            }
        }

        public StatusDetailType StatusDetail {
            get {
                return this.statusDetailField;
            }
            set {
                this.statusDetailField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:protocol")]
    [XmlRoot("StatusCode", Namespace = "urn:oasis:names:tc:SAML:2.0:protocol", IsNullable = false)]
    public partial class StatusCodeType
    {
        #region Fields
        private StatusCodeType statusCodeField;
        private string valueField;
        #endregion

        public StatusCodeType StatusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
            }
        }

        [XmlAttribute(DataType = "anyURI")]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Conditions", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class ConditionsType
    {
        #region Fields
        private ConditionAbstractType[] itemsField;
        private DateTime notBeforeField;
        private bool notBeforeFieldSpecified;
        private DateTime notOnOrAfterField;
        private bool notOnOrAfterFieldSpecified;
        #endregion

        [XmlElement("AudienceRestriction", typeof(AudienceRestrictionType))]
        [XmlElement("Condition", typeof(ConditionAbstractType))]
        [XmlElement("OneTimeUse", typeof(OneTimeUseType))]
        [XmlElement("ProxyRestriction", typeof(ProxyRestrictionType))]
        public ConditionAbstractType[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }

        [XmlAttribute()]
        public DateTime NotBefore {
            get {
                return this.notBeforeField;
            }
            set {
                this.notBeforeField = value;
            }
        }

        [XmlIgnore()]
        public bool NotBeforeSpecified {
            get {
                return this.notBeforeFieldSpecified;
            }
            set {
                this.notBeforeFieldSpecified = value;
            }
        }

        [XmlAttribute()]
        public DateTime NotOnOrAfter {
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

    [XmlInclude(typeof(ProxyRestrictionType))]
    [XmlInclude(typeof(OneTimeUseType))]
    [XmlInclude(typeof(AudienceRestrictionType))]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Condition", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public abstract partial class ConditionAbstractType
    {
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("AudienceRestriction", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class AudienceRestrictionType : ConditionAbstractType
    {
        private string[] audienceField;

        [XmlElement("Audience", DataType = "anyURI")]
        public string[] Audience {
            get {
                return this.audienceField;
            }
            set {
                this.audienceField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("Subject", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class SubjectType
    {
        private object[] itemsField;

        [XmlElement("BaseID", typeof(BaseIDAbstractType))]
        [XmlElement("EncryptedID", typeof(EncryptedElementType))]
        [XmlElement("NameID", typeof(NameIDType))]
        [XmlElement("SubjectConfirmation", typeof(SubjectConfirmationType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("SubjectConfirmation", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class SubjectConfirmationType
    {
        #region Fields
        private object itemField;
        private SubjectConfirmationDataType subjectConfirmationDataField;
        private string methodField;
        #endregion

        [XmlElement("BaseID", typeof(BaseIDAbstractType))]
        [XmlElement("EncryptedID", typeof(EncryptedElementType))]
        [XmlElement("NameID", typeof(NameIDType))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }

        public SubjectConfirmationDataType SubjectConfirmationData {
            get {
                return this.subjectConfirmationDataField;
            }
            set {
                this.subjectConfirmationDataField = value;
            }
        }

        [XmlAttribute(DataType = "anyURI")]
        public string Method {
            get {
                return this.methodField;
            }
            set {
                this.methodField = value;
            }
        }
    }

    [XmlInclude(typeof(KeyInfoConfirmationDataType))]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("SubjectConfirmationData", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class SubjectConfirmationDataType
    {
        #region Fields
        private string recipient;
        private string[] textField;
        private DateTime notOnOrAfterField;
        #endregion

        [XmlAttribute(DataType = "anyURI")]
        public string Recipient {
            get {
                return this.recipient;
            }
            set {
                this.recipient = value;
            }
        }

        [XmlAttribute()]
        public DateTime NotOnOrAfter {
            get {
                return this.notOnOrAfterField;
            }
            set {
                this.notOnOrAfterField = value;
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

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("AuthnStatement", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class AuthnStatementType : StatementAbstractType
    {
        #region Fields
        private SubjectLocalityType subjectLocalityField;
        private AuthnContextType authnContextField;
        private DateTime authnInstantField;
        private string sessionIndexField;
        private DateTime sessionNotOnOrAfterField;
        private bool sessionNotOnOrAfterFieldSpecified;
        #endregion

        public SubjectLocalityType SubjectLocality {
            get {
                return this.subjectLocalityField;
            }
            set {
                this.subjectLocalityField = value;
            }
        }

        public AuthnContextType AuthnContext {
            get {
                return this.authnContextField;
            }
            set {
                this.authnContextField = value;
            }
        }

        [XmlAttribute()]
        public DateTime AuthnInstant {
            get {
                return this.authnInstantField;
            }
            set {
                this.authnInstantField = value;
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

        [XmlAttribute()]
        public DateTime SessionNotOnOrAfter {
            get {
                return this.sessionNotOnOrAfterField;
            }
            set {
                this.sessionNotOnOrAfterField = value;
            }
        }

        [XmlIgnore()]
        public bool SessionNotOnOrAfterSpecified {
            get {
                return this.sessionNotOnOrAfterFieldSpecified;
            }
            set {
                this.sessionNotOnOrAfterFieldSpecified = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion")]
    [XmlRoot("AuthnContext", Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IsNullable = false)]
    public partial class AuthnContextType
    {
        #region Fields
        private object[] itemsField;
        private ItemsChoiceAuthnContext[] itemsElementNameField;
        private string[] authenticatingAuthorityField;
        #endregion

        [XmlElement("AuthnContextClassRef", typeof(string), DataType = "anyURI")]
        [XmlElement("AuthnContextDecl", typeof(object))]
        [XmlElement("AuthnContextDeclRef", typeof(string), DataType = "anyURI")]
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
        public ItemsChoiceAuthnContext[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }

        [XmlElement("AuthenticatingAuthority", DataType = "anyURI")]
        public string[] AuthenticatingAuthority {
            get {
                return this.authenticatingAuthorityField;
            }
            set {
                this.authenticatingAuthorityField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyInfoType
    {
        #region Fields
        private object[] itemsField;
        private ItemsChoiceKeyInfo[] itemsElementNameField;
        private string[] textField;
        private string idField;
        #endregion

        [XmlAnyElement()]
        [XmlElement("KeyName", typeof(string))]
        [XmlElement("KeyValue", typeof(KeyValueType))]
        [XmlElement("MgmtData", typeof(string))]
        [XmlElement("PGPData", typeof(PGPDataType))]
        [XmlElement("RetrievalMethod", typeof(RetrievalMethodType))]
        [XmlElement("SPKIData", typeof(SPKIDataType))]
        [XmlElement("X509Data", typeof(X509DataType))]
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
        public ItemsChoiceKeyInfo[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
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

        [XmlAttribute(DataType = "ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignedInfoType
    {
        #region Fields
        private CanonicalizationMethodType canonicalizationMethodField;
        private SignatureMethodType signatureMethodField;
        private ReferenceSignedInfo[] referenceField;
        private string idField;
        #endregion

        public CanonicalizationMethodType CanonicalizationMethod {
            get {
                return this.canonicalizationMethodField;
            }
            set {
                this.canonicalizationMethodField = value;
            }
        }

        public SignatureMethodType SignatureMethod {
            get {
                return this.signatureMethodField;
            }
            set {
                this.signatureMethodField = value;
            }
        }

        [XmlElement("Reference")]
        public ReferenceSignedInfo[] Reference {
            get {
                return this.referenceField;
            }
            set {
                this.referenceField = value;
            }
        }

        [XmlAttribute(DataType = "ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class X509DataType
    {
        #region Fields
        private object[] itemsField;
        private ItemsChoiceSignatureData[] itemsElementNameField;
        #endregion

        [XmlAnyElement()]
        [XmlElement("X509CRL", typeof(byte[]), DataType = "base64Binary")]
        [XmlElement("X509Certificate", typeof(byte[]), DataType = "base64Binary")]
        [XmlElement("X509IssuerSerial", typeof(X509IssuerSerialType))]
        [XmlElement("X509SKI", typeof(byte[]), DataType = "base64Binary")]
        [XmlElement("X509SubjectName", typeof(string))]
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
        public ItemsChoiceSignatureData[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class CanonicalizationMethodType
    {
        #region Fields
        private XmlNode[] anyField;
        private string algorithmField;
        #endregion

        [XmlText()]
        [XmlAnyElement()]
        public XmlNode[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm {
            get {
                return this.algorithmField;
            }
            set {
                this.algorithmField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DigestMethodType
    {
        #region Fields
        private XmlNode[] anyField;
        private string algorithmField;
        #endregion

        [XmlText()]
        [XmlAnyElement()]
        public XmlNode[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm {
            get {
                return this.algorithmField;
            }
            set {
                this.algorithmField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    public partial class TransformsType
    {
        private TransformType[] transformField;

        [XmlElement("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public TransformType[] Transform {
            get {
                return this.transformField;
            }
            set {
                this.transformField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformType
    {
        #region Fields
        private object[] itemsField;
        private string[] textField;
        private string algorithmField;
        #endregion

        [XmlAnyElement()]
        [XmlElement("XPath", typeof(string))]
        public object[] Items {
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

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm {
            get {
                return this.algorithmField;
            }
            set {
                this.algorithmField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureMethodType
    {
        #region Fields
        private string hMACOutputLengthField;
        private XmlNode[] anyField;
        private string algorithmField;
        #endregion

        [XmlElement(DataType = "integer")]
        public string HMACOutputLength {
            get {
                return this.hMACOutputLengthField;
            }
            set {
                this.hMACOutputLengthField = value;
            }
        }

        [XmlText()]
        [XmlAnyElement()]
        public XmlNode[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm {
            get {
                return this.algorithmField;
            }
            set {
                this.algorithmField = value;
            }
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    public partial class ReferenceType
    {
        #region Fields
        private XmlElement[] anyField;
        private string uRIField;
        #endregion

        [XmlAnyElement()]
        public XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }

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

    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [XmlType(TypeName = "ReferenceType", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [XmlRoot("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ReferenceSignedInfo
    {
        #region Fields
        private TransformType[] transformsField;
        private DigestMethodType digestMethodField;
        private byte[] digestValueField;
        private string idField;
        private string uRIField;
        private string typeField;
        #endregion

        [XmlArrayItem("Transform", IsNullable = false)]
        public TransformType[] Transforms {
            get {
                return this.transformsField;
            }
            set {
                this.transformsField = value;
            }
        }

        public DigestMethodType DigestMethod {
            get {
                return this.digestMethodField;
            }
            set {
                this.digestMethodField = value;
            }
        }

        [XmlElement(DataType = "base64Binary")]
        public byte[] DigestValue {
            get {
                return this.digestValueField;
            }
            set {
                this.digestValueField = value;
            }
        }

        [XmlAttribute(DataType = "ID")]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }

        [XmlAttribute(DataType = "anyURI")]
        public string URI {
            get {
                return this.uRIField;
            }
            set {
                this.uRIField = value;
            }
        }

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
    #endregion

    #region SAML Serialization Enumerations  
    [Serializable()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:assertion", IncludeInSchema = false)]
    public enum ItemsChoiceAuthnContext
    {
        AuthnContextClassRef,
        AuthnContextDecl,
        AuthnContextDeclRef,
    }

    [Serializable()]
    [XmlType(Namespace = "urn:oasis:names:tc:SAML:2.0:protocol", IncludeInSchema = false)]
    public enum ItemsChoiceRequestAuthnContext
    {
        [XmlEnum("urn:oasis:names:tc:SAML:2.0:assertion:AuthnContextClassRef")]
        AuthnContextClassRef,

        [XmlEnum("urn:oasis:names:tc:SAML:2.0:assertion:AuthnContextDeclRef")]
        AuthnContextDeclRef,
    }

    [Serializable()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceSignatureData
    {
        [XmlEnum("##any:")]
        Item,
        X509CRL,
        X509Certificate,
        X509IssuerSerial,
        X509SKI,
        X509SubjectName,
    }

    [Serializable()]
    [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceKeyInfo
    {
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
    #endregion

    #region SAML Helpers...
    public static class Helper
    {
        public static XmlDocument SerializeAndSignSAMLType<T>(object samlType, string uriReference) {
            XmlElement xmlElement = null;
            XmlDocument xmlDocument = SerializeSAMLTypeToXmlDocument(samlType);
            SignedXml signedXml = SignedXMLElement(xmlDocument.DocumentElement, "#" + uriReference);

            //C# 7 switch statement, type matching; will not compile in VS 2015
            switch (samlType) {
                case ResponseType responseType:
                    xmlElement = xmlDocument.DocumentElement[Names.SAMLNamesElementIssuer, Names.SAMLNamesNamespaceAssertion];
                    break;
                case AuthnRequestType authnRequestType:
                    xmlElement = xmlDocument.DocumentElement[Names.SAMLNamesElementIssuer, Names.SAMLNamesNamespaceAssertion];
                    break;
                case EntityDescriptorType entityDescriptorType:
                    //If xmlElement is null, signature InsertAfter root element; for EntityDescriptor the Document Element is signed
                    break;
            }

            return xmlDocument.DocumentElement.InsertAfter(signedXml.GetXml(), xmlElement).OwnerDocument;
        }
        public static XmlDocument SerializeSAMLTypeToXmlDocument<T>(T samlType) where T : class {
            XmlDocument xmlDocument = new XmlDocument { PreserveWhitespace = Names.SAMLPreserveXmlWhitespace };
            XmlWriterSettings xmlSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, Encoding = Encoding.UTF8 };

            //Pattern complies with Microsoft Usage Code Analysis CA2202:DoNotDisposeObjectsMultipleTimes; NO nested using statements
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
        public static SignedXml SignedXMLElement(XmlElement xmlElementToSign, string uriReference) {
            Reference reference = new Reference { Uri = uriReference };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigExcC14NTransform());

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(SigningCert));

            SignedXml signedXML = new SignedXml(xmlElementToSign) { SigningKey = SigningCert.PrivateKey };
            signedXML.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            signedXML.AddReference(reference);
            signedXML.KeyInfo = keyInfo;
            signedXML.ComputeSignature();

            return signedXML;
        }
        public static XmlElement SelectXMLElement(XmlDocument doc, string ElementName, string Namespace) {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace(Names.SAMLNamesSaml, Namespace);
            return doc.DocumentElement.SelectSingleNode(ElementName, namespaceManager) as XmlElement;
        }
        public static bool VerifySignedXMLElement(XmlElement xmlElementToVerify, XmlElement xmlSignature) {
            bool isVerified = false;

            SignedXml signedXml = new SignedXml(xmlElementToVerify);
            signedXml.LoadXml(xmlSignature); //SIGNATURES MUST HAVE KeyInfo Clause
            isVerified = signedXml.CheckSignature();

            return isVerified;
        }

        //This is the Hosting Configuration; either App.Config or Web.Config of the PTS.SAML Services
        private static SecureString password {
            get {
                SecureString pwd = new SecureString();
                foreach (char chr in ("password").ToCharArray()) pwd.AppendChar(chr);
                return pwd;
            }
        }
        public static X509Certificate2 SigningCert {
            get {
                string APPKEY = Config.GetAppSettingsValue(Names.AppSetting_ServiceSigningCertificatePath);
                return new X509Certificate2(APPKEY, password); //key size must be 2048
                //The caller must trap any errors from this static property
            }
        }
        public static X509Certificate2 EncryptingCert {
            get {
                string APPKEY = Config.GetAppSettingsValue(Names.AppSetting_ServiceEncryptingCertificatePath);
                return new X509Certificate2(APPKEY, password); //key size must be 2048
                //The caller must trap any errors from this static property
            }
        }
        public static short SAMLIDPEntityDescriptorExpirationDays {
            get {
                short value = 0;
                string APPKEY = Config.GetAppSettingsValue(Names.AppSetting_SAMLIDPEntityDescriptorExpirationDays);

                return short.TryParse(APPKEY, out value) ? value : Names.SAMLDefaultIDPEntityDescriptorExpirationDays;
            }
        }
        public static short SAMLSPEntityDescriptorExpirationDays {
            get {
                short value = 0;
                string APPKEY = Config.GetAppSettingsValue(Names.AppSetting_SAMLSPEntityDescriptorExpirationDays);

                return short.TryParse(APPKEY, out value) ? value : Names.SAMLDefaultSPEntityDescriptorExpirationDays;
            }
        }
    }
    #endregion

    #region SAML Names...
    public static class Names
    {
        public static short SAMLAssertionExpirationMinutes = 5;
        public static short SAMLDefaultSPEntityDescriptorExpirationDays = 5;
        public static short SAMLDefaultIDPEntityDescriptorExpirationDays = 5;

        public static bool SAMLPreserveXmlWhitespace = true;

        public static string SAMLMessageDefaultIssuer = "Pacific Technology Solutions (PTS)";

        public static string SAMLVersion = "2.0";
        public static string SAMLNamesStatusFailed = "urn:oasis:names:tc:SAML:2.0:status:AuthnFailed";
        public static string SAMLNamesStatusSuccess = "urn:oasis:names:tc:SAML:2.0:status:Success";
        public static string SAMLNamesStatusErrorRequestor = "urn:oasis:names:tc:SAML:2.0:status:Requester";
        public static string SAMLNamesStatusErrorResponder = "urn:oasis:names:tc:SAML:2.0:status:Responder";
        public static string SAMLNamesStatusUnknownPrincipal = "urn:oasis:names:tc:SAML:2.0:status:UnknownPrincipal";
        public static string SAMLNamesStatusNoAuthnContext = "urn:oasis:names:tc:SAML:2.0:status:NoAuthnContext";
        public static string SAMLNamesProtocolBindingRedirect = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect";
        public static string SAMLNamesProtocolBindingPOST = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST";
        public static string SAMLNamesProtocolBindingsSOAP = "urn:oasis:names:tc:SAML:2.0:bindings:SOAP";
        public static string SAMLNamesProtocolStatusSuccess = "urn:oasis:names:tc:SAML:2.0:status:Success";
        public static string SAMLNamesProtocolStatusRequestDenied = "urn:oasis:names:tc:SAML:2.0:status:RequestDenied";
        public static string SAMLNamesFormatBasic = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
        public static string SAMLNamesFormatIssuerEntity = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
        public static string SAMLNamesFormatUnspecified = "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified";
        public static string SAMLNamesContextClassUnspecified = "urn:oasis:names:tc:SAML:2.0:ac:classes:unspecified";
        public static string SAMLNamesContextClassPassword = "urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport";
        public static string SAMLNamesSubjectConfirmationBaerer = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
        public static string SAMLNamesSaml = "saml";
        public static string SAMLNamesElementIssuer = "Issuer";
        public static string SAMLNamesElementSubject = "Subject";
        public static string SAMLNamesElementAssertion = "saml:Assertion";
        public static string SAMLNamesElementAssertionSignature = "saml:Signature";
        public static string SAMLNamesNamespaceAssertion = "urn:oasis:names:tc:SAML:2.0:assertion";
        public static string SAMLNamesNamespaceMetadata = "urn:oasis:names:tc:SAML:2.0:metadata";
        public static string SAMLNamesNamespaceProtocol = "urn:oasis:names:tc:SAML:2.0:protocol";

        public static string AppSetting_ServiceSigningCertificatePath = "ServiceSigningCertificatePath";
        public static string AppSetting_ServiceEncryptingCertificatePath = "ServiceEncryptingCertificatePath";
        public static string AppSetting_SAMLSPEntityDescriptorExpirationDays = "defaultSPMetataExpiresDays";
        public static string AppSetting_SAMLIDPEntityDescriptorExpirationDays = "defaultIDPMetataExpiresDays";
    }
    #endregion
}