using Dapper;
using ExpenseTrackerApi.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTrackerApi.JWT
{
    public class UserServiceRepository : IUserServiceRepository
    {
        public readonly IConfiguration _configuration;
        public UserServiceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> IsValidUserAsync(UserLogin users)
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM dbo.RegistrationTable WHERE username = @UserName";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();
                    string userName = users.UserName;
                    var result = await connection.ExecuteScalarAsync<int>(sql, new { UserName = userName });
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while validating user.", ex);
            }
        }

        public async Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"INSERT INTO UserRefreshTokens (UserName, RefreshToken, IsActive, ExpirationTime) 
    VALUES (@UserName, @RefreshToken, @IsActive, @ExpirationTime);
    SELECT CAST(SCOPE_IDENTITY() as int)";


                    int newId = await connection.ExecuteScalarAsync<int>(sql, user);
                    user.Id = newId;

                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding user refresh tokens.", ex);
            }
        }

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    connection.Open();

                    var sql = @"SELECT * FROM UserRefreshTokens 
                            WHERE UserName = @UserName AND RefreshToken = @RefreshToken";

                    return connection.QuerySingleOrDefault<UserRefreshTokens>(sql, new { UserName = username, RefreshToken = refreshToken });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting user refresh tokens.", ex);
            }
        }

        public async Task<UserRefreshTokens> DeleteUserRefreshTokens(string username, string refreshToken)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"DELETE FROM UserRefreshTokens WHERE UserName = @UserName AND RefreshToken = @RefreshToken;
                        SELECT * FROM UserRefreshTokens WHERE UserName = @UserName";

                    return await connection.QuerySingleOrDefaultAsync<UserRefreshTokens>(sql, new { UserName = username, RefreshToken = refreshToken });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting user refresh tokens.", ex);
            }
        }




    }
}
