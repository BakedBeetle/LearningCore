using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.Models
{
    public static class ModelBuilderExtensions
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        id = 1,
                        Name = "John",
                        Department = Dept.HR,
                        Email = "Doey@gmail.com",
                        Contact = "8888888888",
                    },
                        new Employee
                        {
                            id = 2,
                            Name = "Vasu",
                            Department = Dept.HR,
                            Email = "Vasu@gmail.com",
                            Contact = "8888888888",
                        }

                );
        }
    }
}
