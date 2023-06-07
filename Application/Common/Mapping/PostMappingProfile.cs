using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping;
public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostGetDto>().ReverseMap();
    }
}
