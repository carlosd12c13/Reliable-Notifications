
namespace ReliableNotifications.Domain.Notifications;

public enum NotificationStatus
{
    Pending = 0,
    Validated = 1,
    Sending = 2,
    WaitingForConfirmation = 3,
    Delivered = 4,
    Failed = 5
}
