using AutoMapper;
using Blog.Application.Features.Posts.Dtos;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Posts.Mappers
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostRequest, Post>();
            CreateMap<Post, PostDto>();
        }
    }
}