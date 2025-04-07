using AutoMapper;
using HRLeaveManagement.Application.Contracts.Mailing;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Application.Models.Mail;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.UpdateRequestCmd
{
    public class UpdateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailService emailService,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger) : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger = logger;

        public async Task<Unit> Handle(UpdateLeaveRequestCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Leave Request with Id - ", command.Id);

            // Get Leave Request
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(command.Id);

            if(leaveRequest is null)
            {
                throw new NotFoundException(nameof(LeaveRequest), command.Id);
            }

            var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(command);

            if(validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request, Please try again!", validationResult);
            }

            _mapper.Map(command, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // Send mail confirmation
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
            catch(Exception ex)
            {
                _logger.LogWarning("An error occured while trying to send an email", ex.Message);
                throw;
            }

            

            return Unit.Value;
        }
    }
}
