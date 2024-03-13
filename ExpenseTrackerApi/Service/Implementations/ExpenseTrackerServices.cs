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

        public async Task<MessageHelperModel> AddSpendingAsync(Expense expense)
        {
            // var res = await _UnitOfWorkRepository.Todo.CreateTaskAsync(CreateTaskModel);
            var res = await _repository.AddSpendingAsync(expense);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res.Item2 <= 0)
            {
                msg.Message = "Expenditure exceeds deposit amount";
            }
            else
            {
                msg.Message = "Sucessfully Added";
            }
            return msg;

        }
   
 
     
 

        public async Task<(List<ExpensePercentage>, MessageHelperModel)> GetExpensePercentage(int UserId)
        {

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

        public async Task<(MessageHelperModel, List<Expense>)> SearchById(int UserId)
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

       // public Task<long> Deposit(Deposit deposit)



      public async Task<MessageHelperModel> Deposit(Deposit deposit)
        {
            // var res = await _UnitOfWorkRepository.Todo.CreateTaskAsync(CreateTaskModel);
            var res = await _repository.Deposit(deposit);
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
        public async Task<MessageHelperModel> UpdateByIdAsync(Expense expense)
        {
            var res = await _repository.UpdateByIdAsync(expense);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                msg.Message = "Failed To Update";
            }
            else
            {
                msg.Message = "Sucessfully Updated";
            }
            return msg;
       
        }

        public async Task<MessageHelperModel> DeleteByIdAsync(int Id)
        {
            var res = await _repository.DeleteByIdAsync(Id);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                msg.Message = "Failed To Delete Row";
            }
            else
            {
                msg.Message = "Sucessfully Deleted";
            }
            return msg;

        }
        //Task<MessageHelperModel> GetSum(int UserId, string Category);
        public async Task<(MessageHelperModel,long)> GetSum(int UserId, string Category, DateTime fromDate, DateTime toDate)
        {
            var res = await _repository.GetSum(UserId,Category,fromDate,toDate);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                msg.Message = "Failed To Retrive Information";
            }
            else
            {
                msg.Message = "Sucessfully Retrived";
            }
            return (msg,res);

        }



        public async Task<Expense> LastExpenseAsync(int UserId)
        {
            var res = await _repository.LastExpenseAsync(UserId);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == null)
            {
                msg.Message = "Failed To Delete Row";
            }
            else
            {
                msg.Message = "Sucessfully Deleted";
            }
            return res;

        }







    }
}
