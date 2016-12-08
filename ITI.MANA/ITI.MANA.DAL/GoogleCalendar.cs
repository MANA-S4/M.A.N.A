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
        public Events Events { get; }

    }
}
