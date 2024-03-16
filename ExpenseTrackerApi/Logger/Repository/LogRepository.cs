
using Dapper;
using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.Data.SqlClient;

namespace ExpenseTrackerApi.Logger.Repository
{
    public class LogRepository:ILogRepository
    {
        private readonly IConfiguration _configuration;
        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<long> CreateLog(Log log)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await con.OpenAsync();

                    var sql = @"INSERT INTO [dbo].[Log]
                        ([LogInformation]
                        ,[TimeStamp])
                        VALUES
                        (@LogInformation
                        ,@TimeStamp)";
                    var res = await con.ExecuteAsync(sql, log);
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
