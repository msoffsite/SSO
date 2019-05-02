using System.ServiceModel;
using System.Xml;
using SSOService.Models;

namespace SSOService.Contracts
{
    [ServiceContract]
    public interface IPtsAuthenticate
    {
        [OperationContract]
        Session<Credential> AuthenticateCredential(Session<Credential> session);

        [OperationContract]
        Session<XmlDocument> AuthenticateSamlRequest(Session<Endpoint> session);

        [OperationContract]
        Session<XmlDocument> AuthenticateSamlResponse(Session<Endpoint> session);
    }
}
