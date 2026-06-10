
using ReliableNotifications.Application.Notifications;
using Temporalio.Activities;
using Temporalio.Exceptions;

namespace ReliableNotifications.Temporal.Activities;

public sealed class NotificationActivities
{
    private readonly INotificationSender _notificationSender;
    private readonly NotificationLifecycleService _lifecycleService;


    public NotificationActivities(INotificationSender notificationSender, NotificationLifecycleService lifecycleService)
    {
        _notificationSender = notificationSender;
        _lifecycleService = lifecycleService;
    }

    [Activity]
    public async Task<string> SendNotificationAsync(
        SendNotificationActivityInput input)
    {
        var attempt = ActivityExecutionContext.Current.Info.Attempt;

        if (attempt <= 2)
        {
            throw new ApplicationFailureException(
                message: $"Simulated transient provider failure. Attempt: {attempt}.",
                errorType: "TransientProviderError",
                nonRetryable: false);
        }

        return await _notificationSender.SendAsync(
            input.Recipient,
            input.Content);
    }

    [Activity]
    public Task ValidateNotificationAsync(NotificationRequest request)
    {
        try
        {
            _lifecycleService.Validate(request);
            return Task.CompletedTask;
        }
        catch (ArgumentException exception)
        {
            throw new ApplicationFailureException(
                message: exception.Message,
                inner: exception,
                errorType: "InvalidNotification",
                nonRetryable: true);
        }
    }

    [Activity]
    public Task PersistNotificationAsync(
    NotificationRequest request)
    {
        return _lifecycleService.PersistValidatedAsync(request);
    }
}
