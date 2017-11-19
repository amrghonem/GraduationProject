using GraduationProject.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace GraduationProject.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        public void AccountConfirmationEmail(string email ,string token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("amrghonem39@gmail.com"));
            message.To.Add(new MailboxAddress(email));
            message.Subject = "Account Confirmation";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<a href='"+token+"'>Click Here To Active Your Account<a/>";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587,false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                   client.Authenticate("amrghonem39@gmail.com", "abdallah00");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
