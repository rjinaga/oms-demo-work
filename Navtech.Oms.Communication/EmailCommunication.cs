namespace Navtech.Oms.Communication
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using IocServiceStack;

    using Navtech.Oms.Abstractions.Communication;

    [Service]
    public class EmailCommunication : IEmail
    {
        public EmailCommunication() //TODO: Pass settings to constructor through DI
        {
            // 
        }

        public void Send(EmailMessage emailMessage)
        {
            //NOTE: HARD CODED VALUES FOR DEMO PURPOSE, Production code will be different, and will get from configuration though DI
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true
            };

            MailMessage message = CreateMessage(emailMessage);

            // Send
            client.Send(message);
        }

        private MailMessage CreateMessage(EmailMessage emailMessage)
        {
            // TODO: get configuration from Web.config using DI
            MailAddress from = new MailAddress("rjinaga@gmail.com", "Rajesh Jinaga", System.Text.Encoding.UTF8);

            // Build Message
            MailMessage message = new MailMessage();
            message.From = from;
            // Add TO addresses
            Array.ForEach(emailMessage.ToAddress, address => message.To.Add(new MailAddress(address)));

            message.Body = emailMessage.Body;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            message.Subject = emailMessage.Subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            return message;
        }
    }
}
