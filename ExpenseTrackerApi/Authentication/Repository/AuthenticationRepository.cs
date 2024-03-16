using ExpenseTrackerApi.Model;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ExpenseTrackerApi.Authentication.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        public AuthenticationRepository(IConfiguration configuration)
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

           public async Task<UserLogin?> UserLogInAsync(string UserName, string PassWord)
        {
            try
            {
                var sql = "SELECT * FROM dbo.RegistrationTable WHERE username = @UserName";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<UserLogin>(sql, new { UserName });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
