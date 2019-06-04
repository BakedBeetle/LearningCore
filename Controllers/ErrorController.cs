using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EMPMANA.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{Statuscode}")]
        public IActionResult HttpStatusCodeHandler( int Statuscode)
        {
            switch (Statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Resource Not Found";
                    break;
            }
            return View("NotFound");
        }
    }
}