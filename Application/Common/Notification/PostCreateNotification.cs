using Application.Common.Interfaces;
using MediatR;
using System.Diagnostics;

namespace Application.Common.Notification;


public class PostCreatedNotification : INotification
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
}

public class PostCreatedNotificationHandler : INotificationHandler<PostCreatedNotification>
{
    private readonly IApplicationDbContext _dbContext;

    public PostCreatedNotificationHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(PostCreatedNotification notification, CancellationToken cancellationToken)
    {
        Debug.Print($"{notification.Id}, {notification.UserName}");
    }
}
