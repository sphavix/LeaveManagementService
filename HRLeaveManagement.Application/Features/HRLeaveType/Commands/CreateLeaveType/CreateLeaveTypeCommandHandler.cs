
using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        public async Task<int> Handle(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            // validate inputs
            var validator = new CreateLeaveTypeValidator(_repository);
            var validatorResult = await validator.ValidateAsync(command);

            if(validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid inputs, try again.");
            }

            // map domain entity to dto
            var leaveType = _mapper.Map<LeaveType>(command);

            await _repository.CreateAsync(leaveType);

            return leaveType.Id;
        }
    }
}
