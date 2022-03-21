using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public readonly LeaveManagementDbContext _dbContext;

        public LeaveTypeRepository(LeaveManagementDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
