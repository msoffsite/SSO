using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace SSOService.Models
{
    [DataContract]
    public class Credential
    {
        [DataMember] public string Email { get; set; }
        [DataMember] public string EntityId { get; set; }
        [DataMember] public string ClaimsId { get; set; }
        [DataMember] public string Created { get; set; }
        [DataMember] public string Edited { get; set; }
        [DataMember] public bool Active { get; set; }
        [DataMember] public bool Authenticated { get; set; }
        [DataMember] public bool Refresh { get; set; }

        [DataMember] public List<Role> Roles { get; set; }
        [DataMember] public List<Claim> Claims { get; set; }

        #region AppConfig/WebConfig Application keys...
        public static string ServiceRequestToken => System.Configuration.ConfigurationManager.AppSettings["ServiceRequestToken"];

        #endregion
    }

    [DataContract]
    public class Role
    {
        [DataMember] public string RoleId { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Title { get; set; }
    }

    [DataContract]
    public class Claim
    {
        [DataMember] public string Value { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string FriendlyName { get; set; }
        [DataMember] public string NameFormat { get; set; }
        [DataMember] public string ClaimSet { get; set; }
        [DataMember] public string EntityId { get; set; }
        [DataMember] public bool Required { get; set; }
        [DataMember] public bool Unique { get; set; }
    }

    public class CredentialSqlDto
    {
        public string ApplicationId { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string ClaimSetId { get; set; }
        public string Provider { get; set; }
        public string Referrer { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Description { get; set; }
        public short Post { get; set; }

        public static Credential Clone(CredentialSqlDto sqlDto) {
            DateTime now = DateTime.Now;
            Credential credential = new Credential
            {
                Email = sqlDto.UserEmail, EntityId = sqlDto.UserId, ClaimsId = sqlDto.ClaimSetId
            };
            return credential;
        }

        public static CredentialSqlDto Clone(Credential credential) {
            DateTime now = DateTime.Now;
            CredentialSqlDto sqlDto = new CredentialSqlDto
            {
                UserEmail = credential.Email, UserId = Guid.Parse(credential.EntityId).ToString(), EntityId = credential.EntityId, ClaimSetId = credential.ClaimsId
            };
            return sqlDto;
        }

        public static DataTable DataTable(Credential credential) {
            CredentialSqlDto dtoCredential = Clone(credential);
            DataTable dtoCredentialTable = new DataTable { TableName = credential.GetType().Name };
            List<System.Reflection.PropertyInfo> dtoProperties = new List<System.Reflection.PropertyInfo>(typeof(CredentialSqlDto).GetProperties());

            dtoCredentialTable.Columns.AddRange(dtoProperties.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            dtoCredentialTable.Rows.Add(dtoProperties.Select(p => dtoCredential.GetType().GetProperty(p.Name).GetValue(dtoCredential)).ToArray());

            return dtoCredentialTable;
        }
    }

}
