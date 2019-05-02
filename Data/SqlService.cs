using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

/*
-- ==============================================================
-- Author:			Tim Choate, Computer Systems Engineering, © 2010 - 2018
-- Last update:     02/23/2018
-- Last update:     04/06/2018 - Suppress Code Analysis messages CA1001, CA2100
-- Last update:     05/15/2018 - Add SqlServiceParameter option SqlDbType Structured, IEnumerable
-- Description:	Manage SQL Connections, Commands, Parameters, Data Tables, and Data Sets
-- ==============================================================
 */
namespace SSOService.Data
{

    [SuppressMessage("Microsoft.Design", "CA1001", Justification = "Caller releases connection resource")]
    public class SqlService
    {
        
        private SqlConnection Connection { get; }
        public bool SqlStatusOk { get; private set; }
        public bool SqlConnectionOk { get; }
        public string SqlStatusMessage { get; private set; }
        public string SqlProcedure { get; set; }

        internal Parameters SqlParameters = new Parameters();

        public SqlService(string connectionStringName) {
            SqlStatusOk = false;
            ConnectionStringSettingsCollection connections = System.Configuration.ConfigurationManager.ConnectionStrings;
            if (connections == null)
                SqlStatusMessage = "Services.sqlService, Invalid App.config: SQL Connections section missing or invalid";
            else {
                try {
                    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
                    if (connectionString == null)
                        SqlStatusMessage = "Services.sqlService, Invalid App.config: SQL Connection name " + connectionStringName + " missing or invalid";
                    else {
                        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString());
                        Connection.Open();
                        if (Connection.State == System.Data.ConnectionState.Open) { SqlConnectionOk = true; SqlStatusOk = true; }
                        SqlStatusMessage = "Services.sqlService initialized, connection status: " + Connection.State.ToString();
                    }
                }
                catch (ConfigurationException ex1) {
                    SqlStatusMessage = "Services.sqlService, Invalid configuration, " + ex1.Source + ", " + ex1.Message;
                }
                catch (SqlException ex2) {
                    SqlStatusMessage = "Services.sqlService, Invalid connection, " + ex2.Source + ", " + ex2.Message;
                }
                catch (Exception ex3) {
                    SqlStatusMessage = "Services.sqlService, " + ex3.Source + ", " + ex3.Message;
                }
            }
        }

        ~SqlService() {
            this.ExecuteCloseConnection();
        }

        public DataTable ExecuteReader() {
            if (Connection.State != ConnectionState.Open) return null;

            DataTable sqlDataTable = null;
            SqlStatusOk = false;

            try {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = BuildCommand(this.SqlParameters.List);
                sqlDataTable = new DataTable("reader");
                if (sqlAdapter.Fill(sqlDataTable) == 0) {
                    SqlStatusMessage = "Services.sqlService.ExecuteReader request returned zero records.";
                }
                for (int i = 0; i < this.SqlParameters.List.Length; i++) {
                    if (this.SqlParameters.List[i].DbDirection == ParameterDirection.InputOutput || this.SqlParameters.List[i].DbDirection == ParameterDirection.Output) {
                        this.SqlParameters.List[i].DbOutput = sqlAdapter.SelectCommand.Parameters[this.SqlParameters.List[i].DbName].Value.ToString();
                    }
                }
                SqlStatusOk = true;
            }
            catch (DataException dataException) {
                SqlStatusMessage = "Services.sqlService.ExecuteReader, Invalid data adapter, " + dataException.Source + ", " + dataException.Message;
            }
            catch (InvalidOperationException invalidOperationException) {
                SqlStatusMessage = "Services.sqlService.ExecuteReader, Invalid data table, " + invalidOperationException.Source + ", " + invalidOperationException.Message;
            }
            catch (Exception exception) {
                SqlStatusMessage = "Services.sqlService.ExecuteReader, " + exception.Source + ", " + exception.Message;
            }
            return sqlDataTable;
        }

        public DataSet ExecuteReaders() {
            SqlStatusOk = false;
            DataSet sqlDataSet = null;

            if (Connection.State == ConnectionState.Open) {
                try {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter
                    {
                        SelectCommand = BuildCommand(this.SqlParameters.List)
                    };
                    sqlDataSet = new DataSet("reader");
                    if (sqlAdapter.Fill(sqlDataSet) == 0) {
                        SqlStatusMessage = "Services.sqlService.ExecuteReaders request returned zero tables.";
                    }
                    for (int i = 0; i < this.SqlParameters.List.Length; i++) {
                        if (this.SqlParameters.List[i].DbDirection == ParameterDirection.InputOutput || this.SqlParameters.List[i].DbDirection == ParameterDirection.Output) {
                            this.SqlParameters.List[i].DbOutput = sqlAdapter.SelectCommand.Parameters[this.SqlParameters.List[i].DbName].Value.ToString();
                        }
                    }
                    SqlStatusOk = true;
                }
                catch (DataException ex1) {
                    SqlStatusMessage = "Services.sqlService.ExecuteReaders, Invalid data adapter, " + ex1.Source + ", " + ex1.Message;
                }
                catch (InvalidOperationException ex2) {
                    SqlStatusMessage = "Services.sqlService.ExecuteReaders, Invalid data tables, " + ex2.Source + ", " + ex2.Message;
                }
                catch (Exception ex3) {
                    SqlStatusMessage = "Services.sqlService.ExecuteReaders, " + ex3.Source + ", " + ex3.Message;
                }
            }
            return sqlDataSet;
        }

        public bool ExecuteNonQuery() {
            SqlStatusOk = false;

            if (Connection.State == ConnectionState.Open) {
                try {
                    SqlCommand sqlCommand = BuildCommand(this.SqlParameters.List);
                    sqlCommand.ExecuteNonQuery();
                    for (int i = 0; i < this.SqlParameters.List.Length; i++) {
                        if (this.SqlParameters.List[i].DbDirection == System.Data.ParameterDirection.InputOutput || this.SqlParameters.List[i].DbDirection == ParameterDirection.Output) {
                            this.SqlParameters.List[i].DbOutput = sqlCommand.Parameters[this.SqlParameters.List[i].DbName].Value.ToString();
                        }
                    }
                    SqlStatusOk = true;
                }
                catch (DataException dataException) {
                    SqlStatusMessage = "Services.sqlService.executeNonQuery, Invalid command, " + dataException.Source + ", " + dataException.Message;
                }
                catch (Exception exception) {
                    SqlStatusMessage = "Services.sqlService.executeNonQuery, " + exception.Source + ", " + exception.Message;
                }
            }
            return SqlStatusOk;
        }

        public bool ExecuteCloseConnection() {
            SqlStatusOk = false;
            try {
                if (Connection != null && Connection.State == ConnectionState.Open) Connection.Close();
                SqlStatusOk = true;
            }
            catch (Exception ex) {
                SqlStatusMessage = "Services.sqlService.executeCloseConnection, " + ex.Source + ", " + ex.Message;
            }

            return SqlStatusOk;
        }

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "SqlCommand Parameters are not from user input")]
        private SqlCommand BuildCommand(SqlServiceParameter[] parameters) {
            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = this.Connection, CommandText = this.SqlProcedure, CommandType = CommandType.StoredProcedure
            };

            if (parameters.Any()) {
                sqlCommand.Parameters.AddRange(
                    parameters.Select(parameter => new SqlParameter
                    {
                        ParameterName = parameter.DbName, SqlDbType = parameter.DbType, TypeName = parameter.DbTypeName,
                        Size = parameter.DbSize, Direction = parameter.DbDirection, Value = parameter.DbValue
                    }).ToArray());
            }
            return sqlCommand;
        }

        public class Parameters
        {
            public SqlServiceParameter[] List;

            public SqlServiceParameter this[string index] {
                get {
                    SqlServiceParameter parameter = new SqlServiceParameter();
                    foreach (SqlServiceParameter item in this.List) {
                        if (item.DbName != index) continue;
                        parameter = item;
                        break;
                    }
                    return parameter;
                }
            }
        }
    }

    public enum SqlCommandType { Select, Update, Insert, Delete, Execute };

    public struct SqlServiceParameter
    {
        public int DbSize;
        public string DbName, DbOutput, DbTypeName;
        public object DbValue;
        public SqlDbType DbType;
        public ParameterDirection DbDirection;

        public SqlServiceParameter(string name, SqlDbType type, int size, ParameterDirection direction, string value) {
            DbName = name; DbType = type; DbSize = size; DbDirection = direction; DbValue = value;
            DbOutput = string.Empty; DbTypeName = string.Empty;
        }

        public SqlServiceParameter(string name, SqlDbType type, string typeName, ParameterDirection direction, DataTable value) {
            DbName = name; DbType = type; DbDirection = direction; DbTypeName = typeName; DbValue = value;
            DbOutput = string.Empty; DbSize = 0;
        }
    }
}
