using AutoMapper;
using Blog.Application.Features.Auth.Dtos;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Auth.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserRequest, User>();
        }
    }
}
