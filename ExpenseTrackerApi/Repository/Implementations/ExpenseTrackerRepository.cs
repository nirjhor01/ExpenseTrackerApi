using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using ExpenseTrackerApi.Helper;
using System.Linq;
using System.Data.SqlTypes;

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


        public async Task<(long,long)> AddSpendingAsync(Expense expense)
        {

            try
            {
                

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    connection.Open();



                    var DepositAmountQuery = @"select sum(Amount) as amount from Deposit";
                    var ExpenditureQuery = @"SELECT SUM(Amount) AS Amount FROM Expense";
                    var expenditure = await connection.QueryFirstOrDefaultAsync<long>(ExpenditureQuery);
                    var Deposit = await connection.QueryFirstOrDefaultAsync<long>(DepositAmountQuery);
                    var expenditureAmount = expenditure == null ? 0 : expenditure;
                    var DepositAmount = Deposit == null ? 0 : Deposit;

                   

                    var sql = @"INSERT INTO [dbo].[Expense] 
                          ([UserId],[Category],[Amount],[DateTimeInfo])
                          VALUES
                          (@UserId,@Category,@Amount, @DateTimeInfo)";
                    var res = await connection.ExecuteAsync(sql, expense); // return  0 or 1
                    long dif = DepositAmount - expenditureAmount;
                    return (res, dif);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> Deposit(Deposit deposit)
        {

            try
            {
                var sql = @"INSERT INTO [dbo].[Deposit] 
                          ([UserId],[DateInfo],[Amount])
                          VALUES
                          (@UserId,@DateInfo,@Amount)";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, deposit); // return  0 or 1
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


        public async Task<long> GetTransportSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                string query = @"SELECT SUM(Amount) FROM Expense WHERE Category = 'Transport' AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate AND UserId = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    // Execute the query using Dapper and retrieve the result
                    long transportSum = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                    return transportSum;
                }
            }
            
          
         
            catch (Exception ex)
            {
                string customMessage = "Custom message for custom Exception";
                int statusCode = 500;
                var msg = new MessageHelperModel()
                {
                    Message = customMessage,
                    StatusCode = statusCode
                };
                throw new CustomizedException( statusCode, customMessage ,msg, ex);
            }
        }
        public async Task<long> GetEatingOutSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                string query = @"SELECT SUM(Amount) FROM Expense WHERE Category = 'EatingOut' AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate AND UserId = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    // Execute the query using Dapper and retrieve the result
                    long foodSum = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                    return foodSum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> GetHouseSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                string query = @"SELECT SUM(Amount) FROM Expense WHERE Category = 'House' AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate AND UserId = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    // Execute the query using Dapper and retrieve the result
                    long foodSum = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                    return foodSum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> GetClothsSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                string query = @"SELECT SUM(Amount) FROM Expense WHERE Category = 'Cloths' AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate AND UserId = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    // Execute the query using Dapper and retrieve the result
                    long foodSum = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                    return foodSum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> GetCommunicationSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                string query = @"SELECT SUM(Amount) FROM Expense WHERE Category = 'Communication' AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate AND UserId = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    // Execute the query using Dapper and retrieve the result
                    long foodSum = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                    return foodSum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> GetFoodSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            try

            {
                string query = @"SELECT SUM(Amount) FROM Expense WHERE Category = 'Food' AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate AND UserId = @UserId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    // Execute the query using Dapper and retrieve the result
                    long foodSum = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                    return foodSum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<long> GetTotalSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            int totalSum = 0;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
            {

                string query = @"SELECT SUM(Amount) AS TotalAmount FROM Expense WHERE UserId = @UserId AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate";
                // SqlCommand command = new SqlCommand(query, connection);

                await connection.OpenAsync();

                // Execute the query using Dapper's QueryFirstOrDefaultAsync
                var result = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });
                // var result = await connection.QueryFirstOrDefaultAsync(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                if (result != null)
                {
                    totalSum = Convert.ToInt32(result);
                }
            }

            return totalSum;
        }


        public async Task<List<ExpensePercentage>> GetExpensePercentage(int UserId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    string query = @"
                    SELECT 
                    Category,

                    CAST(SUM(Amount) * 100.0 / (
                        SELECT SUM(Amount)
                        FROM Expense
                        WHERE UserId = @UserId
                    ) AS DECIMAL(10,2)) AS Percentage

                    FROM Expense

                    WHERE UserId = @UserId
                    GROUP BY Category";

                    await connection.OpenAsync();

                    var result = await connection.QueryAsync<ExpensePercentage>(query, new { UserId });

                    return result.AsList();
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
                    var result = await connection.QueryAsync<Categories>(sql, new { UserId }); //QueryAsync is used to execute a SQL query that may return multiple rows of data.
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


