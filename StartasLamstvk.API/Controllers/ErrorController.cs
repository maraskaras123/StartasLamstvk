using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route(Routes.Errors.Endpoint)]
        public ErrorResponse ErrorDevelopment()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = HttpStatusCode.InternalServerError; // Default status code
            var result = new ErrorResponse();

            switch (exception)
            {
                /*case ForbiddenAccessException ex:
                    code = HttpStatusCode.Forbidden;
                    result.SetInfo(code, new Error(ex.Message, true));
                    break;*/

                case UnauthorizedAccessException ex:
                    code = HttpStatusCode.Unauthorized;
                    result.SetInfo(code, new Error(ex.Message, true));
                    break;

                case ValidationException ex:
                    code = HttpStatusCode.BadRequest;
                    result.SetInfo(code, new Error(ex.Message, false));
                    break;
                default:
                    result.SetInfo(code, GetFormedErrors(exception));
                    break;
            }

            Response.StatusCode = (int)code;
            return result;
        }

        private static List<Error> GetFormedErrors<T>(T exception)
            where T : Exception
        {
            var errors = new List<Error> { new (exception.Message, true) };
            if (!string.IsNullOrEmpty(exception.InnerException?.Message))
            {
                errors.Add(new (exception.InnerException?.Message, true));
            }

            return errors;
        }
    }
}