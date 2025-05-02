# Server Monitoring and Notification System
A distributed system built in C# that monitors server resource usage, detects anomalies or high resource consumption, and sends real-time alerts via SignalR. The system consists of three main services:

## Features

- Real-time server statistics collection
  RabbitMQ-based messaging
- Anomaly detection using configurable thresholds
- MongoDB persistence
- SignalR-based alert sending
- Console-based alert consumers

## Task 1: Server Statistics Collection Service

Collects:
- Memory usage & available memory
- CPU usage  
Then publishes statistics to a **RabbitMQ** queue at regular intervals.

### How to Run:
1. Start **RabbitMQ server**:
   ```bash
   cd ServerStatisticsCollectionService
   docker compose up
   dotnet run
## Task 2: Message Processing & Anomaly Detection Service
Consumes stats from RabbitMQ, saves them to MongoDB, and detects anomalies based on thresholds.

 ### Anomaly Detection Logic
 // Memory anomaly
if (CurrentMemoryUsage > PreviousMemoryUsage * (1 + MemoryUsageAnomalyThresholdPercentage))

// CPU anomaly
if (CurrentCpuUsage > PreviousCpuUsage * (1 + CpuUsageAnomalyThresholdPercentage))
// High memory usage
if ((CurrentMemoryUsage / (CurrentMemoryUsage + CurrentAvailableMemory)) > MemoryUsageThresholdPercentage)

// High CPU usage
if (CurrentCpuUsage > CpuUsageThresholdPercentage)

#### When an anomaly or high usage is detected, an alert is sent via SignalR to all connected clients.
## Task 3: SignalR Event Consumer Service
Each instance connects to the SignalR Hub and listens for alert messages in real-time.
### To start SignalR server run SignalRAlertNotificationService project.
```bash
  cd SignalRAlertNotificationService
  dotnet run
## Then to test alert consumers run AlertNotificationsConsumer project.
