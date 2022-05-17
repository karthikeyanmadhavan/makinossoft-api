using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace JSONFileAPI.Common
{
    public static class ApiValidationFilter
    {
        private static string GetErrorMessage(ModelStateDictionary modelState)
        {
            string message = string.Join("; ", modelState.Values.SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            if (!string.IsNullOrEmpty(message) && message.Equals(";"))
            {
                message = string.Join("; ", modelState.Values.SelectMany(x => x.Errors)
                .Select(x => x.Exception.Message));
            }
            return message;
        }

        public static ActionResult CustomResponse(
            ActionContext context)
        {
            return new BadRequestObjectResult(new ApiResponse<bool>(false, GetErrorMessage(context.ModelState))); ;
        }
    }


}
