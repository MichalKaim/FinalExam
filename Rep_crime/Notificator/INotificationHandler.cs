namespace Notificator
{
    public interface INotificationHandler
    {
        void SendMail(string title, string body, string receiver);
    }
}