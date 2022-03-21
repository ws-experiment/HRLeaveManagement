using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public UpdateLeaveRequestDto LeaveRequestDto { get; set; }

        public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }
    }
}
