using BookStoreManager.Entities;
using BookStoreManager.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(BookStoreDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            var newUser = new User()
            {
                Email = registerUserDto.Email,
                BirthDate = registerUserDto.BirthDate,
                Nationality = registerUserDto.Nationality,
                RoleId = registerUserDto.RoleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = hashedPassword;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
