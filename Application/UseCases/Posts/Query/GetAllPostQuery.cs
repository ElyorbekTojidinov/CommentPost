using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Posts.Query;

public class GetAllPostQuery : IRequest<IQueryable<PostGetDto>>
{
}

public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQuery, IQueryable<PostGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPostQueryHandler(IApplicationDbContext context, IMapper mapper)
           => (_context, _mapper) = (context, mapper);
    public async Task<IQueryable<PostGetDto>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Posts;
        var result = _mapper.ProjectTo<PostGetDto>(entities);
        return await Task.FromResult(result);
    }
}