using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Queries;
using FluentValidation;

namespace ElectronicsStore.Services.Models.Validators;

public class ProductQueryValidator : AbstractValidator<ProductQuery>
{
	private int[] allowedPageSizes = new[] { 3, 5, 10, 15 };
	private string[] allowedSortByColumnNames =
	{
		nameof(Product.Name),
		nameof(Product.Price),
		nameof(Product.Brand),
        nameof(Product.Category),
    };
	public ProductQueryValidator()
	{
		RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(1);
		RuleFor(q => q.PageSize).Custom((value, context) =>
		{
			if (!allowedPageSizes.Contains(value))
			{
				context.AddFailure(nameof(ProductQuery.PageSize), $"Page size must be in [{string.Join(", ", allowedPageSizes)}]");
			}
		});

		RuleFor(q => q.SortBy)
			.Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
			.WithMessage($"Sort by is optional, or must be in [{string.Join(", ", allowedSortByColumnNames)}]");
	}
}
