using SignalRAlertNotificationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<AlertNotificationHub>("/alertHub");

app.Run();