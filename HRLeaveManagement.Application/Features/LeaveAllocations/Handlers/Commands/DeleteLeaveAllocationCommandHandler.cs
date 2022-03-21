using AutoMapper;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{

    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveTypeRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.Get(request.Id);
            if (leaveAllocation == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            await _leaveAllocationRepository.Delete(leaveAllocation);

            return Unit.Value;
        }
    }
}
