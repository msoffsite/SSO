using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace SSOService.Models
{
    [DataContract]
    public class Endpoint
    {
        [DataMember] public string Id { get; set; }
        [DataMember] public string ApplicationId { get; set; }
        [DataMember] public string Provider { get; set; }
        [DataMember] public string Referrer { get; set; }
        [DataMember] public string Requestor { get; set; }
        [DataMember] public string Responder { get; set; }
        [DataMember] public string Login { get; set; }
        [DataMember] public string Logout { get; set; }
        [DataMember] public string Organization { get; set; }
        [DataMember] public string Contact { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public DateTime Created { get; set; }
        [DataMember] public DateTime Edited { get; set; }
        [DataMember] public bool Active { get; set; }
        [DataMember] public bool Authenticated { get; set; }

        [DataMember]
        public List<EndpointClaim> Claims { get; set; }

        #region AppConfig/WebConfig Application keys...
        public static string ServiceRequestToken => System.Configuration.ConfigurationManager.AppSettings["ServiceRequestToken"];

        #endregion
    }

    [DataContract]
    public class EndpointClaim
    {
        [DataMember] public string Id { get; set; }
        [DataMember] public int NameId { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string FriendlyName { get; set; }
        [DataMember] public int FormatId { get; set; }
        [DataMember] public string Format { get; set; }
        [DataMember] public string FormatFriendlyName { get; set; }
        [DataMember] public DateTime Created { get; set; }
        [DataMember] public DateTime Edited { get; set; }
        [DataMember] public bool Required { get; set; }
        [DataMember] public bool Singular { get; set; }
        [DataMember] public bool Unique { get; set; }
        [DataMember] public bool Active { get; set; }
    }

    public class EndpointSqlDto
    {
        public string ApplicationId { get; set; }
        public string EntityId { get; set; }
        public string Provider { get; set; }
        public string Referrer { get; set; }
        public string Requestor { get; set; }
        public string Responder { get; set; }
        public string Login { get; set; }
        public string Logout { get; set; }
        public string Organization { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public short Post { get; set; }

        public static Endpoint Clone(EndpointSqlDto sqlDto) {
            DateTime now = DateTime.Now;
            Endpoint endpoint = new Endpoint
            {
                ApplicationId = sqlDto.ApplicationId, Id = sqlDto.EntityId,
                Provider = sqlDto.Provider, Referrer = sqlDto.Referrer, Requestor = sqlDto.Requestor, Responder = sqlDto.Responder,
                Login = sqlDto.Login, Logout = sqlDto.Logout, Organization = sqlDto.Organization, Contact = sqlDto.Contact,
                Description = sqlDto.Description, Created = now, Edited = now, Active = (sqlDto.Post > 0) ? true : false
            };
            return endpoint;
        }

        public static EndpointSqlDto Clone(Endpoint endpoint) {
            EndpointSqlDto sqlDto = new EndpointSqlDto
            {
                ApplicationId = endpoint.ApplicationId, EntityId = endpoint.Id,
                Provider = endpoint.Provider, Referrer = endpoint.Referrer, Requestor = endpoint.Requestor, Responder = endpoint.Responder,
                Login = endpoint.Login, Logout = endpoint.Logout, Organization = endpoint.Organization, Contact = endpoint.Contact,
                Description = endpoint.Description, Post = (endpoint.Active) ? (short)1 : (short)0
            };
            return sqlDto;
        }

        public static DataTable DataTable(Endpoint endpoint) {
            EndpointSqlDto dtoEndpoint = Clone(endpoint);
            DataTable dtoEndpointTable = new DataTable { TableName = endpoint.GetType().Name };
            List<System.Reflection.PropertyInfo> dtoProperties = new List<System.Reflection.PropertyInfo>(typeof(EndpointSqlDto).GetProperties());

            dtoEndpointTable.Columns.AddRange(dtoProperties.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            dtoEndpointTable.Rows.Add(dtoProperties.Select(p => dtoEndpoint.GetType().GetProperty(p.Name).GetValue(dtoEndpoint)).ToArray());

            return dtoEndpointTable;
        }
    }
}
