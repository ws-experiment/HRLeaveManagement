using HRLeaveManagement.Application.Constants;
using HRLeaveManagement.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeaveManagementDbContext _context;

        private ILeaveAllocationRepository _leaveAllocationRepository;
        private ILeaveTypeRepository _leaveTypeRepository;
        private ILeaveRequestRepository _leaveRequestRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(LeaveManagementDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //get the private field if it is null and initialize new object instance
        public ILeaveAllocationRepository LeaveAllocationRepository =>
            _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

        public ILeaveRequestRepository LeaveRequestRepository =>
            _leaveRequestRepository ??= new LeaveRequestRepository(_context);

        public ILeaveTypeRepository LeaveTypeRepository =>
            _leaveTypeRepository ??= new LeaveTypeRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            await _context.SaveChangesAsync(username);
        }
    }
}
