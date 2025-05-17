namespace TTM.Core.Modules.Users.BasicInfo.Create;

public class CreateUserCommandValidator : CustomValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
                .WithMessage("Email cannot be null or empty!")
            .Matches(AppConstants.ValidEmailRegex)
                .WithMessage("Invalid Email Address!");

        RuleFor(p => p.FullName)
            .NotEmpty()
               .WithMessage("FirstName can not be null or empty!");

        RuleFor(p => p.Password)
            .NotEmpty()
                .WithMessage("NewPassword cannot be null or empty!")
            .MinimumLength(6)
                .WithMessage("NewPassword must be at least 6 characters long.");

        RuleFor(p => p.ConfirmPassword)
            .NotEmpty()
                .WithMessage("ConfirmPassword cannot be null or empty!")
            .Equal(p => p.Password)
                .WithMessage("Passwords do not match.");

        RuleFor(x => x.RoleIds)
            .NotNull()
                .WithMessage("RoleIDs cannot be null.")
            .NotEmpty()
                .WithMessage("At least one role is required.");
    }
}