namespace TTM.Core.Modules.Users.BasicInfo.Update;

public class UpdateUserCommandValidator : CustomValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(p => p.FullName)
            .NotEmpty()
               .WithMessage("LastName can not be null or empty!");

        RuleFor(x => x.RoleIds)
            .NotNull()
                .WithMessage("RoleIDs cannot be null.")
            .NotEmpty()
                .WithMessage("At least one role is required.");
    }
}