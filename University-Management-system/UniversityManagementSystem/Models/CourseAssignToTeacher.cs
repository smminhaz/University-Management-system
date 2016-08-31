using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class CourseAssignToTeacher
    {
        public int Id { get; set; }
        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }
        [DisplayName("Teacher")]
        [Required]
        public int TeacherId { get; set; }
        [DisplayName("Credit To be Taken")]
        public float CreditTobeTaken { get; set; }
          [Required]
        [DisplayName("Remaining Credit")]
        public float CreditTaken { get; set; }
          [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }
         [Required]
        public string Name { get; set; }
         [Required]
        public float Credit { get; set; }
        public bool Status { get; set; }
    }
}