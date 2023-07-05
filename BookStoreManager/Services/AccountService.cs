using BookStoreManager.Entities;
using BookStoreManager.Models;

namespace BookStoreManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly BookStoreDbContext _dbContext;
        public AccountService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
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

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
