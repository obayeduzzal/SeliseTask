using Ardalis.Specification;
using TTM.Core.Domain.Team;

namespace TTM.Core.Shared.Specifications.Teams;

public class GetTeamByName : Specification<Team>
{
    public GetTeamByName(string name)
    {
        Query.Where(i => i.Name.ToLower() == name.ToLower());
    }
} 
