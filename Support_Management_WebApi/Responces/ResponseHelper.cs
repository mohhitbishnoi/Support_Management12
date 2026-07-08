        using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Support_Management_WebApi.Responces;

public static class ResponseHelper
{
    public static int StatusCode { get; set; }

    public static IActionResult GenerateResponse<T>(Result<T> request)
    {
        var result = new
        {
            data = request.Data,
            code = request.Code,
            message = request.Message,
            token = request.Token,
            isSuccesed = request.IsSuccess,
            exception = request.Exception
        };
        if (request.IsSuccess)
        {
            return new OkObjectResult(result);
        }
        else
        {
            return new OkObjectResult(result);
            {
                StatusCode = request.Code;
            }
        }
    }

}
