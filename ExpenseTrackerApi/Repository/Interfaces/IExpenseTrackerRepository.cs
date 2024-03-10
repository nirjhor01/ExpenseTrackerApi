using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Repository.Interfaces
{
    public interface IExpenseTrackerRepository
    {
        Task<long> CreateUserAsync(UserModel user);
        Task<UserLogin?> UserLogInAsync(string UserName, string PassWord);
        Task<long> AddSpendingAsync(Expense expense);
        Task<long> GetTransportSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetEatingOutSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetHouseSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetClothsSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetCommunicationSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetFoodSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetTotalSum(int Userid, DateTime fromDate, DateTime toDate);
        Task<ExpensePercentage?> GetExpensePercentage(int UserId);
        Task<List<Categories>> SearchById(int UserId);
        public Task<long> Deposit(Deposit deposit);
    }
}