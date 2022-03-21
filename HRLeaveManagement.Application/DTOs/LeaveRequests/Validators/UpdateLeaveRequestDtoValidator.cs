using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveRequests.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
