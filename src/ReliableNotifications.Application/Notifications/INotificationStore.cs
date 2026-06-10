
using ReliableNotifications.Domain.Notifications;

namespace ReliableNotifications.Application.Notifications;

public interface INotificationStore
{
    Task SaveAsync(
        Notification notification,
        CancellationToken cancellationToken = default);

    Task<Notification?> GetByIdAsync(
        Guid notificationId,
        CancellationToken cancellationToken = default);
}