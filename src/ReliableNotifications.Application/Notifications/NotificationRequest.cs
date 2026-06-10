
namespace ReliableNotifications.Application.Notifications;

public sealed record NotificationRequest(
    Guid NotificationId,
    string Recipient,
    string Content,
    string IdempotencyKey);