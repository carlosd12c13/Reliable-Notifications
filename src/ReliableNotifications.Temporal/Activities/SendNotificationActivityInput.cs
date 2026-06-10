
namespace ReliableNotifications.Temporal.Activities;

public sealed record SendNotificationActivityInput(
    string Recipient,
    string Content);