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
        public static void Main(string[] args)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Marie Dupond", "marie.dupond@test.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = "How you doin'?";
            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler, I just wanted to let you know that Monica and I were goint to go play some paintball, you in ? --Juju"
            };

            using(var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.friends.com", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("juju", "password");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
