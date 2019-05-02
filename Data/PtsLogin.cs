using System;
using System.Diagnostics;
using SSOService.Models;

namespace SSOService.Data
{
    public interface IPtsLogin : IDisposable
    {
        Credential Authenticate(Credential session);
    }

    public class PtsLogin : IPtsLogin
    {
        #region Variables...

        private EventLog LocalServiceLog { get; }
        private PtsLoginMap SqlMapper { get; }

        private SqlService sqlService;
        #endregion

        private string SqlConnection => System.Configuration.ConfigurationManager.AppSettings[Helpers.Constants.ConnectionName];

        public PtsLogin() {
            SqlMapper = new PtsLoginMap();
            sqlService = new SqlService(this.SqlConnection);
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            LocalServiceLog = new System.Diagnostics.EventLog {Source = Helpers.Constants.ServiceEventLog };
        }

        public Credential Authenticate(Credential login) 
        {
            bool serviceOk = false;
            string sqlResponse = string.Empty;

#pragma warning disable 168
            System.Data.DataSet dataSet = sqlService.ExecuteReaders();
#pragma warning restore 168
            try {
                SqlMapper.SelectLoginMapParameters(login, ref sqlService);
                if (sqlService.SqlStatusOk) {
                    string sqlRequest = sqlService.SqlParameters[PtsLoginMap.Names.SqlMessage].DbValue.ToString();
                    sqlResponse = sqlService.SqlParameters[PtsLoginMap.Names.SqlMessage].DbOutput;
                    if (sqlRequest == sqlResponse) {
                        login = SqlMapper.SelectLoginMapData(dataSet);
                        login.Authenticated = login.Active;
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

            return login;
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
                this.LocalServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }
    }
}