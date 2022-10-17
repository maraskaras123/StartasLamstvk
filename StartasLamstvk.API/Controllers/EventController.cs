using Microsoft.AspNetCore.Mvc;
using StartasLamstvk.API.Services;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Event;
using StartasLamstvk.Shared.Models.User;
using System.Net;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost(Routes.Events.Endpoint)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<int>> CreateEvent([FromBody] EventWriteModel model)
        {
            var eventId = await _eventService.CreateEvent(model);
            return Created(Routes.Events.EventId.Endpoint.Replace(Parameters.EventId, eventId.ToString()), eventId);
        }

        [HttpGet(Routes.Events.Endpoint)]
        [ProducesResponseType(typeof(List<EventReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<EventReadModel>>> GetEvents()
        {
            var events = await _eventService.GetEvents();
            return Ok(events);
        }

        [HttpGet(Routes.Events.EventId.Endpoint)]
        [ProducesResponseType(typeof(EventReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserReadModel>> GetEvent([FromRoute] int eventId)
        {
            var @event = await _eventService.GetEvent(eventId);
            return @event is not null ? Ok(@event) : NotFound(eventId);
        }

        [HttpPut(Routes.Events.EventId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateEvent([FromRoute] int eventId, [FromBody] EventWriteModel model)
        {
            var updated = await _eventService.UpdateEvent(eventId, model);
            return updated ? Ok() : NotFound(eventId);
        }

        [HttpDelete(Routes.Events.EventId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteEvent([FromRoute] int eventId)
        {
            var deleted = await _eventService.DeleteEvent(eventId);
            return deleted ? NoContent() : NotFound(eventId);
        }
    }
}