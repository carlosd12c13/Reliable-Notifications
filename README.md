# Reliable Notifications

Lab project in .NET 10 and C# for exploring Temporal as a reliable process
orchestration engine.

The project models notification delivery as a workflow: an API receives the
request, starts the execution in Temporal, and a worker processes validation,
persistence, and simulated delivery activities.

## Contents

- `ReliableNotifications.Api`: HTTP endpoint that starts notification
  workflows.
- `ReliableNotifications.Temporal`: workflow, activities, and task queue
  definitions.
- `ReliableNotifications.TemporalWorker`: hosted worker that registers and runs
  workflows and activities.
- `ReliableNotifications.Application`, `Domain`, and `Infrastructure`: business
  logic, notification model, and in-memory/fake adapters for the lab.

The goal is not to be a production-ready notification system, but a small space
to understand how Temporal coordinates work, retries, and the separation between
orchestration and execution.
