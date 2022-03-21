using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
        Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);
        Task<LeaveRequestVM> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);
    }
}
