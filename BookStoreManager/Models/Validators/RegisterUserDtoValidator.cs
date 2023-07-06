using BookStoreManager.Entities;
using FluentValidation;
using System.Linq;

namespace BookStoreManager.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BookStoreDbContext dbContext)
        {
            RuleFor(x=>x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).NotEmpty().Equal(x => x.ConfirmPassword).MaximumLength(15);

            var users = dbContext.Users;
            RuleFor(x => x.Email)
                .Custom((value, dbContext) =>
                {
                    var existingEmail = users.Any(u => u.Email == value);
                    if (existingEmail)
                    {
                        dbContext.AddFailure("Email", "Email already exists in the Db");
                    }

                });
        }
    }
}
