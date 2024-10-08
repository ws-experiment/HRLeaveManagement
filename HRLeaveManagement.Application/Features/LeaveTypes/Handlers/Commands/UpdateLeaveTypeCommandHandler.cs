﻿using AutoMapper;
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
    public class UpdateLeaveTypeCommandHandler: IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);
            if (leaveType is null)
            {
                throw new NotFoundException(nameof(leaveType), request.LeaveTypeDto.Id);
            }

            _mapper.Map(request.LeaveTypeDto, leaveType);

            await _leaveTypeRepository.Update(leaveType);

            return Unit.Value;
        }
    }
}
