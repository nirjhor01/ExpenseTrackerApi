﻿using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Model
{
    public class UserLogin
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Id { get; set; }
    }
}
