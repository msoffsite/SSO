using System;
using System.Diagnostics;
using System.Xml;
using SSOService.Models;

namespace SSOService.Data
{
    public interface IRequest : IDisposable
    {
        XmlDocument XmlSamlAuthnRequest(Endpoint endpoint);
    }

    public class Request : IRequest
    {
        #region Variables...

        private EventLog LocalServiceLog { get; }
        private RequestMap SqlMapper { get; }

        private SqlService sqlService;

        #endregion

        private string SqlConnection => System.Configuration.ConfigurationManager.AppSettings[Helpers.Constants.ConnectionName];

        public Request() {
            SqlMapper = new RequestMap();
            sqlService = new SqlService(SqlConnection);
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            LocalServiceLog = new EventLog {Source = Helpers.Constants.ServiceEventLog };
        }

        public XmlDocument XmlSamlAuthnRequest(Endpoint endpoint) {
            bool serviceOk = false;
            string sqlResponse = string.Empty;
            XmlDocument xmlDocument = new XmlDocument();

            try {
                SqlMapper.EndpointMapParameters(endpoint, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    string sqlRequest = sqlService.SqlParameters[RequestMap.Names.SqlMessage].DbValue.ToString();
                    sqlResponse = sqlService.SqlParameters[RequestMap.Names.SqlMessage].DbOutput;
                    if (sqlRequest == sqlResponse) {
                        endpoint = SqlMapper.EndpointMapData(dataSet);
                        xmlDocument = SqlMapper.EndpointMapSamlRequest(endpoint);
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

            return xmlDocument;
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
