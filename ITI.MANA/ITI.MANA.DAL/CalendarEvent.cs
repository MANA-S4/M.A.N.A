﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL
{
    public class CalendarEvent
    {

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public int EventId { get; set; }

        /// <summary>
        /// Gets the events list.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GoogleCalendarEvents"/> is private.
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

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
    }
}