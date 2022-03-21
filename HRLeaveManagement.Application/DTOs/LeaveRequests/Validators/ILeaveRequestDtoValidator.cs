using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveRequests.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        //when requesting leave, need to check whether specific leave type is exist or not
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.StartDate)
                  .LessThan(DateTime.Now).WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(p => p.EndDate)
                  .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveRequestExists = await _leaveTypeRepository.Exists(id);
                    return leaveRequestExists;
                })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
