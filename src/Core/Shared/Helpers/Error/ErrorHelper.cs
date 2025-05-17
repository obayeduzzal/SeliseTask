namespace TTM.Core.Shared.Helpers;
public static class ErrorHelper
{
    public static ErrorDTO GetErrorResponse(int statusCode, string type, Dictionary<string, string> messages)
    {
        return new ErrorDTO
        {
            error = new ErrorInfo
            {
                logID = Guid.NewGuid().ToString(),
                statusCode = statusCode,
                type = type,
                messages = messages
            }
        };
    }

    public static ErrorDTO GetErrorResponse(
        int statusCode,
        string type,
        string message,
        string key = "Generic")
    {
        return new ErrorDTO
        {
            error = new ErrorInfo
            {
                logID = Guid.NewGuid().ToString(),
                statusCode = statusCode,
                type = type,
                messages = new Dictionary<string, string>() { { key, message } }
            }
        };
    }

    public static void ThrowBadRequestException(string key, string errorMessage)
    {
        var errorDto = GetErrorResponse(400, "BadRequestException", errorMessage, key);
        string message = JsonConvert.SerializeObject(errorDto);

        throw new AppBadRequestException(message);
    }

    public static void ThrowNotFoundException(string key, string errorMessage)
    {
        var errorDto = GetErrorResponse(404, "NotFoundException", errorMessage, key);
        string message = JsonConvert.SerializeObject(errorDto);

        throw new AppEntityNotFoundException(message);
    }

    public static void ThrowUnauthorizedException(
        string key = "UnAuthorizedException",
        string msg = "You are not logged in or not authorized.")
    {
        var errorDto = GetErrorResponse(401, key, msg);
        string message = JsonConvert.SerializeObject(errorDto);

        throw new AppUnauthorizeException(message);
    }

    public static void ThrowForbiddenException(
        string key = "PermisionDeniedException",
        string msg = "You are not permitted to access this company's information.")
    {
        var errorDto = GetErrorResponse(403, key, msg);
        string message = JsonConvert.SerializeObject(errorDto);

        throw new AppForbiddenException(message);
    }
}
