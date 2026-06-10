
namespace ReliableNotifications.Domain.Notifications;

public sealed class Notification
{
    public Guid Id { get; }
    public string Recipient { get; }
    public string Content { get; }
    public string IdempotencyKey { get; }
    public NotificationStatus Status { get; private set; }

    public Notification(
        Guid id,
        string recipient,
        string content,
        string idempotencyKey)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "Notification ID cannot be empty.",
                nameof(id));
        }

        if (string.IsNullOrWhiteSpace(recipient))
        {
            throw new ArgumentException(
                "Recipient is required.",
                nameof(recipient));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException(
                "Content is required.",
                nameof(content));
        }

        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            throw new ArgumentException(
                "Idempotency key is required.",
                nameof(idempotencyKey));
        }

        Id = id;
        Recipient = recipient;
        Content = content;
        IdempotencyKey = idempotencyKey;
        Status = NotificationStatus.Pending;
    }

    public void MarkAsValidated()
    {
        EnsureStatus(NotificationStatus.Pending);
        Status = NotificationStatus.Validated;
    }

    public void StartSending()
    {
        EnsureStatus(NotificationStatus.Validated);
        Status = NotificationStatus.Sending;
    }

    public void WaitForConfirmation()
    {
        EnsureStatus(NotificationStatus.Sending);
        Status = NotificationStatus.WaitingForConfirmation;
    }

    public void MarkAsDelivered()
    {
        EnsureStatus(NotificationStatus.WaitingForConfirmation);
        Status = NotificationStatus.Delivered;
    }

    public void MarkAsFailed()
    {
        if (Status is NotificationStatus.Delivered
            or NotificationStatus.Failed)
        {
            throw new InvalidOperationException(
                $"Notification cannot fail from status {Status}.");
        }

        Status = NotificationStatus.Failed;
    }

    private void EnsureStatus(NotificationStatus expectedStatus)
    {
        if (Status != expectedStatus)
        {
            throw new InvalidOperationException(
                $"Expected status {expectedStatus}, but current status is {Status}.");
        }
    }
}
