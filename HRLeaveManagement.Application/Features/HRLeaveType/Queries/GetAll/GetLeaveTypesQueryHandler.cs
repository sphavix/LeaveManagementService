using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll
{
    public class GetLeaveTypesQueryHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypesQueryHandler> logger) : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger = logger;
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all the leave types", request);
            // get data
            var leaveTypes = await _repository.GetAsync();

            // map domain enity into the dto
            var response = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            return response;
        }
    }
}
