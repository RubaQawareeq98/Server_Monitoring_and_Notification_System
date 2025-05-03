namespace SignalRAlertNotificationService.AlertHub;

public interface IAlertHubConsumer
{
    Task ReceiveAlert(string alertType, string message);
}