using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Models
{
    public class DogeServiceControllerResponse<T>
    {
        public T? Results { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public int? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
    }

    public static class DogeServiceResponse
    {
        public static async Task<ObjectResult> GenerateControllerResponse<T>(Func<Task<DogeServiceControllerResponse<T>>> func)
        {
            try
            {
                var controllerResponse = await func();
                controllerResponse = controllerResponse ?? new DogeServiceControllerResponse<T>();

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

        private static ObjectResult ErrorResponse<T>(DogeServiceControllerResponse<T> responseObject)
        {
            var httpStatusCode = responseObject?.StatusCode ?? StatusCodes.Status500InternalServerError;

            return new ObjectResult(responseObject)
            {
                StatusCode = httpStatusCode
            };
        }
    }
}
