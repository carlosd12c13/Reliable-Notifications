using ReliableNotifications.Application.Notifications;
using ReliableNotifications.Domain.Notifications;
using ReliableNotifications.Temporal.Activities;
using Temporalio.Common;
using Temporalio.Workflows;

namespace ReliableNotifications.Temporal.Workflows;

[Workflow]
public sealed class NotificationWorkflow
{
    private NotificationStatus _status = NotificationStatus.Pending;

    [WorkflowRun]
    public async Task<string> RunAsync(NotificationWorkflowInput input)
    {
        var request = new NotificationRequest(
            input.NotificationId,
            input.Recipient,
            input.Content,
            input.IdempotencyKey);

        var activityInput = new SendNotificationActivityInput(
            input.Recipient,
            input.Content);

        var activityOptions = new ActivityOptions
        {
            StartToCloseTimeout = TimeSpan.FromSeconds(10),
            RetryPolicy = new RetryPolicy
            {
                MaximumAttempts = 1
            }
        };

        var sendActivityOptions = new ActivityOptions
        {
            StartToCloseTimeout = TimeSpan.FromSeconds(10),
            RetryPolicy = new RetryPolicy
            {
                InitialInterval = TimeSpan.FromSeconds(1),
                BackoffCoefficient = 2.0F,
                MaximumInterval = TimeSpan.FromSeconds(5),
                MaximumAttempts = 3
            }
        };

        await Workflow.ExecuteActivityAsync(
            (NotificationActivities activities) =>
                activities.ValidateNotificationAsync(request),
            activityOptions);

        _status = NotificationStatus.Validated;

        await Workflow.ExecuteActivityAsync(
            (NotificationActivities activities) =>
                activities.PersistNotificationAsync(request),
            activityOptions);

        var result = await Workflow.ExecuteActivityAsync(
            (NotificationActivities activities) =>
                activities.SendNotificationAsync(activityInput),
            sendActivityOptions);

        return result;
    }
}