using System.Text;

namespace Test
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                using (var client = new HttpClient())
                {

                    try
                    {
                        var response = await client.GetAsync("https://localhost:7139/");
                    }
                    catch (HttpRequestException ex)
                    {

                        using (var fs = File.Create(@"C:\TEMP\LOG.txt"))
                        {
                            await fs.WriteAsync(Encoding.UTF8.GetBytes("Site não está no ar as: " + DateTime.Now));
                        }
                        Console.WriteLine("Arquivo de LOG gerado");
                    }

                   

                }
                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}
