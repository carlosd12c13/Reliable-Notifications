
namespace ReliableNotifications.Temporal.Workflows;

public sealed record NotificationWorkflowInput(
    Guid NotificationId,
    string Recipient,
    string Content,
    string IdempotencyKey);