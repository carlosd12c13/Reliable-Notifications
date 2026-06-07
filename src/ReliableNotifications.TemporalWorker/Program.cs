using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReliableNotifications.Application.Notifications;
using ReliableNotifications.Infrastructure.Notifications;
using ReliableNotifications.Temporal;
using ReliableNotifications.Temporal.Activities;
using ReliableNotifications.Temporal.Workflows;
using Temporalio.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<INotificationSender, FakeNotificationSender>();

builder.Services
    .AddHostedTemporalWorker(
        clientTargetHost: "localhost:7233",
        clientNamespace: "default",
        taskQueue: TemporalTaskQueues.Notifications)
    .AddScopedActivities<NotificationActivities>()
    .AddWorkflow<NotificationWorkflow>();

var host = builder.Build();

await host.RunAsync();