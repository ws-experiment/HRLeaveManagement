using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveAllocations;
using HRLeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveType
{
    public class LeaveAllocationDto: BaseDto, ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
