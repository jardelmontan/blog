using AutoMapper;
using Blog.Api.Models;
using Blog.Domain.Errors;

namespace Blog.Api.Mappers
{
    public class ErrorMappingProfile : Profile
    {
        public ErrorMappingProfile()
        {
            CreateMap<ResultError, ErrorResponse>();
            CreateMap<PropertyDetail, PropertyErrorDetail>();
        }
    }
}
