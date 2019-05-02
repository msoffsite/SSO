using System;
using System.Collections.Generic;
using System.Diagnostics;
using SSOService.Models;

namespace SSOService.Data
{
    public interface IPtsEndpoint : IDisposable
    {
        Endpoint GetEndpoint(Endpoint endpoint);
        List<Endpoint> GetEndpoints(); 
    }

    public class PtsEndpoint : IPtsEndpoint
    {
        #region Variables...

        private EventLog LocalServiceLog { get; }
        private PtsEndpointMap SqlMapper { get; }

        private SqlService sqlService;

        #endregion

        private string SqlConnection { get; } = System.Configuration.ConfigurationManager.AppSettings[Helpers.Constants.ConnectionName];

        public PtsEndpoint() {
            SqlMapper = new PtsEndpointMap();
            sqlService = new SqlService(SqlConnection);
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            LocalServiceLog = new EventLog {Source = Helpers.Constants.ServiceEventLog };
        }

        public Endpoint GetEndpoint(Endpoint endpoint) {
            bool serviceOk = false;
            string sqlResponse = string.Empty;

            try {
                SqlMapper.GetEndpointMapParameters(endpoint, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    string sqlRequest = sqlService.SqlParameters[PtsLoginMap.Names.SqlMessage].DbValue.ToString();
                    sqlResponse = sqlService.SqlParameters[PtsLoginMap.Names.SqlMessage].DbOutput;
                    if (sqlRequest == sqlResponse) {
                        endpoint = SqlMapper.GetEndpointMapData(dataSet);
                        serviceOk = true;
                    }
                }
            }
            catch (Exception ex) {
                serviceOk = false;
                sqlResponse = $"{sqlResponse} {ex.Message}";
                LocalServiceLog.WriteEntry($"{sqlService.SqlStatusMessage} {sqlResponse}", EventLogEntryType.FailureAudit);
            }
            finally {
                if (serviceOk == false)
                    throw new Exception($"{sqlService.SqlStatusMessage} {sqlResponse}");
            }

            return endpoint;
        }

        public List<Endpoint> GetEndpoints() {
            bool serviceOk = false;
            string sqlResponse = string.Empty;
            List<Endpoint> endpoints = new List<Endpoint>();

            try {
                SqlMapper.GetEndpointsMapParameters(ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    string sqlRequest = sqlService.SqlParameters[PtsLoginMap.Names.SqlMessage].DbValue.ToString();
                    sqlResponse = sqlService.SqlParameters[PtsLoginMap.Names.SqlMessage].DbOutput;
                    if (sqlRequest == sqlResponse) {
                        endpoints = SqlMapper.GetEndpointsMapData(dataSet);
                        serviceOk = true;
                    }
                }
            }
            catch (Exception ex) {
                serviceOk = false;
                sqlResponse = $"{sqlResponse} {ex.Message}";
                LocalServiceLog.WriteEntry($"{sqlService.SqlStatusMessage} {sqlResponse}", EventLogEntryType.FailureAudit);
            }
            finally {
                if (serviceOk == false)
                    throw new Exception($"{sqlService.SqlStatusMessage} {sqlResponse}");
            }

            return endpoints;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing) {
            if (!disposing) return;
            LocalServiceLog.Close();
            if (sqlService == null) return;
            if (!sqlService.ExecuteCloseConnection())
                LocalServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }
    }
}
