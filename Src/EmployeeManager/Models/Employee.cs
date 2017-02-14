using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManager.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public Guid? ManagerId { get; set; }
        public string Name { get; set; }      
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public Employee Manager { get; set; }
    }
}