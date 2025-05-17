using TTM.Core.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace TTM.Core.Infrastructure.Services.SignalRWebsocket;

public class MessagePublishService<T> : IMessagePublishService<T>
    where T : class
{
    private readonly IHubContext<MessagingHub> _messageingHubContext;

    public MessagePublishService(IHubContext<MessagingHub> messageingHubContext) =>
        _messageingHubContext = messageingHubContext;

    public Task SentToCallerAsync(T payload, string subscribeMethodName, string connecrtionID, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.Client(connecrtionID)
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task BroadcastAsync(T payload, string subscribeMethodName, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.All
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task BroadcastAsync(T payload, string subscribeMethodName, IEnumerable<string> excludedConnectionIDs, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.AllExcept(excludedConnectionIDs)
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task SendToGroupAsync(T payload, string subscribeMethodName, string groupName, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.Group(groupName)
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task SendToGroupAsync(T payload, string subscribeMethodName, string groupName, IEnumerable<string> excludedConnectionIDs, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.GroupExcept(groupName, excludedConnectionIDs)
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task SendToGroupsAsync(T payload, string subscribeMethodName, IEnumerable<string> groupNames, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.Groups(groupNames)
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task SendToUserAsync(T payload, string subscribeMethodName, string userID, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.User(userID)
            .SendAsync(subscribeMethodName, payload, cancellationToken);

    public Task SendToUsersAsync(T payload, string subscribeMethodName, IEnumerable<string> userIDs, CancellationToken cancellationToken = default) =>
        _messageingHubContext.Clients.Users(userIDs)
            .SendAsync(subscribeMethodName, payload, cancellationToken);
}

