using FluentValidation;
using System.Linq;

namespace BookStoreManager.Models.Validators
{
    public class BookStoreQueryValidator : AbstractValidator<BookStoreQuery>
    {
        private int[] allowedPageSizes = new int[] { 5, 10, 15 };
        public BookStoreQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Value of PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
