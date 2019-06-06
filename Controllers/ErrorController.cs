using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMPMANA.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{Statuscode}")]
        public IActionResult HttpStatusCodeHandler( int Statuscode)
        {
            var exceptiondetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (Statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Resource Not Found";
                    _logger.LogWarning($"404 error occured  path :{exceptiondetails.OriginalPath} and QuerySting {exceptiondetails.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult ErrorLog()
        {
            var exceptiondetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"Error Path: {exceptiondetails.Path} /n Details : {exceptiondetails.Error} /n "); 

            return View();
        }
    }
}