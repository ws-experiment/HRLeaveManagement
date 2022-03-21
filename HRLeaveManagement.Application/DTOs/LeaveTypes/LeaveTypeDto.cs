using HRLeaveManagement.Application.DTOs.LeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.Common
{
    public class LeaveTypeDto: BaseDto, ILeaveTypeDto
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
