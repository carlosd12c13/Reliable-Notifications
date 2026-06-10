
using ReliableNotifications.Application.Notifications;

namespace ReliableNotifications.Infrastructure.Notifications;

public sealed class FakeNotificationSender : INotificationSender
{
    public async Task<string> SendAsync(
        string recipient,
        string content,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);

        Console.WriteLine(
            $"[Fake provider] Notification sent to {recipient}: {content}");

        return $"Notification sent to {recipient}";
    }
}