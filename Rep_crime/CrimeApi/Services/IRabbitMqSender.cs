namespace CrimeApi.Services
{
    public interface IRabbitMqSender
    {
        void SendMessageToNotificator(string email);
    }
}