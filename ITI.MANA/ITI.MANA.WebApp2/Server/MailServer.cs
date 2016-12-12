using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ITI.MANA.WebApp.Server
{
    public class MailServer
    {
        public MailServer()
        {

        }

        public void MailServerTest()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Julie Laco", "cookit2015m@gmail.com"));
            message.To.Add(new MailboxAddress("Chergui Yacine", "chergui@intechinfo.fr"));
            message.Subject = "How you doin'?";
            message.Body = new TextPart("plain")
            {
                Text = @"Hey Yacine, I just wanted to let you know that Monica 
and I were goint to go play some paintball, you in ? --Juju"
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
