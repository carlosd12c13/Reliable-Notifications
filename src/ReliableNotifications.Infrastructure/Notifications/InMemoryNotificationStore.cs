
using ReliableNotifications.Application.Notifications;
using ReliableNotifications.Domain.Notifications;
using System.Collections.Concurrent;

namespace ReliableNotifications.Infrastructure.Notifications;

public sealed class InMemoryNotificationStore : INotificationStore
{
    private readonly ConcurrentDictionary<Guid, Notification> _notifications = new();

    public Task SaveAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notification);
        cancellationToken.ThrowIfCancellationRequested();

        _notifications[notification.Id] = notification;

        return Task.CompletedTask;
    }

    public Task<Notification?> GetByIdAsync(
        Guid notificationId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _notifications.TryGetValue(notificationId, out var notification);

        return Task.FromResult(notification);
    }
}
