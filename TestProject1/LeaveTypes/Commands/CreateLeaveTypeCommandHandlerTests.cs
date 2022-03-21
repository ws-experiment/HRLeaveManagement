using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveTypes;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Profiles;
using HRLeaveManagement.Application.Responses;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProject1.Mocks;
using Xunit;

namespace TestProject1.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        //private readonly Mock<ILeaveTypeRepository> _mockRepo;

        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            //_mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
            _mockUow = MockUnitOfWork.GetMockUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            //_handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);
            _handler = new CreateLeaveTypeCommandHandler(_mockUow.Object, _mapper);
            _leaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test DTO"
            };
        }

        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            leaveTypes.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            _leaveTypeDto.DefaultDays = -1;
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

            leaveTypes.Count.ShouldBe(2);
            result.ShouldBeOfType<BaseCommandResponse>();
            
        }
    }
}
