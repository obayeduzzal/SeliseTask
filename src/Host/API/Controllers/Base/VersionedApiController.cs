namespace TTM.Host.Controllers.Base;

[Route("api/v{version:apiVersion}/[controller]")]
public class VersionedApiController : BaseApiController;
