
using ReliableNotifications.Application.Notifications;
using Temporalio.Activities;

namespace ReliableNotifications.Temporal.Activities;

public sealed class NotificationActivities
{
    private readonly INotificationSender _notificationSender;

    public NotificationActivities(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    [Activity]
    public Task<string> SendNotificationAsync(
        SendNotificationActivityInput input)
    {
        return _notificationSender.SendAsync(
            input.Recipient,
            input.Message);
    }
}
