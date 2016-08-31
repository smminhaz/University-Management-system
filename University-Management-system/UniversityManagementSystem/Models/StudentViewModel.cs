using System;

namespace UniversityManagementSystem.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public DateTime RegisterationDate { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }
}