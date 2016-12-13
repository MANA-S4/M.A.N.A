using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL
{
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public byte[] Password { get; set; }

        /// <summary>
        /// Gets or sets the google refresh token.
        /// </summary>
        /// <value>
        /// The google refresh token.
        /// </value>
        public string GoogleRefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the microsoft access token.
        /// </summary>
        /// <value>
        /// The microsoft access token.
        /// </value>
        public string MicrosoftAccessToken { get; set; }
    }
}
