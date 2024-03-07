using ExpenseTrackerApi.Controllers;
using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Service.Implementations
{
    public class ExpenseTrackerServices : IExpeneseTrackerServices
    {
      
        private readonly IExpenseTrackerRepository _repository;
        public ExpenseTrackerServices(IExpenseTrackerRepository repository)
        {
            _repository = repository;
        }

        public async Task<MessageHelperModel> AddSpendingAsync(Categories categories)
        {
           // var res = await _UnitOfWorkRepository.Todo.CreateTaskAsync(CreateTaskModel);
            var res = await _repository.AddSpendingAsync(categories);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                msg.Message = "Faild To Create";
            }
            else
            {
                msg.Message = "Sucessfully Added";
            }
            return msg;

        }
        public async Task<long> GetTransportSum(DateTime fromdate, DateTime toDate)
        {
            var res = await _repository.GetTransportSum(fromdate, toDate);
           // var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
           return res;

        }

        public async Task<( ExpensePercentage,MessageHelperModel)> GetExpensePercentage(int UserId) { 
   
            var res = await _repository.GetExpensePercentage(UserId);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == null)
            {
                msg.Message = "Faild to get percentage";
            }
            else
            {
                msg.Message = "Sucessfully get percentage";
            }
            return (res, msg);

    }

        public async Task<long> GetTotalSum(int Userid, DateTime fromdate, DateTime toDate)
        {
            var res = await _repository.GetTotalSum(Userid, fromdate, toDate);
            // var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            return res;

        }

        public async Task<(MessageHelperModel, List<Categories>)> SearchById(int UserId)
        {
            var res = await _repository.SearchById(UserId);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == null || res.Count == 0)
            {
                msg.Message = "Failed to get data";
            }
            else
            {
                msg.Message = "Successfully got the data";
            }
            return (msg, res);
        }












    }
}
