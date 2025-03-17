using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<DeleteLeaveTypeCommandHandler> logger) : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        private readonly IAppLogger<DeleteLeaveTypeCommandHandler> _logger = logger;

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _repository.GetByIdAsync(request.Id);

            if(leaveType is null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            _logger.LogWarning("Deleting Leave Type with Id: ", request.Id);
            await _repository.DeleteAsync(leaveType);

            return Unit.Value;
        }
    }
}
