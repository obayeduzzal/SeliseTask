using System.Net;
using Microsoft.AspNetCore.Http;
using TTM.Core.Infrastructure.Identity;
using TTM.Core.Shared.Exceptions;
using TTM.Core.Shared.Helpers.Json;

namespace TTM.Core.Infrastructure.Middleware;

internal class ExceptionMiddleware(ICurrentUser currentUser) : IMiddleware
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var currentUserId = _currentUser.GetId() != Guid.Empty ? _currentUser.GetId() : Guid.Empty;
            var response = context.Response;
            string? errorInfo = ex.Message;
            response.ContentType = "application/json";

            switch (ex)
            {
                case AppBadRequestException:
                case BadHttpRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case AppEntityNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case AppUnauthorizeException:
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case AppForbiddenException:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorInfo = "Something went wrong. Internal server error.";
                    break;
            }

            if (ex is not AppBadRequestException &&
                ex is not AppEntityNotFoundException &&
                ex is not AppUnauthorizeException &&
                ex is not AppForbiddenException)
            {
                var errorDto = ErrorHelper.GetErrorResponse(
                    response.StatusCode,
                    ex!.GetType().ToString(),
                    ex?.Message!);
                errorInfo = JsonConvertHelper.Serialize(errorDto);
            }

            LoggerHelper.SaveLog(ex!, errorInfo, currentUserId);

            await response.WriteAsync(errorInfo!);
        }
    }
}