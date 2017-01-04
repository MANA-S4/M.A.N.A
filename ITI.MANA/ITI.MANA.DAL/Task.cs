﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL
{
    public class Task
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public bool IsFinish { get; set; }
    }
}
