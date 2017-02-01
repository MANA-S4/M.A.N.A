﻿using System;

namespace ITI.MANA.DAL
{
    public class Task
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public DateTime TaskDate { get; set; }

        public string AttributeTo { get; set; }

        public string Email { get; set; }
    }
}
