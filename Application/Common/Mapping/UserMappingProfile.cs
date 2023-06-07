using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserGetDto>();
        CreateMap<UserGetDto, User>();
    }
}
