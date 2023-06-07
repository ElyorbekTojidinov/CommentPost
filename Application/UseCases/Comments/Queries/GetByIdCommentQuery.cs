using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Comments.Queries;

public class GetByIdCommentQuery : IRequest<CommentGetDto>
{
    public Guid Id { get; set; }
}

public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdCommentQuery, CommentGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdCommentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CommentGetDto> Handle(GetByIdCommentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
              .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Comment), request.Id);

        var result = _mapper.Map<CommentGetDto>(entity);
        return result;
    }
}
