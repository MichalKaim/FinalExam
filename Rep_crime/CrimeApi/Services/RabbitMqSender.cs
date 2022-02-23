using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CrimeApi.Services
{
    public class RabbitMqSender : IRabbitMqSender
    {
        private readonly IConnection connection;

        public RabbitMqSender()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            connection = factory.CreateConnection();
        }

        public void SendMessageToNotificator(string email)
        {
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare("report_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.ExchangeDeclare("report_exchange", "fanout");
                channel.QueueBind("report_queue", "report_exchange", "");
                string message = JsonConvert.SerializeObject(email);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("report_exchange", "", null, body);
            }
        }
    }
}
