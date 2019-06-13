using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _elist;
        public EmployeeRepository()
        {
            _elist = new List<Employee>() {

           new Employee() { id=1,Name="a",Email="a@gmail.com",Department=Dept.HR ,Contact="809782805"},
           new Employee() { id=2,Name="b",Email="b@gmail.com",Department=Dept.IT ,Contact="809782805"},
           new Employee() { id=3,Name="c",Email="c@gmail.com",Department=Dept.payroll ,Contact="809782805"}
           };
        }

        public Employee AddEmployee(Employee employee)
        {
           
            employee.id = _elist.Max(x => x.id) + 1;
            _elist.Add(employee);
            return employee;
        }

       

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _elist;
        }

        public Employee GetEmployee(int Id)
        {
            return _elist.FirstOrDefault(e => e.id == Id);   
        }

        public Employee UpdateEmployee(Employee EmployeeChanges)
        {
            Employee emp = _elist.FirstOrDefault(e => e.id == EmployeeChanges.id);
            if (emp != null)
            {
                emp.Name = EmployeeChanges.Name;
                emp.Email = EmployeeChanges.Email;
                emp.Contact = EmployeeChanges.Contact;
                emp.Department = EmployeeChanges.Department;
            }
            return emp;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee emp = _elist.FirstOrDefault(e => e.id == id);
            if(emp != null)
            {
                _elist.Remove(emp);
            }
            return emp;
        }
    }
}
