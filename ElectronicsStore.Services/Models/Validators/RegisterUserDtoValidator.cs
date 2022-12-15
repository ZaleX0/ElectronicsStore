using ElectronicsStore.Data;
using FluentValidation;

namespace ElectronicsStore.Services.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterDto>
{
	public RegisterUserDtoValidator(ElectronicsStoreDbContext dbContext)
	{
		RuleFor(x => x.Password)
			.MinimumLength(8);

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress()
			.Custom((value, context) =>
			{
				var emailInUse = dbContext.Users.Any(u => u.Email == value);
				if (emailInUse)
				{
					context.AddFailure(nameof(RegisterDto.Email), "That email is taken");
				}
			});
	}
}
