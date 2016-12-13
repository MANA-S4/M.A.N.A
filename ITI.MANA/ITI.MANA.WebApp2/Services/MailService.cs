﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.MANA.WebApp.Server;
using ITI.MANA.DAL;

namespace ITI.MANA.WebApp.Services
{
    // MailService
    public class MailService
    {
        readonly string _email;

        public MailService(string email)
        {
            _email = email;
        }

        public string Email { get; }

        /// <summary>
        /// Call method SendMail who use parameter email
        /// </summary>
        /// <param name="email"></param>
        public void SendConfirmationMail(string email)
        {
            MailServer mail = new MailServer();
            mail.SendMail(email);
        }
    }
}
