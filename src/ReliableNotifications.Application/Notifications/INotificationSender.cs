
namespace ReliableNotifications.Application.Notifications;

public interface INotificationSender
{
    Task<string> SendAsync(
        string recipient,
        string message,
        CancellationToken cancellationToken = default);
}