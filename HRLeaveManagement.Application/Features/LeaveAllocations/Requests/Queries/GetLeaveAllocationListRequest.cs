using HRLeaveManagement.Application.DTOs.LeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationListRequest: IRequest<List<LeaveAllocationDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
