using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest: IRequest<List<LeaveRequestListDto>>
    {
        //get the leave request list for the logged in user or for an admin
        public bool IsLoggedInUser { get; set; }
    }
}
