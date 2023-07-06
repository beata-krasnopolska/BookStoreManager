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

            RuleFor(x => x.Email)
                .Custom((value, dtoCcontext) =>
                {
                    var existingEmail = dbContext.Users.Any(u => u.Email == value);
                    if (existingEmail)
                    {
                        dtoCcontext.AddFailure("Email", "Email already exists in the Db");
                    }

                });
        }
    }
}
