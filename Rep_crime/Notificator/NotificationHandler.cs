using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notificator
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly SmtpClient _smtpClient;

        public NotificationHandler()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("FinalExamMailCrime@gmail.com", "FinalPass"),
                EnableSsl = true
            };
        }

        public void SendMail(string title, string body, string receiver)
        {
            Console.WriteLine($"Sending mail to {receiver}");

            try
            {
                _smtpClient.Send("hohohospitalnotificator@gmail.com", receiver, title, body);
                Console.WriteLine($"Mail sent to {receiver}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
