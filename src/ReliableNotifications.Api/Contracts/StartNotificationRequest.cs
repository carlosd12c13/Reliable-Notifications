namespace ReliableNotifications.Api.Contracts;

public sealed record StartNotificationRequest(
    string Recipient,
    string Content,
    string IdempotencyKey);
