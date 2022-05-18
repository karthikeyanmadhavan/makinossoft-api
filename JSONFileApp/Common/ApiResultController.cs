namespace JSONFileAPI.Common
{
    public class ApiResultController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public new BadRequestObjectResult BadRequest()
        {
            return base.BadRequest(new ApiResponse<bool>(false));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new BadRequestObjectResult BadRequest(object code)
        {
            return base.BadRequest(new ApiResponse<bool>(false, (int)code));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult InternalServerError()
        {
            return new OkObjectResult(new ApiResponse<bool>(false))
            { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult InternalServerError(int code)
        {
            return new OkObjectResult(new ApiResponse<bool>(false, code))
            { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new NotFoundObjectResult NotFound()
        {
            return base.NotFound(new ApiResponse<bool>(false));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new NotFoundObjectResult NotFound(int code)
        {
            return base.NotFound(new ApiResponse<bool>(false, code));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult Ok()
        {
            return base.Ok(new ApiResponse<bool>(true));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult Ok(bool status)
        {
            return base.Ok(new ApiResponse<bool>(status));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult Ok(int code)
        {
            return base.Ok(new ApiResponse<bool>(true, code));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult Ok<T>(T data)
        {
            return base.Ok(new ApiResponse<T>(data));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult Ok<T>(T data, int code)
        {
            return base.Ok(new ApiResponse<T>(data, code));
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public new OkObjectResult Ok<T>(int code, T data)
        {
            return base.Ok(new ApiResponse<T>(data, code));
        }
    }
}
