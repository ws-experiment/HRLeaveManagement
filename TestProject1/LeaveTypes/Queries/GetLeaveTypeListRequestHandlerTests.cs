using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests;
using HRLeaveManagement.Application.Profiles;
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

namespace TestProject1.LeaveTypes.Queries
{
    public class GetLeaveTypeListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        
        //to interact with the handler
        private readonly Mock<ILeaveTypeRepository> _mockRepo;


        public GetLeaveTypeListRequestHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();

            result.Count.ShouldBe(2);
        }
    }
}
