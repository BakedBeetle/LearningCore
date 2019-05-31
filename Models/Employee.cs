using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.Models
{
    public class Employee
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Dept Department { get; set; }

        public string Contact { get; set; }
    }
}
