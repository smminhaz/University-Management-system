using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a teacher name!")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Please enter a valid address!")]
        public string Address { get; set; }
         [Required(ErrorMessage = "Please enter a valid Email address!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address! Enter correct Email address!")]
        public string Email { get; set; }
         [Required(ErrorMessage = "please enter a valid contact Number!")]
         [DisplayName("Contact No.")]
        public string Contact { get; set; }
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
         [DisplayName("Department")]
        public int DepartmentId { get; set; }
         [Required(ErrorMessage = "Credit to be taken is required!")]
        [DisplayName("Credit To be Taken")]
        [RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?",ErrorMessage = "Please! enter a positive number! ")]
        public double CreditTobeTaken { get; set; }
       
         public double CreditTaken { get; set; }
    }
}