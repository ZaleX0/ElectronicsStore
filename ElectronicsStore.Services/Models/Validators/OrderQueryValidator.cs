using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Queries;
using FluentValidation;

namespace ElectronicsStore.Services.Models.Validators;

public class OrderQueryValidator : AbstractValidator<OrderQuery>
{
    private int[] allowedPageSizes = new[] { 2, 5, 10, 20, 30 };

    public OrderQueryValidator()
    {
        RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(q => q.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
            {
                context.AddFailure(nameof(ProductQuery.PageSize), $"Page size must be in [{string.Join(", ", allowedPageSizes)}]");
            }
        });
    }
}
