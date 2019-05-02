using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using SSOService.Models;

namespace SSOService.Data
{
    internal class PtsEndpointMap
    {
        public void GetEndpointMapParameters(Endpoint endpoint, ref SqlService service) {
            //Map procedure...
            service.SqlProcedure = Names.SqlCommandAdministration;
            string sqlMessage = Names.SqlMessageGetEndpoint;

            //Map Endpoint to SQL DTO Table type...
            DataTable dtoDataTable = EndpointSqlDto.DataTable(endpoint);

            //Map parameters...
            SqlServiceParameter[] parameters = {
                new SqlServiceParameter(Names.ParameterDtoEndpoint, SqlDbType.Structured, Names.SqlParameterDtoTypeEndpoint, ParameterDirection.Input, dtoDataTable),
                new SqlServiceParameter(Names.SqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, sqlMessage)
            }; service.SqlParameters.List = parameters;
        }
        public Endpoint GetEndpointMapData(DataSet dataSet) {
            DataTable dataTableClaims = dataSet.Tables[Names.DataSetTableClaims];
            DataTable dataTableEndpoint = dataSet.Tables[Names.DataSetTableEndpoints];

            //Map claims...
            var queryClaims = dataTableClaims.AsEnumerable()
                .Select(claim => new {
                    Id = claim.Field<string>(Names.MapClaimId),
                    Name = claim.Field<string>(Names.MapClaimName),
                    FriendlyName = claim.Field<string>(Names.MapClaimFriendlyName),
                    FormatId = claim.Field<int>(Names.MapClaimFormatId),
                    FormatFriendlyName = claim.Field<string>(Names.MapClaimFormatFriendlyName),
                    FormatName = claim.Field<string>(Names.MapClaimFormatName),
                    Created = DateTime.Parse(claim.Field<string>(Names.MapClaimCreated)),
                    Edited = DateTime.Parse(claim.Field<string>(Names.MapClaimEdited)),
                    Unique = bool.Parse(claim.Field<string>(Names.MapClaimUnique)),
                    Required = bool.Parse(claim.Field<string>(Names.MapClaimRequired)),
                    Singular = bool.Parse(claim.Field<string>(Names.MapClaimSingular)),
                    Active = bool.Parse(claim.Field<string>(Names.MapClaimActive))
                }).Distinct();

            //Map Endpoint...
            Endpoint endpoint = new Endpoint
            {
                Authenticated = true,
                Id = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointId),
                ApplicationId = dataTableEndpoint.Rows[Names.DataSingleRow].Field<Guid>(Names.MapApplicationId).ToString(),
                Provider = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointProvider),
                Referrer = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointReferrer),
                Requestor = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointRequestor),
                Responder = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointResponder),
                Login = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointLogin),
                Logout = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointLogout),
                Organization = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointOrganization),
                Contact = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointContact),
                Description = dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointDescription),
                Created = DateTime.Parse(dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointCreated)),
                Edited = DateTime.Parse(dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointEdited)),
                Active = bool.Parse(dataTableEndpoint.Rows[Names.DataSingleRow].Field<string>(Names.MapEndpointActive)),
                Claims = queryClaims.Select(c => new EndpointClaim
                {
                    Id = c.Id, Name = c.Name, FriendlyName = c.FriendlyName, Format = c.FormatName, Required = c.Required, Singular = c.Singular, Unique = c.Unique
                }).ToList()
            };

            return endpoint;
        }

        public void GetEndpointsMapParameters(ref SqlService service) {
            //Map procedure...
            service.SqlProcedure = Names.SqlCommandAdministration;
            string sqlMessage = Names.SqlMessageGetEndpoints;

            //Map parameters...
            SqlServiceParameter[] parameters = {
                new SqlServiceParameter(Names.SqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, sqlMessage)
            }; service.SqlParameters.List = parameters;
        }
        public List<Endpoint> GetEndpointsMapData(DataSet dataSet) {
            DataTable dataTableEndpoints = dataSet.Tables[Names.DataSetTableEndpoints];
            DataTable dataTableClaims = dataSet.Tables[Names.DataSetTableClaims];
            List<Endpoint> listEndpoints = new List<Endpoint>();

            //Query Endpoints...
            var endpoints = dataTableEndpoints.AsEnumerable()
                .Select(endpoint => new {
                    EndpointId = endpoint.Field<string>(Names.MapEndpointId),
                    ApplicationId = endpoint.Field<Guid>(Names.MapApplicationId).ToString(),
                    Provider = endpoint.Field<string>(Names.MapEndpointProvider),
                    Referrer = endpoint.Field<string>(Names.MapEndpointReferrer),
                    Requestor = endpoint.Field<string>(Names.MapEndpointRequestor),
                    Responder = endpoint.Field<string>(Names.MapEndpointResponder),
                    Login = endpoint.Field<string>(Names.MapEndpointLogin),
                    Logout = endpoint.Field<string>(Names.MapEndpointLogout),
                    Organization = endpoint.Field<string>(Names.MapEndpointOrganization),
                    Contact = endpoint.Field<string>(Names.MapEndpointContact),
                    Description = endpoint.Field<string>(Names.MapEndpointDescription),
                    Created = DateTime.Parse(endpoint.Field<string>(Names.MapEndpointCreated)),
                    Edited = DateTime.Parse(endpoint.Field<string>(Names.MapEndpointEdited)),
                    Active = bool.Parse(endpoint.Field<string>(Names.MapEndpointActive)),
                }).Distinct();

            //Map Endpoints
            foreach (var endpoint in endpoints) {
                //Query endpoint claims
                var queryClaims = dataTableClaims.AsEnumerable()
                    .Select(claim => new {
                        EndpointId = claim.Field<string>(Names.MapEndpointId),
                        ClaimId = claim.Field<string>(Names.MapClaimId),
                        NameId = claim.Field<int>(Names.MapClaimNameId),
                        FriendlyName = claim.Field<string>(Names.MapClaimFriendlyName),
                        Name = claim.Field<string>(Names.MapClaimName),
                        FormatId = claim.Field<int>(Names.MapClaimFormatId),
                        FormatFriendlyName = claim.Field<string>(Names.MapClaimFormatFriendlyName),
                        FormatName = claim.Field<string>(Names.MapClaimFormatName),
                        Created = DateTime.Parse(claim.Field<string>(Names.MapClaimCreated)),
                        Edited = DateTime.Parse(claim.Field<string>(Names.MapClaimEdited)),
                        Unique = bool.Parse(claim.Field<string>(Names.MapClaimUnique)),
                        Required = bool.Parse(claim.Field<string>(Names.MapClaimRequired)),
                        Singular = bool.Parse(claim.Field<string>(Names.MapClaimSingular)),
                        Active = bool.Parse(claim.Field<string>(Names.MapClaimActive))
                    }).Where(c => c.EndpointId == endpoint.EndpointId).Distinct();

                listEndpoints.Add(new Endpoint
                {
                    ApplicationId = endpoint.ApplicationId, Id = endpoint.EndpointId, Provider = endpoint.Provider, Referrer = endpoint.Referrer, Requestor = endpoint.Requestor, Responder = endpoint.Responder,
                    Login = endpoint.Login, Logout = endpoint.Logout, Created = endpoint.Created, Edited = endpoint.Edited, Active = endpoint.Active,
                    Description = endpoint.Description, Organization = endpoint.Organization, Contact = endpoint.Contact,
                    Claims = queryClaims.Select(c => new EndpointClaim
                    {
                        Id = c.ClaimId, NameId = c.NameId, Name = c.Name, FriendlyName = c.FriendlyName, FormatId = c.FormatId, Format = c.FormatName, FormatFriendlyName = c.FormatFriendlyName,
                        Required = c.Required, Singular = c.Singular, Unique = c.Unique, Active = c.Active, Created = c.Created, Edited = c.Edited }).ToList()
                });                
            }

            return listEndpoints;
        }

        #region Enumeration map...
        //Database names...
        internal static class Names
        {
            //SQL commands...
            internal static string SqlCommandAdministration { get; } = "dbo.ssoAdministration";
            internal static string SqlParameterDtoTypeEndpoint { get; } = "dbo.dtoTypeEndpoint";

            //SQL Command Messages...
            internal static string SqlMessageGetEndpoints { get; } = "SelectEndpoints";
            internal static string SqlMessageGetEndpoint { get; } = "SelectEndpoint";
            internal static string SqlMessageSaveEndpoint { get; } = "SaveEndpoint";

            //SQL Command Parameters...
            internal static string SqlMessage { get; } = "message";
            internal static string ParameterDtoEndpoint { get; } = "endpoint";

            //SQL Data Map Parameters...
            internal static string MapEndpointId { get; } = "endpointId";
            internal static string MapApplicationId { get; } = "applicationId";
            internal static string MapEndpointProvider { get; } = "endpointProvider";
            internal static string MapEndpointReferrer { get; } = "endpointReferrer";
            internal static string MapEndpointRequestor { get; } = "endpointRequestor";
            internal static string MapEndpointResponder { get; } = "endpointResponder";
            internal static string MapEndpointLogin { get; } = "endpointLogin";
            internal static string MapEndpointLogout { get; } = "endpointLogout";
            internal static string MapEndpointOrganization { get; } = "endpointOrganization";
            internal static string MapEndpointContact { get; } = "endpointContact";
            internal static string MapEndpointDescription { get; } = "endpointDescription";
            internal static string MapEndpointCreated { get; } = "endpointCreated";
            internal static string MapEndpointEdited { get; } = "endpointEdited";
            internal static string MapEndpointActive { get; } = "endpointActive";

            internal static string MapClaimId { get; } = "endpointClaim";
            internal static string MapClaimName { get; } = "claimName";
            internal static string MapClaimFriendlyName { get; } = "claimFriendlyName";
            internal static string MapClaimNameId { get; } = "claimNameId";
            internal static string MapClaimFormatFriendlyName { get; } = "claimFormatFriendlyName";
            internal static string MapClaimFormatName { get; } = "claimFormatName";
            internal static string MapClaimFormatId { get; } = "claimFormatId";
            internal static string MapClaimCreated { get; } = "claimCreated";
            internal static string MapClaimEdited { get; } = "claimEdited";
            internal static string MapClaimUnique { get; } = "claimUnique";
            internal static string MapClaimRequired { get; } = "claimRequired";
            internal static string MapClaimSingular { get; } = "claimSingular";
            internal static string MapClaimActive { get; } = "claimActive";

            //Data set tables
            internal static short DataSetTableEndpoints { get; } = 0;
            internal static short DataSetTableClaims { get; } = 1;
            internal static short DataSingleRow { get; } = 0;
        }
        #endregion

    }
}
