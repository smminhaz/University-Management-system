using System.Collections.Generic;
using UniversityManagementSystem.Core.Gateway;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.BLL
{
    public class DesignationManager
    {
        DesignationGateway designationGateway=new DesignationGateway();
        public IEnumerable<Designation> GetAll
        {

            get { return designationGateway.GetAll; }
        } 
    }
}