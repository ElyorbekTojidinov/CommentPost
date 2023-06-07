using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping;
public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentGetDto>().ReverseMap();
    }
}
