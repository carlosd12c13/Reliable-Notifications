namespace ReliableNotifications.Api.Contracts;

public sealed record StartNotificationRequest(
    string Recipient,
    string Message);
