﻿using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users.Queries;

public class GetAllUserQuery : IRequest<IQueryable<UserGetDto>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, IQueryable<UserGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
     => (_context, _mapper) = (context, mapper);

    public async Task<IQueryable<UserGetDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Users.Include(x => x.Roles);
        var result = _mapper.ProjectTo<UserGetDto>(entities);
        return result;
    }
}
