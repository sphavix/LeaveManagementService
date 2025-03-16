using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetLeaveType
{
    public class GetLeaveTypeByIdQueryHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<GetLeaveTypeByIdQuery, LeaveTypeDetailsDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await _repository.GetByIdAsync(request.Id);

            var response = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

            return response;
        }
    }
}
