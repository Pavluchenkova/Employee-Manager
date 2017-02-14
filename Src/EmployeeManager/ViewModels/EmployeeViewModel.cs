using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EmployeeManager.Models;


namespace EmployeeManager.ViewModels
{
    public class EmployeeViewModel
    {
        public Guid EmployeeId { get; set; }

        [Display(Name = "Manager")]
        public Guid? ManagerId { get; set; }

        [Display(Name = "Manager")]
        public String Manager { get; set; }

        [Display(Name = "First Name")]  
        [Required(ErrorMessage = "Please enter first name.")]
        [StringLength(50, ErrorMessage ="The length is too large.")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]        
        [Required(ErrorMessage = "Please enter last name.")]
        [StringLength(50, ErrorMessage = "The length is too large.")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please enter e-mail.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Please enter position.")]
        [StringLength(50, ErrorMessage = "The length is too large.")]
        public string Position { get; set; }

        public List<SelectListItem> SelectEmployeeList { get; set; }

    }
}