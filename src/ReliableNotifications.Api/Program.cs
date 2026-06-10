
using ReliableNotifications.Api.Contracts;
using ReliableNotifications.Domain.Notifications;
using ReliableNotifications.Temporal;
using ReliableNotifications.Temporal.Workflows;
using Temporalio.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddTemporalClient(
    clientTargetHost: "localhost:7233",
    clientNamespace: "default");

var app = builder.Build();

app.MapPost("/notifications", async (
    StartNotificationRequest request,
    ITemporalClient temporalClient) =>
{
    var workflowId = $"notification-{Guid.NewGuid():N}";
    var notificationId = Guid.NewGuid();

    await temporalClient.StartWorkflowAsync(
        (NotificationWorkflow workflow) =>
            workflow.RunAsync(
                new NotificationWorkflowInput(
                    notificationId,
                    request.Recipient,
                    request.Content,
                    request.IdempotencyKey)),
        new WorkflowOptions(
            id: workflowId,
            taskQueue: TemporalTaskQueues.Notifications));

    return Results.Accepted(
        $"/notifications/{workflowId}",
        new
        {
            WorkflowId = workflowId
        });
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.Run();