using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTrackerApi.Repository.Implementations
{
    public class ExpenseTrackerRepository:IExpenseTrackerRepository
    {
        public readonly IConfiguration _configuration;
        public ExpenseTrackerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<long> CreateUserAsync(UserModel userModel)
        {
            try
            {
                var sql = "INSERT INTO [dbo].[RegistrationTable] " +
                          "([UserName],[Password],[Email],[Role])" +
                          "VALUES" +
                          "(@UserName,@Password,@Email,@Role)";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, userModel); // return  0 or 1
                    return res;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<long> AddSpendingAsync(Categories categories)
        {
            try
            {
                var sql = "INSERT INTO [dbo].[Categories] " +
                          "([UserId],[Transport],[Food],[EatingOut],[House],[Cloths],[Communication])" +
                          "VALUES" +
                          "(@UserId,@Transport,@Food,@EatingOut,@House,@Cloths,@Communication)";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, categories); // return  0 or 1
                    return res;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<UserLogin?> UserLogInAsync(string UserName, string PassWord)
            {
                try
                {
                var sql = "SELECT * FROM dbo.RegistrationTable WHERE username = @UserName";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryAsync<UserLogin>(sql, new {UserName});
                        return result.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


        public async Task<long> GetTransportSum(DateTime fromDate, DateTime toDate)
        {
            int transportSum = 0;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
            {
                string query = "SELECT SUM(Transport) FROM Categories WHERE DateTimeColumn >= @FromDate AND DateTimeColumn <= @ToDate";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FromDate", fromDate);
                command.Parameters.AddWithValue("@ToDate", toDate);

                await connection.OpenAsync();

                object result = await command.ExecuteScalarAsync();

                if (result != DBNull.Value)
                {
                    transportSum = Convert.ToInt32(result);
                }
            }

            return transportSum;
        }
    }


     }
    
}
