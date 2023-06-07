using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Permissions.Queries;

public class GetAllPermissionQuery : IRequest<IQueryable<PermissionGetDto>>
{

}
public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, IQueryable<PermissionGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPermissionQueryHandler(IApplicationDbContext context, IMapper mapper)
           => (_context, _mapper) = (context, mapper);

    public Task<IQueryable<PermissionGetDto>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        var entites = _context.Permissions;
        var result = _mapper.ProjectTo<PermissionGetDto>(entites);

        return Task.FromResult(result);
    }
}

