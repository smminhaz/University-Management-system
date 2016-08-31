using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a valid course code!")]
       [StringLength(100,MinimumLength = 5,ErrorMessage = "Code must be contains 5 characters!")]
        public string Code { get; set; }
         [Required(ErrorMessage = "Please enter a valid course name!")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Please enter a valid Course credit!")]
        [Range(0.5,5.00,ErrorMessage = "Credit must be betwwen 0.5 and 5.00")]
        public double Credit { get; set; }
          [Required(ErrorMessage = "Please write course description!")]
        public string Description { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "Please! Select related department!")]
        public int DepartmentId { get; set; }
        [DisplayName("Semester")]
        [Required(ErrorMessage = "Please! Select related semester!")]
        public int SemesterId { get; set; }
    }
}