using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Posts.Commands;

public class UpdatePostCommand : IRequest<bool>
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public UpdatePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
           => (_context, _currentUserService) = (context, currentUserService);

    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FindAsync(request.PostId);
        if (post == null)
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        post.Title = request.Title;
        post.Content = request.Content;
        post.LastUpdated = DateTime.UtcNow;
        post.LastUpdatedBy = _currentUserService.UserName;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
