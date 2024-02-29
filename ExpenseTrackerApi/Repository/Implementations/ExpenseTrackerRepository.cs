using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

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
                    var res = await connection.ExecuteAsync(sql, userModel);
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
                    var sql = "SELECT * FROM dbo.RegistrationTable WHERE username = @UserName AND password = @PassWord";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryAsync<UserLogin>(sql, new { username = UserName, password = PassWord});
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
