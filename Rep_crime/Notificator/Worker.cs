using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Notificator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INotificationHandler _notificator;

        public Worker(ILogger<Worker> logger, INotificationHandler notificator)
        {
            _logger = logger;
            _notificator = notificator;

            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "report_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var resultsConsumer = new EventingBasicConsumer(channel);


                    resultsConsumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        _logger.LogInformation($"Message: {message}");

                        _notificator.SendMail("Incident Reported!", $"Incident Reported!", message);
                    };
                    channel.BasicConsume(queue: "results_queue", autoAck: true, consumer: resultsConsumer);

                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000000000, stoppingToken);
            }
        }
    }
}