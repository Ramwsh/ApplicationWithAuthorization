using System.Net;
using System.Net.Mail;

namespace ApplicationWithAuthorization.Model.RegistrationLogic
{
    public class EmailSender
    {
        private string smtpUser = "rexbur221282@mail.ru";
        private string smtpPassword = "YRfSkjF5twbQjgi0LCnW";
        private string smtpServer = "smtp.mail.ru";
        private int smtpPort = 587;
        private SmtpClient smtpClient;

        public EmailSender()
        {
            smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPassword);
            smtpClient.EnableSsl = true;
        }

        public void SendMessage(string targetEmail, string message, string subject)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                try
                {
                    mailMessage.From = new MailAddress(smtpUser);
                    mailMessage.To.Add(targetEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    smtpClient.Send(mailMessage);
                }
                catch
                {

                }                
            }
        }
    }
}
