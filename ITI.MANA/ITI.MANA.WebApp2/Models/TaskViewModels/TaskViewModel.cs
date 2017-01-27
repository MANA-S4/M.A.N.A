using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.WebApp.Models.TaskViewModels
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public DateTime TaskDate { get; set; }

        public string AttributeTo { get; set; }
    }
}
