using System.Threading;
using TTM.Core.Modules.Users.BasicInfo.Create;
using TTM.Core.Modules.Users.BasicInfo.Delete;
using TTM.Core.Modules.Users.BasicInfo.Get;
using TTM.Core.Modules.Users.BasicInfo.Search;
using TTM.Core.Modules.Users.BasicInfo.Update;
using TTM.Host.Controllers.Base;

namespace TTM.Host.Controllers.User;

public class UsersController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(AppActions.Search, AppResources.Users)]
    [ProducesResponseType(typeof(PagedData<UserDTO>), 200)]
    [OpenApiOperation("Search users.", "")]
    public async Task<IActionResult> SearchAsync([FromQuery] SearchUsersCommand command, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(command, ct);

        return Ok(responseDTO);
    }

    [HttpGet("{id}")]
    [MustHavePermission(AppActions.View, AppResources.Users)]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [OpenApiOperation("Get a user details.", "")]
    public async Task<IActionResult> GetByIDAsync(Guid id, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(new GetUserRequest(id), ct);

        return Ok(responseDTO);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseMetaDTO), 200)]
    [MustHavePermission(AppActions.Create, AppResources.Users)]
    [OpenApiOperation("Create a user.", "")]
    public async Task<IActionResult> CreateAsync(CreateUserCommand command, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(command, ct);

        return Ok(responseDTO);
    }

    [HttpPut]
    [MustHavePermission(AppActions.Update, AppResources.Users)]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [OpenApiOperation("Update a user.", "")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand command, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(command, ct);

        return Ok(responseDTO);
    }

    [HttpDelete("{id}/delete")]
    [MustHavePermission(AppActions.Delete, AppResources.Users)]
    [ProducesResponseType(typeof(ResponseMetaDTO), 200)]
    [OpenApiOperation("Delete a user.", "")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
    {
        var responseDTO = await Mediator.Send(new DeleteUserCommand(id), ct);

        return Ok(responseDTO);
    }
}