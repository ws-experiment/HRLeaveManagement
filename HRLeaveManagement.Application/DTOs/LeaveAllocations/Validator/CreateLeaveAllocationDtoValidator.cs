using FluentValidation;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocations.Validator
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                //MustAsync => Must returning something
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                    return leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exist");
            
        }
    }
}
