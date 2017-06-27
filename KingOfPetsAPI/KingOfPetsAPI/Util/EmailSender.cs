using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
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
                string FromAddress = fromAddress;
                string FromAdressTitle = "TEST";

                //To Address
                string ToAddress = toAddress;
                string ToAdressTitle = "TEST";
                string Subject = "Hello Andone!";
                string BodyContent = "Hello Andone, this is an email sended from ASP.NET Core application! :) nice";

                //Smtp Server
                string SmtpServer = "smtp.gmail.com";

                //Smtp Port Number
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent

                };

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
    }
}
