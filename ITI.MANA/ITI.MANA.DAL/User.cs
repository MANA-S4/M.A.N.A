using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL
{
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public string GoogleRefreshToken { get; set; }

        /// <summary>
        /// We have to verify the name and the possibility
        /// </summary>
        public string MicrosoftRefreshToken { get; set; }
    }
}
