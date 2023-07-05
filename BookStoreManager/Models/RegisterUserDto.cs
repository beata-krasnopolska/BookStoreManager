using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManager.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Password { get; set; }
        public string Nationality { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; } = 3;
    }
}
