using AutoMapper;
using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.DTOs.LeaveAllocations;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.DTOs.LeaveRequests;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.DTOs.LeaveTypes;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            //Custom mapping for certain field.
            //When mapping from the domain object to dto object, look for the destination (dto) and get the date requested field
            //and map directly to the date created field in the LeaveRequest
            CreateMap<LeaveRequest, LeaveRequestListDto>()
                .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(src => src.DateCreated))
                .ReverseMap();
            CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();

            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, CreateLeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, UpdateLeaveAllocationDto>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();

        }
        
    }
}
