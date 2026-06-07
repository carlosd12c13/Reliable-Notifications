
namespace ReliableNotifications.Temporal.Workflows;

public sealed record NotificationWorkflowInput(
    string Recipient,
    string Message);