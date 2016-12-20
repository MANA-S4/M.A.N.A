using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string Email { get; }

        public string RelationType { get; set; }
    }
}
