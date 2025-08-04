using AutoMapper;
using Blog.Api.Models;
using Blog.Application.Features.Auth.Dtos;
using Blog.Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/auth")]
    public class AuthController(
        IAuthService _authService,
        IMapper _mapper) : BaseApiController(_mapper)
    {
        /// <summary>
        /// Registers a user.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);
            return result.IsSuccess ? NoContent() : HandleErrorResult(result.Error);
        }

        /// <summary>
        /// Authenticate a user.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : HandleErrorResult(result.Error);
        }
    }
}
