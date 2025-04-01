using AutoMapper;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.CreateAllocation;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.UpdateAllocation;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocation;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocations;
using HRLeaveManagement.Application.Features.HRLeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll;
using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetLeaveType;
using HRLeaveManagement.Domain.Entities;

namespace HRLeaveManagement.Application.MappingConfiguration
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Leave Types
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();

            // Leave Allocations
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
        }
    }
}
