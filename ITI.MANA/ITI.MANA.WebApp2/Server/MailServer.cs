using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ITI.MANA.DAL;

namespace ITI.MANA.WebApp.Server
{
    public class MailServer
    {
        /// <summary>
        /// MailServer constructor
        /// </summary>
        public MailServer()
        {          
        }

        /// <summary>
        /// Allow to send confirmation inscription email
        /// </summary>
        /// <param name="email"></param>
        public void SendMail(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Julie Laco", "cookit2015m@gmail.com"));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = "Confirmation d'inscription";
            message.Body = new TextPart("plain")
            {
                Text = @"Félicitation pour votre inscription ! M.A.N.A vous remercie pour votre attention à son égard."
            };

            using(var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 465, true);
                
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("cookit2015m@gmail.com", "intechinfo");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
