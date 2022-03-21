using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocations.Validator
{
    public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveAllocationRepository;
        public UpdateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;

            Include(new ILeaveAllocationDtoValidator(_leaveAllocationRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
