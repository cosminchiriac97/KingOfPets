using KingOfPetsAPI.Model;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KingOfPetsAPI.Util
{
    public class EmailSender
    {
        public static Boolean sendEmail(string fromAddress, string toAddress)
        {
            try
            {

                //From Address
                var mimeMessage = new MimeMessage();
                
                //To Address
                string ToAddress = toAddress;
                string ToAdressTitle = "TEST";
                string Subject = "Hello Andone!";
                string BodyContent = "Hello Andone, this is an email sended from ASP.NET Core application! :) nice";

                //Smtp Server
                string SmtpServer = "smtp.gmail.com";

                //Smtp Port Number
                int SmtpPortNumber = 587;

                mimeMessage.From.Add(new MailboxAddress("chiriac.cosmin97@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));

                var builder = new BodyBuilder { TextBody = "text" };
                
                builder.Attachments.Add("bin\\Debug\\netcoreapp1.1\\javaee_slide_en.pdf");
                //A:\\ProiectKINGOPET\\KingOfPets\\kingofpet\\src\\kingofpet\\
                mimeMessage.Subject = Subject;
                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("chiriac.cosmin97@gmail.com", "021897081401f25df189/");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static async void SendEmail(EmailContent emailContent)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("chiriac.cosmin97@gmail.com"));

            using (var db = new EmailModel())
            {
                var emails = db.Emails.FromSql("select * from Emails").ToList();
                foreach (Email email in emails) {
                    emailMessage.To.Add(new MailboxAddress(email.email));
                }
            }
            emailMessage.Subject = emailContent.subject;
            var builder = new BodyBuilder { TextBody = emailContent.textBody };


            //Fetch the attachments from db
            //considering one or more attachments
            if (emailContent.attachements != null)
            {
                foreach (string email in emailContent.attachements)
                {
                    builder.Attachments.Add(email);
                }
            }

            emailMessage.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = "chiriac.cosmin97@gmail.com",
                    Password = "021897081401f25df189/"
                };
                await client.ConnectAsync("smtp.gmail.com", 587).ConfigureAwait(false);
                await client.AuthenticateAsync(credentials);               
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
