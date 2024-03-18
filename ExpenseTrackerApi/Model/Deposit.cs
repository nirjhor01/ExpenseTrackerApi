using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Model
{
    

    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int intValue)
            {
                return intValue > 0;
            }

            return false;
        }
    }
    public class Deposit
    {
        [Required]
        [GreaterThanZero(ErrorMessage = "UserId should be greater than 0")]
        public int UserId { get; set; }
        public DateTime DateInfo { get; set; }
        [Required]
        [GreaterThanZero(ErrorMessage = "Amount should be greater than 0")]
        public int Amount {  get; set; }
    }
}
