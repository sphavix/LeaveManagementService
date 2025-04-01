using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationsQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper,
        IAppLogger<GetLeaveAllocationsQueryHandler> logger) : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _repository = leaveAllocationRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger = logger;
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all leave allocations.", request);

            var allocations = await _repository.GetAsync();

            var response = _mapper.Map<List<LeaveAllocationDto>>(allocations);

            return response;
        }
    }
}
