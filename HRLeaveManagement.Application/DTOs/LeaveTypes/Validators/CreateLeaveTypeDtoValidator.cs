using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs.LeaveTypes.Validators
{
    public class CreateLeaveTypeDtoValidator: AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidator()
        {
            Include(new ILeaveTypeDtoValidator());

        }
    }
}
