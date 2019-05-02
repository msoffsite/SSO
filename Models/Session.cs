using System.Runtime.Serialization;

namespace SSOService.Models
{
    public class NullT { }

    [DataContract]
    public class Session<T> where T : class
    {
        [DataMember] public T Data { get; set; }
        [DataMember] public bool SessionOk { get; set; }
        [DataMember] public string ClientMessage { get; set; }
        [DataMember] public string ServerMessage { get; set; }
        [DataMember] public string SqlKey { get; set; }

        public Session<Tdata> Clone<Tdata>(Tdata data) where Tdata : class {

            return this.InitCommon(this, data);
        }

        public static Session<Tdata> Clone<Tdata>(Session<T> session, Tdata data = null) where Tdata : class {

            return session.InitCommon(session, data);
        }

        private Session<Tdata> InitCommon<Tdata>(Session<T> session, Tdata data) where Tdata : class {

            return new Session<Tdata> {
                SessionOk = session.SessionOk,
                ClientMessage = session.ClientMessage,
                ServerMessage = session.ServerMessage,
                SqlKey = session.SqlKey,
                Data = data
            };
        }
    }
}

