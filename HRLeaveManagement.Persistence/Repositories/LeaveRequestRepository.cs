using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public readonly LeaveManagementDbContext _dbContext;

        public LeaveRequestRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveRequest;
        }
    }
}
