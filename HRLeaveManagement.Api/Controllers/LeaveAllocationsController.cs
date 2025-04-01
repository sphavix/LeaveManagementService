using HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.CreateAllocation;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.UpdateAllocation;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocation;
using HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocations;
using HRLeaveManagement.Application.Features.HRLeaveType.Commands.DeleteLeaveType;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _meditor = mediator;

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> GetAllocations(bool isLoggedInUser = false)
        {
            var allocations = await _meditor.Send(new GetLeaveAllocationsQuery());

            return Ok(allocations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> GetAllocation(int id)
        {
            var allocation = await _meditor.Send(new GetLeaveAllocationByIdQuery(id));

            if(allocation is null)
            {
                return NotFound();
            }

            return Ok(allocation);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateAllocation(CreateLeaveAllocationCommand command)
        {
            var response = await _meditor.Send(command);

            return CreatedAtAction(nameof(GetAllocation), new { id = command.LeaveTypeId }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAllocation(UpdateLeaveAllocationCommand commad)
        {
            await _meditor.Send(commad);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAllocation(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            await _meditor.Send(command);

            return NoContent();
        }
    }
}
