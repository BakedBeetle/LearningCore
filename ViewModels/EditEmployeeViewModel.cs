using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.ViewModels
{
    public class EditEmployeeViewModel :CreateEmployeeViewModel
    {
        public int id { get; set; }

        public string existingpath { get; set; }
    }
}
