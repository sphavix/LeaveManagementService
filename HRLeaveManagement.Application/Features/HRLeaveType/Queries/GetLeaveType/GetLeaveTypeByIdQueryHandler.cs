using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
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

            if(leaveType is null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            var response = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

            return response;
        }
    }
}
