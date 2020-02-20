using Dapper;
using dotNetCore31.DataAccess.Infrastructure.Helpers.Connection;
using dotNetCore31.DataAccess.IRepositories;
using dotNetCore31.DataAccess.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNetCore31.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConnectionHelper _connectionHelper;

        public CustomerRepository(IConnectionHelper connectionHelper)
        {
            this._connectionHelper = connectionHelper;
        }

        /// <summary>
        /// 取得客戶清單
        /// </summary>
        /// <param name="customerIds">客戶編號(多筆)</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomersDataModel>> GetCustomerListAsync(IEnumerable<string> customerIds)
        {
            using (var conn = this._connectionHelper.GetNorthwindConnection())
            {
                var result = await conn.QueryAsync<CustomersDataModel>
                (
                    this.GetCustomerListAsyncSQL(),
                    new { customerIds = customerIds }
                );

                return result;
            }
        }

        /// <summary>
        /// 取得客戶清單 SQL
        /// </summary>
        /// <returns></returns>
        private string GetCustomerListAsyncSQL()
        {
            return @"SELECT
                     [CustomerID],
                     [CompanyName],
	                 [ContactName],
	                 [ContactTitle],
	                 [Address],
	                 [City],
	                 [Region],
	                 [PostalCode],
	                 [Country],
	                 [NUM-3] AS [NUM_3],
	                 [ALPHA-2] AS [ALPHA_2],
	                 [ALPHA-3] AS [ALPHA_3],
	                 [Phone],
	                 [Fax]
                     FROM [dbo].[Customers]
                     WHERE [CustomerID] IN @customerIds";
        }
    }
}