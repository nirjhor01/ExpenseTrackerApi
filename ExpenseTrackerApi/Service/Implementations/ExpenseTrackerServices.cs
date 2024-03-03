using ExpenseTrackerApi.Controllers;
using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

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
        









    }
}
