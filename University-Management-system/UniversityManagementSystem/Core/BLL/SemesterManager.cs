using System.Collections.Generic;
using UniversityManagementSystem.Core.Gateway;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.BLL
{
    public class SemesterManager
    {
        SemesterGateway semesterGateway=new SemesterGateway();
        public List<Semester> GetAll
        {
            get { return semesterGateway.GetAll; }
        }
    }
}