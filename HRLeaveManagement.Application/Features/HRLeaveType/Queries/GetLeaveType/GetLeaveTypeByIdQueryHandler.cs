using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetLeaveType
{
    public class GetLeaveTypeByIdQueryHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypeByIdQueryHandler> logger) : IRequestHandler<GetLeaveTypeByIdQuery, LeaveTypeDetailsDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypeByIdQueryHandler> _logger = logger;
        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await _repository.GetByIdAsync(request.Id);

            if(leaveType is null)
            {
                _logger.LogWarning("Could not retrieve the requested data with Id: ", request.Id);
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            // map domain entity to dto
            var response = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

            return response;
        }
    }
}
