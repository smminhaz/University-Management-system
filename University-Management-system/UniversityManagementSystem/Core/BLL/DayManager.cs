using System.Collections.Generic;
using UniversityManagementSystem.Core.Gateway;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.BLL
{
    public class DayManager
    {
        DayGateway dayGateway=new DayGateway();
        public IEnumerable<Day> GetAllDays
        {
            get { return dayGateway.GetAllDays; }
        } 
    }
}