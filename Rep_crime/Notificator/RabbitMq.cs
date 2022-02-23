using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Notificator
{

    public  class RabbitMq
    {
        private readonly INotificationHandler _notificator;

        public RabbitMq(INotificationHandler notificator)
        {
            Console.WriteLine("Run RabbitMQ");
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
                        var message = Encoding.UTF8.GetString(body).Trim('"');
                        Console.WriteLine($"Message: {message}");

                        _notificator.SendMail("Incident Reported!", $"Incident Reported!", message);
                    };
                    channel.BasicConsume(queue: "report_queue", autoAck: true, consumer: resultsConsumer);

                    while (true)
                    {

                    }
                }
            }
        }
    }
}
