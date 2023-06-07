using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Comments.Commands;

public class CreateCommentCommand : IRequest<Guid>
{
    public string Content { get; set; }
    public Guid PostId { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public CreateCommentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }


    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        var entity = new Comment
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            AuthorId = _currentUserService.UserId,
            PostId = request.PostId,
            Created = DateTime.UtcNow,
            CreatedBy = _currentUserService.UserName,

        };
        await _context.Comments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}