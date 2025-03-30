using AutoMapper;
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
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();

            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        }
    }
}
