using DogeServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Util;

public static class DogeServiceResponse
{
    public static async Task<ObjectResult> GenerateControllerResponse<T>(Func<Task<DogeServiceControllerResponse<T>>> func)
    {
        try
        {
            var controllerResponse = await func();
            controllerResponse ??= new DogeServiceControllerResponse<T>();

            var hasError = !string.IsNullOrEmpty(controllerResponse?.ErrorMessage);
            return (hasError)
                ? ErrorResponse(controllerResponse)
                : new OkObjectResult(controllerResponse); // return HTTP StatusCode 200

        }
        catch (Exception exception)
        {
            var response = new DogeServiceControllerResponse<int>
            {
                ErrorMessage = exception?.Message,
                StackTrace = exception?.StackTrace,
                StatusCode = StatusCodes.Status500InternalServerError
            };

            return ErrorResponse<int>(response);
        }
    }

    private static ObjectResult ErrorResponse<T>(DogeServiceControllerResponse<T>? responseObject)
    {
        var httpStatusCode = responseObject?.StatusCode ?? StatusCodes.Status500InternalServerError;

        return new ObjectResult(responseObject)
        {
            StatusCode = httpStatusCode
        };
    }
}