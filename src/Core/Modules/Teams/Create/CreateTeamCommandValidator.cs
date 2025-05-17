namespace TTM.Core.Modules.Teams.Create;
public class CreateTeamCommandValidator : CustomValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Team name is required");
        RuleFor(p => p.TeamMembersId)
            .Must(p => p.Count > 0)
            .WithMessage("Atleast one team member should be selected");
    }
}
