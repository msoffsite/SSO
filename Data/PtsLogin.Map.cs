using System;
using System.Data;
using System.Linq;
using SSOService.Models;

namespace SSOService.Data
{
    public class PtsLoginMap
	{
        public void SelectLoginMapParameters(Credential login, ref SqlService service) {
            //Map procedure...
            service.SqlProcedure = Names.SqlCommandAuthentication;
            string sqlMessage = (login.Refresh) ? Names.SqlMessageRefreshCredential : Names.SqlMessageAuthenticate ;

            //Map login to SQL DTO Credential Table type...
            DataTable dtoDataTable = CredentialSqlDto.DataTable(login);

            //Map parameters...
            SqlServiceParameter[] parameters = {
                new SqlServiceParameter(Names.ParameterDtoCredential, SqlDbType.Structured, Names.SqlParameterDtoTypeCredential, ParameterDirection.Input, dtoDataTable),
                new SqlServiceParameter(Names.SqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, sqlMessage)
            }; service.SqlParameters.List = parameters;
        }

        public Credential SelectLoginMapData(DataSet dataSet) {
            DataTable dataTableCredential = dataSet.Tables[Names.DataSetTableCredential];
            DataTable dataTableRoles = dataSet.Tables[Names.DataSetTableCredentialRoles];
            DataTable dataTableClaims = dataSet.Tables[Names.DataSetTableCredentialClaims];

            //Map roles...
            var queryRoles = dataTableRoles.AsEnumerable()
                .Select(role => new {
                    RoleId = role.Field<Guid>(Names.MapCredentialRoleId).ToString(),
                    RoleName = role.Field<string>(Names.MapCredentialRoleName)
                });

            //Map claims...
            var queryClaims = dataTableClaims.AsEnumerable()
                .Select(claim => new {
                    CredentialId = claim.Field<string>(Names.MapCredentialId),
                    CredentialClaim = claim.Field<string>(Names.MapCredentialClaim),
                    ClaimName = claim.Field<string>(Names.MapClaimName),
                    ClaimFriendlyName = claim.Field<string>(Names.MapClaimFriendlyName),
                    ClaimFormat = claim.Field<string>(Names.MapClaimFormat),
                    ClaimRequired = bool.Parse(claim.Field<string>(Names.MapClaimRequired)),
                    ClaimSingular = bool.Parse(claim.Field<string>(Names.MapClaimSingular)),
                    ClaimUnique = bool.Parse(claim.Field<string>(Names.MapClaimUnique)),
                    ClaimSetEntityId = claim.Field<string>(Names.MapClaimSetEntityId),
                    ClaimSetDescription = claim.Field<string>(Names.MapClaimSetDescription),
                    ClaimSetOrder = claim.Field<short>(Names.MapClaimSetOrder)
                });

            //Map Credential...
            Credential login = new Credential {
                EntityId = dataTableCredential.Rows[Names.DataSingleRow].Field<string>(Names.MapCredentialId),
                ClaimsId = dataTableCredential.Rows[Names.DataSingleRow].Field<string>(Names.MapClaimSetEntityId),
                Email = dataTableCredential.Rows[Names.DataSingleRow].Field<string>(Names.MapCredentialEmail),
                Created = dataTableCredential.Rows[Names.DataSingleRow].Field<string>(Names.MapCredentialCreated),
                Edited = dataTableCredential.Rows[Names.DataSingleRow].Field<string>(Names.MapCredentialEdited),
                Active = bool.Parse(dataTableCredential.Rows[Names.DataSingleRow].Field<string>(Names.MapCredentialActive)),
                Roles = queryRoles.Select(role => new Role { RoleId = role.RoleId, Name = role.RoleName }).ToList(),
                Claims = queryClaims.Select(claim => new Claim
                {
                    Name = claim.ClaimName, FriendlyName = claim.ClaimFriendlyName, NameFormat = claim.ClaimFormat, Value = claim.CredentialClaim,
                    Required = claim.ClaimRequired, Unique = claim.ClaimUnique, ClaimSet = claim.ClaimSetDescription, EntityId = claim.ClaimSetEntityId }).ToList()
                };

            return login;
        }

        #region Enumeration map...
        //Database names...
        internal static class Names
        {
            //SQL commands...
            internal static string SqlCommandAuthentication { get; } = "dbo.ssoAuthenticate";
            internal static string SqlParameterDtoTypeCredential { get; } = "dbo.dtoTypeCredential";

            //SQL Command Messages...
            internal static string SqlMessageAuthenticate { get; } = "Authenticate";
            internal static string SqlMessageRefreshCredential { get; } = "RefreshCredential";

            //SQL Command Parameters...
            internal static string SqlMessage { get; } = "message";
            internal static string ParameterDtoCredential { get; } = "credential";

            //SQL Data Map Parameters...
            internal static string MapCredentialId { get; } = "credentialId";
            internal static string MapCredentialEmail { get; } = "credentialEmail";
            internal static string MapCredentialCreated { get; } = "credentialCreated";
            internal static string MapCredentialEdited { get; } = "credentialEdited";
            internal static string MapCredentialActive { get; } = "credentialActive";
            internal static string MapCredentialRoleId { get; } = "credentialRoleId";
            internal static string MapCredentialRoleName { get; } = "credentialRoleName";

            internal static string MapCredentialClaim { get; } = "credentialClaim";
            internal static string MapClaimName { get; } = "claimName";
            internal static string MapClaimFriendlyName { get; } = "claimFriendlyName";
            internal static string MapClaimFormat { get; } = "claimFormatName";
            internal static string MapClaimRequired { get; } = "claimRequired";
            internal static string MapClaimSingular { get; } = "claimSingular";
            internal static string MapClaimUnique { get; } = "claimUnique";
            internal static string MapClaimSetEntityId { get; } = "claimSetId";
            internal static string MapClaimSetDescription { get; } = "claimSetDescription";
            internal static string MapClaimSetOrder { get; } = "claimSetOrder";

            //Data set tables
            internal static short DataSetTableCredential { get; } = 0;
            internal static short DataSetTableCredentialRoles { get; } = 1;
            internal static short DataSetTableCredentialClaims { get; } = 2;
            internal static short DataSingleRow { get; } = 0;
        }
        #endregion
    }
}