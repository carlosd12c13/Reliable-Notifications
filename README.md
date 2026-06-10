# Reliable Notifications

Laboratorio en .NET 10 y C# para explorar Temporal como motor de
orquestacion de procesos confiables.

El proyecto modela el envio de notificaciones como un workflow: una API recibe
la solicitud, inicia la ejecucion en Temporal y un worker procesa las
actividades de validacion, persistencia y envio simulado.

## Contenido

- `ReliableNotifications.Api`: endpoint HTTP para iniciar workflows de
  notificacion.
- `ReliableNotifications.Temporal`: definicion del workflow, activities y task
  queue.
- `ReliableNotifications.TemporalWorker`: worker hospedado que registra y
  ejecuta workflows/activities.
- `ReliableNotifications.Application`, `Domain` e `Infrastructure`: logica de
  negocio, modelo de notificacion y adaptadores en memoria/fake para el
  laboratorio.

El objetivo no es ser un sistema de notificaciones listo para produccion, sino
un espacio pequeno para entender como Temporal coordina trabajo, reintentos y
separacion entre orquestacion y ejecucion.
