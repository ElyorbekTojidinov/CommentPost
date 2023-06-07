using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Comments.Commands;

public class UpdateCommentCommand : IRequest<bool>
{
    public Guid CommentId { get; init; }
    public string Comment { get; init; }
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, bool>
{
    private readonly IApplicationDbContext _context;

    private readonly ICurrentUserService _currentUserService;

    public UpdateCommentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
    }
       
    public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
            .FindAsync(new object[] { request.CommentId }, cancellationToken);
        if(entity == null)
        {
            throw new NotFoundException(nameof(Comment), request.CommentId);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
        
    }
}