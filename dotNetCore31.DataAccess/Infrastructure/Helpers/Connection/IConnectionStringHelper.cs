namespace dotNetCore31.DataAccess.Infrastructure.Helpers.Connection
{
    public interface IConnectionStringHelper
    {
        /// <summary>
        /// 從appsettings.json的ConnectionStrings抓連線字串
        /// </summary>
        /// <param name="dbName">appsettings的ConnectionStrings的dbName</param>
        /// <returns></returns>
        string GetConnectionString(string dbName);
    }
}