namespace TTM.Core.Modules.Authentication.Login;
public class LoginCommandValidator : CustomValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
                .WithMessage("Email cannot be null or empty!")
            .Matches(AppConstants.ValidEmailRegex)
                .WithMessage("Invalid Email Address!");

        RuleFor(p => p.Password)
            .NotEmpty()
               .WithMessage("Password can not be null or empty!.");
    }
}