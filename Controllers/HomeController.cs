using EMPMANA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            IEnumerable<Employee> model = _employeerepository.GetAllEmployee();
            return View("~/Views/Home/Index.cshtml", model);
        }

        public ViewResult Details(int? id)
        {
            Employee empmodel = _employeerepository.GetEmployee(id ?? 1);
            return View(empmodel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _employeerepository.AddEmployee(emp);
                return RedirectToAction("details", new { id = emp.id });
            }

            return View();

        }
    }
}