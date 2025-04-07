using AutoMapper;
using HRLeaveManagement.Application.Contracts.Mailing;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Application.Models.Mail;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.CreateRequestCmd
{
    public class CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailService emailService,
        IAppLogger<CreateLeaveRequestCommandHandler> logger) : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger = logger; 
        public async Task<Unit> Handle(CreateLeaveRequestCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new Leave Request", command);

            // Validate
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(command);

            if(validationResult.Errors.Any())
            {
                _logger.LogWarning("An error occured while tring to create the leave request!");
                throw new BadRequestException(nameof(LeaveRequest), validationResult);
            }

            // TODO: requestor Id if Authorized.

            // TODO: Check the requestor's leave allocations

            // Create leave request
            var leaveRequest = _mapper.Map<LeaveRequest>(command);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            // Send email
            try
            {
                var mail = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request from {command.StartDate:D} to {command.EndDate:D} " +
                        $"has been updated successfully.",
                    Subject = "Leave Request Submitted"
                };

                await _emailService.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occured while trying to send an email", ex.Message);
                throw;
            }

            return Unit.Value;
        }
    }
}
