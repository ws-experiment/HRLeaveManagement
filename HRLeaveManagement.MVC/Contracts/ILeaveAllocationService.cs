using HRLeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
