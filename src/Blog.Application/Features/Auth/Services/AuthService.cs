using AutoMapper;
using Blog.Application.Common.Errors;
using Blog.Application.Common.Interfaces;
using Blog.Application.Features.Auth.Dtos;
using Blog.Application.Features.Auth.Interfaces;
using Blog.Domain.Common;
using Blog.Domain.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Interfaces;
using Blog.Domain.Interfaces.Repositories;

namespace Blog.Application.Features.Auth.Services
{
    public class AuthService(
        IUserRepository _userRepository,
        IJwtTokenService _jwtTokenService,
        IPasswordHasher _passwordHasher,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IAuthService
    {
        public async Task<Result> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                return Result.Failure(AuthErrors.EmailAlreadyRegistered);
            }

            var newUser = _mapper.Map<User>(request);
            newUser.PasswordHash = _passwordHasher.Hash(request.Password);
            newUser.Role = UserRole.Author;

            await _userRepository.AddAsync(newUser, cancellationToken);
            await _unitOfWork.CommitAsync(CancellationToken.None);

            return Result.Success();
        }

        public async Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Result<LoginUserResponse>.Failure(AuthErrors.InvalidCredentials);
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email, user.Role.ToString());

            return Result<LoginUserResponse>.Success(new() { Token = token });
        }
    }
}
