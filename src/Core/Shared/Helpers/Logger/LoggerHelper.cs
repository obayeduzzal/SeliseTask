using TTM.Core.Shared.Helpers.Json;
using Serilog;

namespace TTM.Core.Shared.Helpers;
public static class LoggerHelper
{
    public static void SaveLog(Exception ex, string? errorResponse, Guid userID)
    {
        if (string.IsNullOrEmpty(errorResponse))
            return;

        var errorDTO = errorResponse.Deserialize<ErrorDTO>();
        string messageTemplate = string.Format("{0} {1} {2} {3} {4}", errorDTO?.error?.type + "(" + errorDTO?.error?.statusCode + ")\n", "UserID:{CurrentUserID}\n", "LogID:{LogID}\n", "ErrorInformation:{ErrorInfo}\n", "{StackTrace}\n");

        Log.Error(messageTemplate, userID, errorDTO?.error?.logID, errorResponse, ex.StackTrace);
    }

    public static void SaveLog(Exception ex, ErrorDTO errorDTO, Guid? userID = null)
    {
        string messageTemplate = string.Format(
                                        "{0} {1} {2} {3} {4}",
                                        errorDTO?.error?.type + "(" + errorDTO?.error?.statusCode + ")\n",
                                        "UserID:{CurrentUserID}\n",
                                        "LogID:{LogID}\n",
                                        "ErrorInformation:{ErrorInfo}\n",
                                        "{StackTrace}\n");

        Log.Error(messageTemplate, userID ?? Guid.Empty, errorDTO?.error?.logID, JsonConvert.SerializeObject(errorDTO), ex.StackTrace);
    }

    public static void SaveLog(string errorResponse, Guid userID)
    {
        var errorDTO = JsonConvert.DeserializeObject<ErrorDTO>(errorResponse);
        string messageTemplate = string.Format("{0} {1} {2} {3}", errorDTO!.error?.type + "(" + errorDTO.error?.statusCode + ")\n", "UserID:{CurrentUserID}\n", "LogID:{LogID}\n", "ErrorInformation:{ErrorInfo}\n");
        Log.Error(messageTemplate, userID, errorDTO.error?.logID, errorResponse);
    }

    public static void SaveLog(ErrorDTO error)
    {
        string errorResponse = JsonConvert.SerializeObject(error);
        string messageTemplate = string.Format("{0} {1} {2}", $"{error.error?.type} ({error.error?.statusCode})\n", "LogID:{LogID}\n", "ErrorInformation:{ErrorInfo}\n");

        Log.Error(messageTemplate, error.error?.logID, errorResponse);
    }
}
