using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace EmpFormMVC.Models
{
    public class Employee
    {
        
        public int id { get; set; }
        [Required(ErrorMessage ="Please enter First Name")]
        [Display(Name="First Name")]
        public string fname { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        [Display(Name = "Last Name")]
        public string lname { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Please enter Phone number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [FileExtensions(Extensions ="jpeg,png",ErrorMessage ="Please select proper format")]
        [Required(ErrorMessage = "Please upload an image")]
        [Display(Name = "Image")]
        public string img { get; set; }
    }
}