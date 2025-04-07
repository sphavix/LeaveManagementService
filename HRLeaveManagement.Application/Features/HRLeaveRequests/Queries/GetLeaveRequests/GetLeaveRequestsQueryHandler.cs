using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IAppLogger<GetLeaveRequestsQueryHandler> logger) : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger = logger;

        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all Leave Requests");

            // TODO: Check if the requester is authorized

            // Get Leave Requests and Map to Dto
            var leaveRequests = await _leaveRequestRepository.GetAsync();

            var response = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            return response;
        }
    }
}
