using MessageProcessingAndAnomalyDetectionService.Models.Enums;

namespace MessageProcessingAndAnomalyDetectionService.Models;

public class Alert
{
    public AlertType AlertType { get; set; }
    public string Message { get; set; }
}
