using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using ExpenseTrackerApi.Helper;

namespace ExpenseTrackerApi.Repository.Implementations
{
    public class ExpenseTrackerRepository : IExpenseTrackerRepository
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
                var sql = @"INSERT INTO [dbo].[Categories] 
                          ([UserId],[Transport],[Food],[EatingOut],[House],[Cloths],[Communication])
                          VALUES
                          (@UserId,@Transport,@Food,@EatingOut,@House,@Cloths,@Communication)";

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
                    var result = await connection.QueryAsync<UserLogin>(sql, new { UserName });
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

        public async Task<long> GetTotalSum(int Userid, DateTime fromDate, DateTime toDate)
        {
            int totalSum = 0;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
            {

                string query = @"
    SELECT 
        SUM(Transport) AS TransportSum,
        SUM(Food) AS FoodSum,
        SUM(EatingOut) AS EatingOutSum,
        SUM(House) AS HouseSum,
        SUM(Cloths) AS ClothsSum,
        SUM(Communication) AS CommunicationSum,
        SUM(Transport + Food + EatingOut + House + Cloths + Communication) AS TotalSum
        FROM Categories
        WHERE 
        UserId = @Userid 
";
               // SqlCommand command = new SqlCommand(query, connection);

                await connection.OpenAsync();

                // Execute the query using Dapper's QueryFirstOrDefaultAsync
                var result = await connection.QueryAsync<Categories>(query, new { Userid , fromDate, toDate });

                if (result != null)
                {
                    totalSum = Convert.ToInt32(result);
                }
            }

            return totalSum;
        }


        public async Task<ExpensePercentage?> GetExpensePercentage(int UserId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                var query = @"
                SELECT 
                TransportPercentage = SUM(Transport) * 100.0 / NULLIF(SUM(Transport + Food + EatingOut + House + Cloths + Communication), 0),
                FoodPercentage = SUM(Food) * 100.0 / NULLIF(SUM(Transport + Food + EatingOut + House + Cloths + Communication), 0),
                EatingOutPercentage = SUM(EatingOut) * 100.0 / NULLIF(SUM(Transport + Food + EatingOut + House + Cloths + Communication), 0),
                HousePercentage = SUM(House) * 100.0 / NULLIF(SUM(Transport + Food + EatingOut + House + Cloths + Communication), 0),
                ClothsPercentage = SUM(Cloths) * 100.0 / NULLIF(SUM(Transport + Food + EatingOut + House + Cloths + Communication), 0),
                CommunicationPercentage = SUM(Communication) * 100.0 / NULLIF(SUM(Transport + Food + EatingOut + House + Cloths + Communication), 0)
                FROM 
                Categories

                WHERE
                UserId = @UserId";

                    var result = await connection.QueryFirstOrDefaultAsync<ExpensePercentage>(query, new { UserId});
                    await connection.OpenAsync();
                    return result;

                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }

        public async Task<List<Categories>> SearchById(int UserId)
        {
            try
            {
                var sql = "SELECT * FROM dbo.Categories WHERE userid = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync(); // Open the connection
                    var result = await connection.QueryAsync<Categories>(sql, new { UserId });
                    return result.AsList(); // Convert the IEnumerable result to a List
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }


}
    

