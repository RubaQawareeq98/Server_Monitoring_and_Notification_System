using SignalRAlertNotificationService;
using SignalRAlertNotificationService.AlertHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<AlertNotificationHub>("/alertHub");

app.Run();