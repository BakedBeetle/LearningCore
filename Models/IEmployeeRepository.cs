using System.Collections.Generic;

namespace EMPMANA.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);

        IEnumerable<Employee> GetAllEmployee();

        Employee AddEmployee(Employee employee);
    }
}
