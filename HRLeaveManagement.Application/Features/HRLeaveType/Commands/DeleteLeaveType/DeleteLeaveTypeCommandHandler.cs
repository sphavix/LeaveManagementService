using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _repository.GetByIdAsync(request.Id);

            if(leaveType is null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            await _repository.DeleteAsync(leaveType);

            return Unit.Value;
        }
    }
}
