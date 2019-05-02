using System;
using System.ServiceModel;
using System.Xml;
using SSOService.Contracts;
using SSOService.Data;
using SSOService.Models;

namespace SSOService.Services
{
    public class PtsAuthenticate : IPtsAuthenticate
    {
        private IPtsLogin LoginRepository { get; }
        private IRequest RequestRepository { get; }
        private IResponse ResponseRepository { get; }

        public PtsAuthenticate() : this(new PtsLogin(), new Request(), new Response()) { }

        public PtsAuthenticate(IPtsLogin loginRepository, IRequest requestRepository, IResponse responseRepository)
        {
            this.LoginRepository = loginRepository;
            this.RequestRepository = requestRepository;
            this.ResponseRepository = responseRepository;
        }

        public Session<Credential> AuthenticateCredential(Session<Credential> session) 
        {
            try {
                if (session.SqlKey.Equals(Credential.ServiceRequestToken)) {
                    session.Data = LoginRepository.Authenticate(session.Data);
                    session.SessionOk = true;
                }
                LoginRepository.Dispose();
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }

            return session;
        }

        public Session<XmlDocument> AuthenticateSamlRequest(Session<Endpoint> sessionIn) {
            Session<XmlDocument> sessionOut = Session<Endpoint>.Clone<XmlDocument>(sessionIn);

            try {
                if (sessionIn.SqlKey.Equals(Credential.ServiceRequestToken)) {
                    sessionOut.Data = RequestRepository.XmlSamlAuthnRequest(sessionIn.Data);
                    sessionOut.SessionOk = true;
                }
                RequestRepository.Dispose();
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }

            return sessionOut;
        }

        public Session<XmlDocument> AuthenticateSamlResponse(Session<Endpoint> sessionIn) {
            Session<XmlDocument> sessionOut = Session<Endpoint>.Clone<XmlDocument>(sessionIn);

            try {
                if (sessionIn.SqlKey.Equals(Credential.ServiceRequestToken)) {
                    sessionOut.Data = ResponseRepository.XmlSamlAuthnResponse(sessionIn.Data);
                    sessionOut.SessionOk = true;
                }
                ResponseRepository.Dispose();
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }

            return sessionOut;
        }
    }
}
