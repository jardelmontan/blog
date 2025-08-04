using AutoMapper;
using Blog.Application.Features.Auth;
using Blog.Application.Features.Posts.Dtos;
using Blog.Application.Features.Posts.Interfaces;
using Blog.Domain.Common;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Domain.Interfaces.Repositories;

namespace Blog.Application.Features.Posts.Services
{
    public class PostService(
        IPostRepository _postRepository,
        IUserRepository _userRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IPostService
    {
        public async Task<Result<PostDto>> GetByPostIdAsync(int postId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(postId, cancellationToken);
            if (post == null)
            {
                return Result<PostDto>.Failure(PostErrors.PostDoesNotExist);
            }

            var response = _mapper.Map<PostDto>(post);

            return Result<PostDto>.Success(response);
        }

        public async Task<Result<IEnumerable<PostDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllAsync(cancellationToken);

            var response = _mapper.Map<List<PostDto>>(posts);

            return Result<IEnumerable<PostDto>>.Success(response);
        }

        public async Task<Result<PostDto>> CreateAsync(int userId, CreatePostRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                return Result<PostDto>.Failure(AuthErrors.UserDoesNotExist);
            }

            var isTitleDuplicated = await _postRepository.IsTitleDuplicatedAsync(userId, request.Title, null, cancellationToken);
            if (isTitleDuplicated)
            {
                return Result<PostDto>.Failure(PostErrors.TitleAlreadyExists);
            }

            var post = _mapper.Map<Post>(request);
            post.UserId = userId;

            await _postRepository.AddAsync(post, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            var response = _mapper.Map<PostDto>(post);

            return Result<PostDto>.Success(response);
        }

        public async Task<Result> UpdateAsync(int postId, int userId, UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(postId, cancellationToken);
            if (post == null)
            {
                return Result.Failure(PostErrors.PostDoesNotExist);
            }

            if (post.UserId != userId)
            {
                return Result.Failure(AuthErrors.UserDoesNotHavePermission);
            }

            var isTitleDuplicated = await _postRepository.IsTitleDuplicatedAsync(userId, request.Title, postId, cancellationToken);
            if (isTitleDuplicated)
            {
                return Result.Failure(PostErrors.TitleAlreadyExists);
            }

            post.Title = request.Title;
            post.Content = request.Content;

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int postId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(postId, cancellationToken);
            if (post == null)
            {
                return Result.Failure(PostErrors.PostDoesNotExist);
            }

            _postRepository.Remove(post);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}