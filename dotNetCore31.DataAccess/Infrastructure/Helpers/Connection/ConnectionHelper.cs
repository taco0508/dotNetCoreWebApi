﻿using CoreProfiler;
using CoreProfiler.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace dotNetCore31.DataAccess.Infrastructure.Helpers.Connection
{
    public class ConnectionHelper : IConnectionHelper
    {
        private readonly IConnectionStringHelper _connectionStringHelper;
        private readonly string _northwindDbName = "Northwind";

        public ConnectionHelper(IConnectionStringHelper connectionStringHelper)
        {
            this._connectionStringHelper = connectionStringHelper;
        }

        /// <summary>
        /// Get Northwind Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetNorthwindConnection()
        {
            return this.GetConnection(this._northwindDbName);
        }

        /// <summary>
        /// Get GetConnection
        /// </summary>
        /// <param name="dbName">Db Name</param>
        /// <returns></returns>
        private IDbConnection GetConnection(string dbName)
        {
            var connectionString = this._connectionStringHelper.GetConnectionString(dbName);
            var connection = new SqlConnection(connectionString);

            //使用CoreProfiller的connection，才能紀錄T-SQ與傳入參數
            var dbProfiler = new DbProfiler(ProfilingSession.Current.Profiler);
            return new ProfiledDbConnection(connection, dbProfiler);
        }
    }
}