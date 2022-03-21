using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocations.Validator
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveAllocationRepository;
        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.");

            RuleFor(p => p.Period)
             .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.")
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveAllocationRepository.Exists(id);
                    return leaveTypeExists;
                })
                 .WithMessage("{PropertyName} does not exist.");
           
        }
    }
}
