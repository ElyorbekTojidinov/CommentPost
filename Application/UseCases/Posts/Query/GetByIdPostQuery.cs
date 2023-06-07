using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Posts.Query;
public class GetByIdPostQuery : IRequest<PostGetDto>
{
    public Guid PostId { get; set; }
}

public class GetByIdPostQueryHandler : IRequestHandler<GetByIdPostQuery, PostGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdPostQueryHandler(IApplicationDbContext context, IMapper mapper)
           => (_context, _mapper) = (context, mapper);

    public async Task<PostGetDto> Handle(GetByIdPostQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts.FindAsync(new object[] { request.PostId }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Post), request.PostId);

        var result = _mapper.Map<PostGetDto>(entity);
        return result;
    }
}