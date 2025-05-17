using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TTM.Core.Shared.Helpers;
public static class ValidationErrorHelper
{
    public static IActionResult ValidationResponse(ActionContext context)
    {
        ErrorDTO errorList = new ErrorDTO
        {
            error = new ErrorInfo
            {
                logID = Guid.NewGuid().ToString(),
                statusCode = 400,
                type = "ValidatorException",
                messages = new Dictionary<string, string>()
            }
        };

        foreach (var keyModelStatePair in context.ModelState)
        {
            var errors = keyModelStatePair.Value.Errors;

            if (errors?.Count > 0)
            {
                if (errors.Count == 1)
                {
                    string message = GetErrorMessage(errors[0]);
                    errorList.error.messages.Add(keyModelStatePair.Key, message);
                }
                else
                {
                    string[] errorMessages = new string[errors.Count];

                    for (int i = 0; i < errors.Count; i++)
                    {
                        errorMessages[i] = GetErrorMessage(errors[i]);

                        if (!errorList.error.messages.ContainsKey(keyModelStatePair.Key))
                            errorList.error.messages.Add(keyModelStatePair.Key, errorMessages[i]);
                    }
                }
            }
        }

        LoggerHelper.SaveLog(errorList);

        var badRequestResponse = new BadRequestObjectResult(errorList);
        badRequestResponse.ContentTypes.Add("application/json");

        return badRequestResponse;
    }

    private static string GetErrorMessage(ModelError error)
    {
        return string.IsNullOrEmpty(error.ErrorMessage) ? "The input was not valid." : error.ErrorMessage;
    }
}
