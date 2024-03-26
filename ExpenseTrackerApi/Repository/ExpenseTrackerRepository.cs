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
using System.Threading.Tasks;

namespace ExpenseTrackerApi.Repository.Implementations
{
    public class ExpenseTrackerRepository : IExpenseTrackerRepository
    {
        public readonly IConfiguration _configuration;
        public ExpenseTrackerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task<(long, long)> AddSpendingAsync(Expense expense)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync(); // Asynchronous open operation
                    using (var trans = connection.BeginTransaction())
                    {
                        try
                        {
                            // Corrected SQL queries
                            var DepositAmountQuery = @"SELECT ISNULL(SUM(Amount), 0) AS Amount FROM Deposit";
                            var ExpenditureQuery = @"SELECT ISNULL(SUM(Amount), 0) AS Amount FROM Expense";

                            // Execute queries asynchronously
                            var expenditure = await connection.ExecuteScalarAsync<long>(ExpenditureQuery, transaction: trans);
                            var deposit = await connection.ExecuteScalarAsync<long>(DepositAmountQuery, transaction: trans);

                            // Cast to long is unnecessary as ExecuteScalarAsync already returns long
                            var expenditureAmount = expenditure;
                            var depositAmount = deposit;

                            // Corrected SQL statement, added parameters
                            var sql = @"INSERT INTO [dbo].[Expense] ([UserId],[Category],[Amount],[DateTimeInfo],[Note]) 
                                VALUES (@UserId, @Category, @Amount, @DateTimeInfo, @Note)";

                            // ExecuteAsync method expects an object parameter for the parameters in the query
                            var res = await connection.ExecuteAsync(sql, expense, transaction: trans);

                            // Calculate the difference between deposits and expenditures
                            long difference = depositAmount - expenditureAmount;

                            // Commit transaction if everything is successful
                            trans.Commit();

                            return (res, difference);
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions and rollback transaction if an error occurs
                            trans.Rollback();
                            throw new Exception("Error occurred while processing the request.", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Throw the exception if any error occurs during the connection or opening transaction
                throw new Exception("Error occurred while establishing database connection.", ex);
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

     











        public async Task<long> GetTotalSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            int totalSum = 0;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
            {

                string query = @"SELECT SUM(Amount) AS TotalAmount FROM Expense WHERE UserId = @UserId AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate";

                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId, FromDate = fromDate, ToDate = toDate });

                if (result != 0)
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
                    await connection.OpenAsync();
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

                    var result = await connection.QueryAsync<ExpensePercentage>(query, new { UserId });

                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public async Task<List<Expense>> SearchById(int UserId)
        {
            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();
                    var sql = @"SELECT * FROM Expense WHERE userid = @UserId";
                    var result = await connection.QueryAsync<Expense>(sql, new { UserId }); 
                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> UpdateByIdAsync(Expense expense)
        {
            try
            {



                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();
                    var sql = @"UPDATE [dbo].[Expense]
                            SET [UserId] = @UserId, 
                            [Category] = @Category,
                            [Amount] = @Amount,
                            [DateTimeInfo] = @DateTimeInfo 
                            [Note] = @Note 
                            WHERE [Id] = @Id";
                    var res = await connection.ExecuteAsync(sql, expense);

                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<long> DeleteByIdAsync(int Id)
        {
            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();
                    var sql = @"DELETE FROM Expense WHERE Id = @Id";
                    var res = await connection.ExecuteAsync(sql, new { Id });
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






        public async Task<long> GetSum(int UserId, string Category, DateTime fromDate, DateTime toDate)
        {
            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();

                    string query = @"SELECT ISNULL(SUM(Amount),0) AS TotalAmount FROM Expense WHERE UserId = @UserId AND Category=@Category AND DateTimeInfo >= @FromDate AND DateTimeInfo <= @ToDate";
                    var res = await connection.QueryFirstOrDefaultAsync<long>(query, new { UserId = UserId, Category = Category, FromDate = fromDate, ToDate = toDate });
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Expense?> LastExpenseAsync(int UserId)
        {
            try
            {


                using (var connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection")))
                {
                    await connection.OpenAsync();
                    var sql = @"SELECT TOP 1 *FROM Expense WHERE UserId = @UserId ORDER BY DateTimeInfo DESC";
                    var res = await connection.QueryAsync<Expense>(sql, new { UserId = UserId });
                    return res.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }


}


