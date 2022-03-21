using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveTypes.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Responses;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //refactored at 34. Setup Leave Type Management Service
        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            //validator
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                leaveType = await _unitOfWork.LeaveTypeRepository.Add(leaveType);

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveType.Id;

                await _unitOfWork.Save();
            }
            return response;
        }
    }
}
