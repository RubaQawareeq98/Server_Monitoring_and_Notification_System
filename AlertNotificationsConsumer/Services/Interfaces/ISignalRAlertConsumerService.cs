namespace AlertNotificationsConsumer.Services.Interfaces;

public interface ISignalRAlertConsumerService
{
    Task ConsumeAlertsAsync();
}