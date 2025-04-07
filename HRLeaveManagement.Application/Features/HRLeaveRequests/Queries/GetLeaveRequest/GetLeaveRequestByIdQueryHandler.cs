using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Queries.GetLeaveRequest
{
    public class GetLeaveRequestByIdQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IAppLogger<GetLeaveRequestByIdQueryHandler> logger) : IRequestHandler<GetLeaveRequestByIdQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<GetLeaveRequestByIdQueryHandler> _logger = logger;

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching leave request with Id - ", request.Id);

            // TODO: check if requester is authorized and add emp details.

            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            var response = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

            return response;
        }
    }
}
