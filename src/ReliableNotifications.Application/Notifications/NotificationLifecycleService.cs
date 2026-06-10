
using ReliableNotifications.Domain.Notifications;

namespace ReliableNotifications.Application.Notifications;

public sealed class NotificationLifecycleService
{
    private readonly INotificationStore _notificationStore;

    public NotificationLifecycleService(
        INotificationStore notificationStore)
    {
        _notificationStore = notificationStore;
    }

    public void Validate(NotificationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        _ = CreateNotification(request);
    }

    public async Task PersistValidatedAsync(
        NotificationRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var notification = CreateNotification(request);

        notification.MarkAsValidated();

        await _notificationStore.SaveAsync(
            notification,
            cancellationToken);
    }

    private static Notification CreateNotification(
        NotificationRequest request)
    {
        return new Notification(
            request.NotificationId,
            request.Recipient,
            request.Content,
            request.IdempotencyKey);
    }
}