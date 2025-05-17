using System.Threading;
using TTM.Core.Modules.Teams.Create;
using TTM.Core.Modules.Teams.Get;
using TTM.Host.Controllers.Base;

namespace TTM.Host.Controllers.Team;
public class TeamsController : VersionedApiController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseMetaDTO), 200)]
    [MustHavePermission(AppActions.Create, AppResources.Teams)]
    [OpenApiOperation("Create a Team.", "")]
    public async Task<IActionResult> CreateAsync(CreateTeamCommand command, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(command, ct);

        return Ok(responseDTO);
    }

    [HttpGet("{id}")]
    [MustHavePermission(AppActions.View, AppResources.Teams)]
    [ProducesResponseType(typeof(TeamDTO), 200)]
    [OpenApiOperation("Get a team details.", "")]
    public async Task<IActionResult> GetByIDAsync(Guid id, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(new GetTeamQuery(id), ct);

        return Ok(responseDTO);
    }
}
