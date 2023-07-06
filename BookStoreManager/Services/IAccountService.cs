using BookStoreManager.Models;

namespace BookStoreManager.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto registerUserDto);

        string GenerateJwt(LoginDto loginDto);
    }
}
