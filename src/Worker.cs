namespace PayStreamAggregator;

public class Worker : IHostedService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker is alive at: {time}", DateTimeOffset.Now);

            // Safe async delay (non-blocking)
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }

        _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }
}
