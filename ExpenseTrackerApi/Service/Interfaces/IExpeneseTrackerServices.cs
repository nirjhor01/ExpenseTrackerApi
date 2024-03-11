using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Service.Interfaces
{
    public interface IExpeneseTrackerServices
    {
        //Task<(UserLogin, MessageHelperModel)> LoginUserAsync(string UserName, string PassWord);
        Task<MessageHelperModel> AddSpendingAsync(Expense expense);
        Task<long> GetTransportSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetFoodSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetEatingOutSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetHouseSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetClothsSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<long> GetCommunicationSum(int UserId, DateTime fromDate, DateTime toDate);
        Task<(MessageHelperModel, List<Categories>)> SearchById(int UserId);
        Task<(List<ExpensePercentage>, MessageHelperModel)> GetExpensePercentage(int UserId);
        Task<long> GetTotalSum(int Userid, DateTime fromDate, DateTime toDate);
        Task<MessageHelperModel> Deposit(Deposit deposit);

    }
}
