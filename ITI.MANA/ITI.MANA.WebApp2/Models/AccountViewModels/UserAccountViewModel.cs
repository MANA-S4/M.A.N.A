﻿using System;

namespace ITI.MANA.WebApp.Models.AccountViewModels
{
    public class UserAccountViewModel
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public byte[] Password { get; set; }
    }
}
