using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMPMANA.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EMPMANA.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEmployeeRepository _employeerepository;

        public HomeController(IEmployeeRepository emp)
        {
            _employeerepository = emp;
        }

        public ViewResult Index()
        {
            var model = _employeerepository.GetAllEmployee();
            return View("~/Views/Home/Index.cshtml",model);
        }
        
        public ViewResult Details(int? id)
        {
            Employee empmodel = _employeerepository.GetEmployee(id??1);
            return View(empmodel);

        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(Employee emp)
        {
            _employeerepository.AddEmployee(emp);
            return RedirectToAction("details", new {id=emp.id });
        }
    }
}