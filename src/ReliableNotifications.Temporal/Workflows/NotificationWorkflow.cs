using Temporalio.Workflows;
using ReliableNotifications.Temporal.Activities;

namespace ReliableNotifications.Temporal.Workflows;

[Workflow]
public sealed class NotificationWorkflow
{
    [WorkflowRun]
    public async Task<string> RunAsync(NotificationWorkflowInput input)
    {
        var activityInput = new SendNotificationActivityInput(
            input.Recipient,
            input.Message);

        return await Workflow.ExecuteActivityAsync(
            (NotificationActivities activities) =>
                activities.SendNotificationAsync(activityInput),
            new ActivityOptions
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(10)
            });
    }
}