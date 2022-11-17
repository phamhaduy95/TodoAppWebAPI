using Microsoft.AspNetCore.Mvc;

namespace SharedObjects.Common
{
    public class ResponseResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public object DataObject { get; set; }
        public int StatusCode { get; set; }

        public ResponseResult()
        {
            StatusCode = 400;
        }

        public static ResponseResult GetFailResult(object obj, string message = "")
        {
            return new ResponseResult()
            {
                DataObject = obj,
                StatusCode = 400,
                Succeed = false,
                Message = message
            };
        }

        public static ResponseResult GetFailResult(string message)
        {
            return new ResponseResult
            {
                Succeed = false,
                StatusCode = 400,
                Message = message
            };
        }

        public static ResponseResult GetSuccessResult(string message = "")
        {
            return new ResponseResult
            {
                Succeed = true,
                StatusCode = 200,
                Message = message
            };
        }

        public static ResponseResult DataBaseError()
        {
            return new ResponseResult
            {
                Succeed = false,
                Message = "internal database error",
                StatusCode = 500,
            };
        }

        public static ResponseResult NotFound(object obj, string message = "not found")
        {
            return new ResponseResult
            {
                Succeed = false,
                DataObject = obj,
                Message = message,
                StatusCode = 404,
            };
        }

        public static ResponseResult NotFound(string message = "not found")
        {
            return new ResponseResult
            {
                Succeed = false,
                Message = message,
                StatusCode = 404,
            };
        }

        public ActionResult GenerateActionResult()
        {
            switch (StatusCode)
            {
                case 400: return new BadRequestObjectResult(DataObject);
                case 404: return new NotFoundObjectResult(DataObject);
                case 500: return new StatusCodeResult(500);
                case 200: return new OkResult();
                default:
                    return new OkResult();
            }
        }
    }
}