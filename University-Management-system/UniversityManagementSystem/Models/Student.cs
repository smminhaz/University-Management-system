using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
         [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        
         [Required(ErrorMessage = "Email is required!")]
         [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
         [Required(ErrorMessage = "Contact is required!")]
         [DisplayName("Contact No.")]
        public string Contact { get; set; }
         [DisplayName("Date")] 
         public DateTime RegDate { get; set; }
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
    }
}