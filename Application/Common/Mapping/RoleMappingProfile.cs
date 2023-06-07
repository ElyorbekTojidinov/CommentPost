using Application.Common.Models;
using AutoMapper;
using Domain.IdentityEntity;

namespace Application.Common.Mapping;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleGetDto>().ReverseMap();
    }
}
