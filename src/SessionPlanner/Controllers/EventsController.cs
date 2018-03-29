using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionPlanner.Domain;
using SessionPlanner.Repositories;
using SessionPlanner.Utility;

namespace SessionPlanner.Controllers
{
    [ApiController]
    [Route("/api/events")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController( IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<PagedResultSet<EventSummary>> FindAllAsync(int pageIndex = 0)
        {
            return await _eventService.FindAllAsync(pageIndex);
        }

        [HttpGet]
        [Route("{id}", Name = "FindByIdAsync")]
        public async Task<ActionResult<Event>> FindByIdAsync(int id)
        {
            var foundEvent = await _eventService.FindByIdAsync(id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            return Ok(foundEvent);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> InsertAsync(CreateEventData data)
        {
            var operationResult = await _eventService.CreateAsync(data);

            if (!operationResult.IsValid)
            {
                ModelState.AddFromOperationResult(operationResult);
                return BadRequest(ModelState);
            }

            return Created($"/api/events/{operationResult.Result.Id}", null);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Event>> UpdateAsync(int id, UpdateEventData data)
        {
            var operationResult = await _eventService.UpdateAsync(id, data);

            if(operationResult.Result == null)
            {
                return NotFound();
            }

            if(!operationResult.IsValid)
            {
                ModelState.AddFromOperationResult(operationResult);
                return BadRequest(ModelState);
            }

            return Ok(operationResult.Result);
        }
    }
}