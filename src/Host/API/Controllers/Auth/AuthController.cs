using TTM.Core.Modules.Authentication.Login;
using TTM.Host.Controllers.Base;

namespace TTM.Host.Controllers.Auth;

[AllowAnonymous]
public sealed class AuthController : VersionedApiController
{
    #region Endpoints
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthDTO), 200)]
    [OpenApiOperation("Request an access token using credentials.", "")]
    public async Task<IActionResult> LoginAsync(LoginCommand command)
    {
        var responseDTO = await Mediator.Send(command);

        return Ok(responseDTO);
    }

    #endregion
}