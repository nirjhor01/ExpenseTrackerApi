using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Service.Interfaces
{
    public interface IExpeneseTrackerServices
    {
        //Task<(UserLogin, MessageHelperModel)> LoginUserAsync(string UserName, string PassWord);
        Task<MessageHelperModel> AddSpendingAsync(Categories categories);
        Task<long> GetTransportSum(DateTime fromDate, DateTime toDate);
        Task<(MessageHelperModel, List<Categories>)> SearchById(int UserId);
        Task<(ExpensePercentage, MessageHelperModel)> GetExpensePercentage(int UserId);
        Task<long> GetTotalSum(int Userid, DateTime fromDate, DateTime toDate);

    }
}
