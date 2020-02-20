using System.Data;

namespace dotNetCore31.DataAccess.Infrastructure.Helpers.Connection
{
    public interface IConnectionHelper
    {
        /// <summary>
        /// Get Northwind Connection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetNorthwindConnection();
    }
}