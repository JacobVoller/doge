using DogeServer.Models.DogeResponses;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Util;

public static class DogeServiceResponse
{

    public static async Task<ObjectResult> GenerateControllerResponse<T>(Func<Task<DogeResponse<T>>> func)
    {
        try
        {
            var controllerResponse = await func();
            return GenerateControllerResponse(controllerResponse);
        }
        catch (Exception exception)
        {
            var response = new DogeResponse<int>
            {
                ErrorMessage = exception?.Message,
                StackTrace = exception?.StackTrace,
                StatusCode = StatusCodes.Status500InternalServerError
            };

            return ErrorResponse<int>(response);
        }
    }

    public static ObjectResult GenerateControllerResponse<T>(DogeResponse<T>? controllerResponse)
    {
        controllerResponse ??= new DogeResponse<T>();

        var hasError = !string.IsNullOrEmpty(controllerResponse?.ErrorMessage);
        return (hasError)
            ? ErrorResponse(controllerResponse)
            : new OkObjectResult(controllerResponse); // return HTTP StatusCode 200
    }

    private static ObjectResult ErrorResponse<T>(DogeResponse<T>? responseObject)
    {
        var httpStatusCode = responseObject?.StatusCode ?? StatusCodes.Status500InternalServerError;

        return new ObjectResult(responseObject)
        {
            StatusCode = httpStatusCode
        };
    }
}