using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.DTOs;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, 
            IUserService userService, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }
    }
}
