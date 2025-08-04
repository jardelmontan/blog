using AutoMapper;
using Blog.Api.Models;
using Blog.Domain.Enums;
using Blog.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Api.Controllers
{
    public class BaseApiController(IMapper mapper) : Controller
    {
        private readonly IMapper _mapper = mapper;

        protected IActionResult HandleErrorResult(ResultError domainError)
        {
            if (domainError == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new ErrorResponse("Internal.Error", "An unexpected internal error occurred."));
            }

            var errorResponse = _mapper.Map<ErrorResponse>(domainError);

            return domainError.Type switch
            {
                ErrorType.NotFound => NotFound(errorResponse),
                ErrorType.Validation => BadRequest(errorResponse),
                ErrorType.BusinessRule => UnprocessableEntity(errorResponse),
                ErrorType.Unauthorized => Unauthorized(errorResponse),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, errorResponse)
            };
        }
    }
}
