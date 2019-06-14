using EMPMANA.Models;
using EMPMANA.Services;
using EMPMANA.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EMPMANA.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEmployeeRepository _employeerepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        static CloudBlobClient blobClient;
        const string blobContainerName = "webappstoragedotnet-imagecontainer";
        static CloudBlobContainer blobContainer;

        public IConfiguration Configuration { get; }
        public IAzureBlobService AzureBlobService { get; }

        public HomeController(IEmployeeRepository emp,
            IHostingEnvironment hostingEnvironment, IConfiguration configuration,IAzureBlobService azureBlobService)
        {
            _employeerepository = emp;
            this._hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            AzureBlobService = azureBlobService;
        }

        public ViewResult Index()
        {
            IEnumerable<Employee> model = _employeerepository.GetAllEmployee();
            return View("~/Views/Home/Index.cshtml", model);
        }

        public ViewResult Details(int id)
        {


            Employee empmodel = _employeerepository.GetEmployee(id);
            if (empmodel == null)
            {
                Response.StatusCode = 404;
                return View("CustomNotFoundError", id);
            }
            return View(empmodel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                string filename = ProcessUploadFile(emp); ;
                Employee employee = new Employee
                {
                    Name = emp.Name,
                    Department = emp.Department,
                    Contact = emp.Contact,
                    Email = emp.Email,
                    photopath = filename,
                };

                _employeerepository.AddEmployee(employee);
                return RedirectToAction("details", new { id = employee.id });
            }

            return View();

        }

        [HttpGet]
        public ViewResult Edit(int id = 0)
        {
            Employee employee = _employeerepository.GetEmployee(id);
            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel()
            {
                id = employee.id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                Contact = employee.Contact,
                existingpath = employee.photopath,
            };
            return View(editEmployeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                Employee model = _employeerepository.GetEmployee(emp.id);
                model.Name = emp.Name;
                model.Email = emp.Email;
                model.Department = emp.Department;
                model.Contact = emp.Contact;
                if (emp.photopath != null)
                {
                    if (emp.existingpath != null)
                    {
                        string delfilepath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", emp.existingpath);
                        System.IO.File.Delete(delfilepath);
                    }
                    model.photopath = ProcessUploadFile(emp);
                }


                _employeerepository.UpdateEmployee(model);
                return View("~/Views/Account/Login.cshtml");
            }

            return View();

        }

        private string ProcessUploadFile(CreateEmployeeViewModel emp)
        {
            string filename;
            string Upload = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
            filename = Guid.NewGuid() + "_" + emp.photopath.FileName;
            string filepath = Path.Combine(Upload, filename);
            using (FileStream filestream = new FileStream(filepath, FileMode.Create))
            {
                emp.photopath.CopyTo(filestream);
            }

            return filename;
        }

    }
}