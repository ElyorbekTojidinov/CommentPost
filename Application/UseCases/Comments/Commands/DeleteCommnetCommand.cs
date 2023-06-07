using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Comments.Commands;


public class DeleteCommentCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public DeleteCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
          
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if(entity is null)
        {
            throw new NotFoundException(nameof(Comment), request.Id);
        }


        _context.Comments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
