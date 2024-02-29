using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Service.Interfaces
{
    public interface IExpeneseTrackerServices
    {
        Task<MessageHelperModel> CreateUserAsync(UserModel user);
      
        Task<(UserLogin, MessageHelperModel)> LoginUserAsync(string UserName, string PassWord);

    }
}
