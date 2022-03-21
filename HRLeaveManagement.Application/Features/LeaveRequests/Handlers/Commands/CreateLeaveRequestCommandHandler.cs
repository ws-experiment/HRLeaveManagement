using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequests.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Responses;
using HRLeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Contracts.Infrastructure;
using HRLeaveManagement.Application.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public CreateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IEmailSender emailSender, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            //get the user ID from claims principle, JWT token
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
                q => q.Type == "uid")?.Value;

            var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveRequestDto.LeaveTypeId),
                    "You do not have any allocation for this leave type"));
            }
            else
            {
                int daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;

                if (daysRequested > allocation.NumberOfDays)
                {
                    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
                }
            }
           

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
                leaveRequest.RequestingEmployeeId = userId;

                // Newly added
                leaveRequest.DateRequested = DateTime.Now;

                leaveRequest = await _unitOfWork.LeaveRequestRepository.Add(leaveRequest);
                await _unitOfWork.Save();
             
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;

                // System.IdentityModel.Tokens.Jwt;
                var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

                var email = new Email
                {
                    To = emailAddress,
                    Body = $"Your Leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                            $"has been submitted successfully",
                    Subject = "Leave Request Submitted"
                };

                try
                {
                    await _emailSender.SendEmail(email);
                }
                catch (Exception ex)
                {
                    // Log or handle error, but don't throw
                }
            }

            return response;
        }
    }
}
