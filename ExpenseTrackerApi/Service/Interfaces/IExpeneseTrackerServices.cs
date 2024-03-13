using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Service.Interfaces
{
    public interface IExpeneseTrackerServices
    {
        //Task<(UserLogin, MessageHelperModel)> LoginUserAsync(string UserName, string PassWord);
        Task<MessageHelperModel> AddSpendingAsync(Expense expense);
        Task<(MessageHelperModel, List<Expense>)> SearchById(int UserId);
        Task<(List<ExpensePercentage>, MessageHelperModel)> GetExpensePercentage(int UserId);
        Task<long> GetTotalSum(int Userid, DateTime fromDate, DateTime toDate);
        Task<MessageHelperModel> Deposit(Deposit deposit);
        Task<MessageHelperModel> UpdateByIdAsync(Expense expense);
        Task<MessageHelperModel> DeleteByIdAsync(int Id);
        Task<Expense> LastExpenseAsync(int UserId);
        Task<(MessageHelperModel,long)> GetSum(int UserId, string Category, DateTime fromDate, DateTime toDate);
       


    }
}
