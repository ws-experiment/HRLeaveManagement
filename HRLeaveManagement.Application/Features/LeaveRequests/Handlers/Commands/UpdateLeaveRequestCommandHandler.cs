using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequests.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
           
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.Get(request.Id);
            if (leaveRequest is null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            if (request.LeaveRequestDto != null)
            {
                //check whether the leave request is valid
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
                if (validationResult.IsValid == false)
                {
                    throw new ValidationException(validationResult);
                }
                _mapper.Map(request.LeaveRequestDto, leaveRequest);
                await _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
                await _unitOfWork.Save();
            }

            /* Deduct a leave allocation when there's approval*/
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved.Value)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    int dayRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                    allocation.NumberOfDays -= dayRequested;
                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);
                }
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}
