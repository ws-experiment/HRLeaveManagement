using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveRequests;
using HRLeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs
{
    public class LeaveRequestDto: BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Employee Employee{ get; set; }
        public string RequestingEmployeeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public string RequestComments { get; set; }
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
    }
}
