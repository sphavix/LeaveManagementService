using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocation
{
    public class GetLeaveAllocationByIdQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper,
        IAppLogger<GetLeaveAllocationByIdQueryHandler> logger) : IRequestHandler<GetLeaveAllocationByIdQuery, LeaveAllocationDetailsDto>
    {
        private readonly ILeaveAllocationRepository _repository = leaveAllocationRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<GetLeaveAllocationByIdQueryHandler> _logger = logger;

        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching leave requested leave allocation", request.id);

            var allocation = await _repository.GetByIdAsync(request.id);

            var response = _mapper.Map<LeaveAllocationDetailsDto>(allocation);

            return response;
        }
    }
}
