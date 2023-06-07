using Application.Common.Models;
using AutoMapper;
using Domain.IdentityEntity;

namespace Application.Common.Mapping;
public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionGetDto>().ReverseMap();
    }
}
