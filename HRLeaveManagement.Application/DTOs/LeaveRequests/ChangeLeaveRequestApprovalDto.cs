﻿using HRLeaveManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveRequests
{
    public class ChangeLeaveRequestApprovalDto: BaseDto
    {
        public bool? Approved { get; set; }
    }
}
