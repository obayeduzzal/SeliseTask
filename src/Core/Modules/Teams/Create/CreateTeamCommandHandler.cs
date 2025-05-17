using TTM.Core.Domain.Team;
using TTM.Core.Shared.Specifications.Teams;

namespace TTM.Core.Modules.Teams.Create;
public class CreateTeamCommandHandler(IRepository<Team> teamRepository, IRepository<TeamMember> teamMemberRepository) : IRequestHandler<CreateTeamCommand, ResponseMetaDTO>
{
    private readonly IRepository<Team> _teamRepository = teamRepository;
    private readonly IRepository<TeamMember> _teamMemberRepository = teamMemberRepository;
    public async Task<ResponseMetaDTO> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var duplicateTeam = await _teamRepository.FirstOrDefaultAsync(new GetTeamByName(request.Name), cancellationToken);
        if (duplicateTeam != null)
            ErrorHelper.ThrowBadRequestException("Team", "Team already exists");

        var newTeam = Team.Create(request.Name, request.Description);

        await _teamRepository.AddAsync(newTeam, cancellationToken);
        List<TeamMember> members = [];

        request.TeamMembersId.ForEach(i =>
        {
            members.Add(TeamMember.Create(newTeam.Id, i));
        });

        await _teamMemberRepository.AddRangeAsync(members, cancellationToken);

        return new ResponseMetaDTO
        {
            Message = "Team Created successfully"
        };
    }
}
