using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionPlanner.API.Utility;
using SessionPlanner.Domain;
using SessionPlanner.Infrastructure.Repositories;

namespace SessionPlanner.API.Controllers
{
    [ApiController]
    [Route("/api/events")]
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<PagedResultSet<EventSummary>> FindAllAsync(int pageIndex = 0)
        {
            return await _eventRepository.FindAllAsync(pageIndex);
        }

        [HttpGet]
        [Route("{id}", Name = "FindByIdAsync")]
        public async Task<ActionResult<Event>> FindByIdAsync(int id)
        {
            var foundEvent = await _eventRepository.FindByIdAsync(id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            return Ok(foundEvent);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> InsertAsync(CreateEventCommand data)
        {
            var operationResult = Event.Create(data);

            if (!operationResult.IsValid)
            {
                ModelState.AddFromOperationResult(operationResult);
                return BadRequest(ModelState);
            }

            await _eventRepository.InsertAsync(operationResult.Result);

            return Created($"/api/events/{operationResult.Result.Id}", null);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Event>> UpdateAsync(int id, UpdateEventCommand data)
        {
            var foundEvent = await _eventRepository.FindByIdAsync(id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            var operationResult = foundEvent.UpdateEventDetails(data);

            if (!operationResult.IsValid)
            {
                ModelState.AddFromOperationResult(operationResult);
                return BadRequest(ModelState);
            }

            await _eventRepository.UpdateAsync(foundEvent);

            return Ok(foundEvent);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var foundEvent = await _eventRepository.FindByIdAsync(id);

            if(foundEvent == null)
            {
                return NotFound();
            }

            await _eventRepository.DeleteAsync(foundEvent);

            return NoContent();
        }
    }
}