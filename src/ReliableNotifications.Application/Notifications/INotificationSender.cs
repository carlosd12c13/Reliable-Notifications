
namespace ReliableNotifications.Application.Notifications;

public interface INotificationSender
{
    Task<string> SendAsync(
        string recipient,
        string content,
        CancellationToken cancellationToken = default);
}