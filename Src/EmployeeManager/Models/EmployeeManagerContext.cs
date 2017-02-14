using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace EmployeeManager.Models
{
    public class EmployeeManagerContext : DbContext
    {

        public EmployeeManagerContext() : base("name=EmployeeManagerContext")
        {
        }

        public System.Data.Entity.DbSet<EmployeeManager.Models.Employee> Employees { get; set; }
    
    }
}
