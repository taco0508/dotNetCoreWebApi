using Microsoft.Extensions.Configuration;

namespace dotNetCore31.DataAccess.Infrastructure.Helpers.Connection
{
    public class ConnectionStringHelper : IConnectionStringHelper
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// 從appsettings.json的ConnectionStrings抓連線字串
        /// </summary>
        /// <param name="dbName">appsettings的ConnectionStrings的dbName</param>
        /// <returns></returns>
        public string GetConnectionString(string dbName)
        {
            return this._configuration.GetConnectionString(dbName);
        }
    }
}