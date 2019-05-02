using System.Collections.Generic;
using System.ServiceModel;
using SSOService.Models;

namespace SSOService.Contracts
{
    [ServiceContract]
    public interface IAdministration
    {
        [OperationContract]
        //[WebInvoke (UriTemplate = "/Admin/Endpoint/{sKey}", Method = "GET", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml) ]
        Session<Endpoint> GetEndpoint(Session<Endpoint> session);

        [OperationContract]
        //[WebInvoke (UriTemplate = "/Admin/Endpoints/{sKey}", Method = "GET", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml) ]
        Session<List<Endpoint>> GetEndpoints(Session<NullT> session);

        //[OperationContract]
        //SSOService.Model.Session<List<SSOService.Model.Endpoint>> SaveEndpoint(Session<Endpoint> session);

        //[OperationContract]
        //SSOService.Model.Session<List<SSOService.Model.Endpoint>> GetEndpointClaims(Session<Endpoint> session);

        //[OperationContract]
        //SSOService.Model.Session<List<SSOService.Model.Endpoint>> SaveEndpointClaim(Session<Endpoint> session);
    }
}
