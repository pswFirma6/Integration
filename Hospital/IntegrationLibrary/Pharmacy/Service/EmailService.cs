using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.Model;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace IntegrationLibrary.Pharmacy.Service
{
    public class EmailService
    {

        public EmailService()
        {
        }

        public void SendEmail(Message message, EmailDto pharmacyEmail)
        {
            var mailMessage = CreateEmailMessage(message, pharmacyEmail);

            Send(mailMessage, pharmacyEmail);
        }

        public MimeMessage CreateEmailMessage(Message message, EmailDto pharmacyEmail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", pharmacyEmail.PharmacyEmail));

            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = "<!DOCTYPE html><body>" + message.Content + "<br></body></html>",
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage, EmailDto pharmacyEmail)
        {
            
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(pharmacyEmail.PharmacyEmail, pharmacyEmail.PharmacyPassword);
                    client.Send(mailMessage);
                }               
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
