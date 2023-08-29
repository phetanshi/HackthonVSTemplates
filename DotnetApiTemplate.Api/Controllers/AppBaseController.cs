using DotnetApiTemplate.Api.Constants;
using DotnetApiTemplate.Api.Util;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiTemplate.Api.Controllers
{
    [ApiController]
    public abstract class AppBaseController : ControllerBase
    {
        public AppBaseController(IConfiguration config, ILogger logger)
        {
            Configuration = config;
            Logger = logger;
        }
        public IConfiguration Configuration { get; set; }
        public ILogger Logger { get; set; }

        [NonAction]
        protected OkObjectResult OkDone<T>(bool isSuccess, string msg, T data)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.IsSuccess = isSuccess;
            response.Message = msg;
            response.Payload = data;
            return Ok(response);
        }
        [NonAction]
        protected OkObjectResult OkDone(bool isSuccess, string msg)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = isSuccess;
            response.Message = msg;
            response.Payload = null;
            return Ok(response);
        }
        [NonAction]
        protected OkObjectResult OkDone<T>(string msg, T data)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.IsSuccess = true;
            response.Message = msg;
            response.Payload = data;
            return Ok(response);
        }
        [NonAction]
        protected OkObjectResult OkDone<T>(T data)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.IsSuccess = true;
            response.Message = AppConstants.SUCCESS;
            response.Payload = data;
            return Ok(response);
        }
        [NonAction]
        protected OkObjectResult OkDone()
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = true;
            response.Message = AppConstants.SUCCESS;
            response.Payload = null;
            return Ok(response);
        }

        [NonAction]
        protected NotFoundObjectResult ObjectNotFound()
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = false;
            response.Message = ErrorMessages.NOT_FOUNT;
            response.Payload = null;
            return NotFound(response);
        }

        [NonAction]
        protected ObjectResult InternalServerError()
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = false;
            response.Message = ErrorMessages.FAIL;
            response.Payload = null;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [NonAction]
        protected ObjectResult InternalServerError(string message)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = false;
            response.Message = message;
            response.Payload = null;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [NonAction]
        protected BadRequestObjectResult ObjectNullError()
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = false;
            response.Message = ErrorMessages.OBJECT_NULL;
            response.Payload = null;
            return BadRequest(response);
        }

        [NonAction]
        protected BadRequestObjectResult ObjectNullError(string message)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            response.IsSuccess = false;
            response.Message = message;
            response.Payload = null;
            return BadRequest(response);
        }
    }
}
