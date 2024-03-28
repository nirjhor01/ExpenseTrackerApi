using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerApi.Service;
using ExpenseTrackerApi.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IExpeneseTrackerServices _services;
        public UserController(IExpeneseTrackerServices services)
        {
            _services = services;
        }

       /* [HttpGet("Admins")]
        [Authorize]
        public string AdminsEndPoint()
        {
           // var currentUser = GetCurrentUser();
           // return Ok($"HI {currentUser.UserName},you are an{currentUser.Role}");
           return "401";
        }*/
        [HttpPost]
        [Route("AddSpending")]
        public async Task<IActionResult> AddSpending( Expense expense)
        {
            var res = await _services.AddSpendingAsync(expense);
            return Ok(res);

        }
        [HttpPost]
        [Route("Deposit")]
        public async Task<IActionResult> Deposit(Deposit deposit)
        {
            var res = await _services.Deposit(deposit);
            return Ok(res);

        }



        [HttpGet]
        [Route("GetTotalSum")]
        public async Task<IActionResult> GetTotalSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            var totalSum = await _services.GetTotalSum(UserId, fromDate, toDate);

            // Return the total sum as a JSON response
            return Ok(new { TotalSum = totalSum });
        }
        [Authorize]
        [HttpGet]
        [Route("ExpensePercentage")]
        public async Task<IActionResult> ExpensePercentage(int UserId)
        {
            var res = await _services.GetExpensePercentage(UserId);
            return Ok(new { info = res.Item1, msg = res.Item2 }) ;
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(int UserId)
        {
            var res = await _services.SearchById(UserId);
            return Ok(new { info = res.Item1, msg = res.Item2 });
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Expense expense)
        {
            var res = await _services.UpdateByIdAsync(expense);
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var res = await _services.DeleteByIdAsync(Id);
            return Ok(res);
        }

           [HttpGet]
           [Route("lastExpense")]
           public async Task<IActionResult> LastExpense(int UserId)
           {
               var res = await _services.LastExpenseAsync(UserId);
               return Ok(res);
           }

     


        [HttpGet]
        [Route("categorySum")]
        public async Task<IActionResult> GetSum([Range(1, long.MaxValue, ErrorMessage = "Please enter UserId value bigger than {1}")] int UserId, String Category, DateTime FromDate, DateTime ToDate)
        {
            var res = await _services.GetSum(UserId,Category,FromDate,ToDate);
            return Ok(new { info = res.Item1, sum = res.Item2 });
        }














    }
}
