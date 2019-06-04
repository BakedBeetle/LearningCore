using EMPMANA.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Contact Number")]
        public string Contact { get; set; }

        public IFormFile photopath { get; set; }
    }
}
