namespace TTM.Core.Infrastructure.Services.SignalRWebsocket;
public interface IMessagePublishService<T>
    where T : class
{
    Task SentToCallerAsync(
        T payload,
        string subscribeMethodName,
        string connecrtionID,
        CancellationToken cancellationToken = default);

    Task BroadcastAsync(
        T payload,
        string subscribeMethodName,
        CancellationToken cancellationToken = default);

    Task BroadcastAsync(
        T payload,
        string subscribeMethodName,
        IEnumerable<string> excludedConnectionIDs,
        CancellationToken cancellationToken = default);

    Task SendToGroupAsync(
        T payload,
        string subscribeMethodName,
        string groupName,
        CancellationToken cancellationToken = default);

    Task SendToGroupAsync(
        T payload,
        string subscribeMethodName,
        string groupName,
        IEnumerable<string> excludedConnectionIDs,
        CancellationToken cancellationToken = default);

    Task SendToGroupsAsync(
        T payload,
        string subscribeMethodName,
        IEnumerable<string> groupNames,
        CancellationToken cancellationToken = default);

    Task SendToUserAsync(
        T payload,
        string subscribeMethodName,
        string userID,
        CancellationToken cancellationToken = default);

    Task SendToUsersAsync(
        T payload,
        string subscribeMethodName,
        IEnumerable<string> userIDs,
        CancellationToken cancellationToken = default);
}