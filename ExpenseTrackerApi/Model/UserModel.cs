using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required,MinLength(2),MaxLength(30)]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
