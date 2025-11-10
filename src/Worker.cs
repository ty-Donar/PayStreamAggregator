namespace PayStreamAggregator;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.UtcNow);

        while (!stoppingToken.IsCancellationRequested)
        {
            // This is a heartbeat / placeholder for Kafka consumption
            _logger.LogInformation("Worker heartbeat at: {time}", DateTimeOffset.UtcNow);

            // Wait 5 seconds between heartbeats
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.UtcNow);
    }
}
