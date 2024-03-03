using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Service.Interfaces
{
    public interface IExpeneseTrackerServices
    {
        //Task<(UserLogin, MessageHelperModel)> LoginUserAsync(string UserName, string PassWord);
        Task<MessageHelperModel> AddSpendingAsync(Categories categories);
        Task<long> GetTransportSum(DateTime fromDate, DateTime toDate);


    }
}
