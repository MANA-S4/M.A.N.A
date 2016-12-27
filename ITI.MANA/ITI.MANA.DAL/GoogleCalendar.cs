using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Text;
using System.Threading;
using System.IO;

namespace ITI.MANA.DAL
{
    public class GoogleCalendar
    {
        /// <summary>
        /// Gets the events list.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public Events Events { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finish.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is finish; otherwise, <c>false</c>.
        /// </value>
        public bool IsFinish { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GoogleCalendar"/> is private.
        /// </summary>
        /// <value>
        ///   <c>true</c> if private; otherwise, <c>false</c>.
        /// </value>
        public bool Private { get; set; }

        /// <summary>
        /// Gets or sets the event members.
        /// </summary>
        /// <value>
        /// The event members.
        /// </value>
        public string Members { get; set; }
    }
}
