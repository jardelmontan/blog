using AutoMapper;
using Blog.Api.Models;
using Blog.Application.Common.Interfaces;
using Blog.Application.Features.Posts.Dtos;
using Blog.Application.Features.Posts.Interfaces;
using Blog.Domain.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/posts")]
    public class PostsController(
        IPostService _postService,
        ICurrentUserService _currentUser,
        IMapper _mapper) : BaseApiController(_mapper)
    {
        /// <summary>
        /// Get post by id.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _postService.GetByPostIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : HandleErrorResult(result.Error);
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _postService.GetAllAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : HandleErrorResult(result.Error);
        }

        /// <summary>
        /// Create a post.
        /// </summary>
        [Authorize(Policy = Policies.Author)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
        {
            var result = await _postService.CreateAsync(_currentUser.UserId, request, cancellationToken);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
            }

            return HandleErrorResult(result.Error);
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [Authorize(Policy = Policies.Author)]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var result = await _postService.UpdateAsync(id, _currentUser.UserId, request, cancellationToken);
            return result.IsSuccess ? NoContent() : HandleErrorResult(result.Error);
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [Authorize(Policy = Policies.Author)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _postService.DeleteAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : HandleErrorResult(result.Error);
        }
    }
}