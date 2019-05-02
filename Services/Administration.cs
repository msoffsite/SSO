using System;
using System.Collections.Generic;
using System.ServiceModel;
using SSOService.Contracts;
using SSOService.Data;
using SSOService.Models;

namespace SSOService.Services
{
    public class PtsAdministration : IAdministration
    {
        private readonly IPtsEndpoint endpointRepository;

        public PtsAdministration() : this(new PtsEndpoint()) { }

        public PtsAdministration(PtsEndpoint endpointRepository) {
            this.endpointRepository = endpointRepository;
        }

        public Session<Endpoint> GetEndpoint(Session<Endpoint> session) {
            try {
                if (session.SqlKey.Equals(Endpoint.ServiceRequestToken)) {
                    session.Data = endpointRepository.GetEndpoint(session.Data);
                    session.SessionOk = true;
                }
                endpointRepository.Dispose();
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }

            return session;
        }

        public Session<List<Endpoint>> GetEndpoints(Session<NullT> sessionIn) {
            Session<List<Endpoint>> sessionOut = Session<NullT>.Clone<List<Endpoint>>(sessionIn);

            try {
                if (sessionIn.SqlKey.Equals(Endpoint.ServiceRequestToken)) {
                    sessionOut.Data = endpointRepository.GetEndpoints();
                    sessionOut.SessionOk = true;
                }
                endpointRepository.Dispose();
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }

            return sessionOut;
        }
    }
}
