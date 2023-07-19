using BookStoreManager.Entities;
using FluentValidation;
using System.Linq;

namespace BookStoreManager.Models.Validators
{
    public class BookStoreQueryValidator : AbstractValidator<BookStoreQuery>
    {
        private int[] allowedPageSizes = new int[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames = new string[] {nameof(BookStore.Name), nameof(BookStore.Description), nameof(BookStoreDto.Category) };

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

            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
