using Microsoft.AspNetCore.Mvc;
using SharedObjects.Common;

namespace SharedObjects.Utils
{
    public static class ActionResultGenerator
    {
        public static IActionResult GetActionResultFrom(ResponseResult result)
        {
            var statusCode = result.StatusCode;
            var messageObj = new ErrorMessage(result.Message);

            switch (statusCode)
            {
                case 404: return new NotFoundObjectResult(messageObj);
                case 500: return new StatusCodeResult(500);
                case 200: return new OkObjectResult(null);
                default:
                    return new BadRequestObjectResult(messageObj);
            }
        }
    }
}