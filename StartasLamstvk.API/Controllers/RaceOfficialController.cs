using Microsoft.AspNetCore.Mvc;
using StartasLamstvk.API.Services;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.RaceOfficials;
using System.Net;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public class RaceOfficialController : ControllerBase
    {
        private readonly IRaceOfficialService _raceOfficialService;

        public RaceOfficialController(IRaceOfficialService raceOfficialService)
        {
            _raceOfficialService = raceOfficialService;
        }

        [HttpPost(Routes.Events.EventId.RaceOfficials.Endpoint)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<int>> CreateRaceOfficial(
            [FromRoute] int eventId,
            [FromBody] RaceOfficialWriteModel model)
        {
            var raceOfficialId = await _raceOfficialService.CreateRaceOfficial(eventId, model);
            return Created(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Endpoint
                .Replace(Parameters.EventId, eventId.ToString())
                .Replace(Parameters.RaceOfficialId, raceOfficialId.ToString()), raceOfficialId);
        }

        [HttpGet(Routes.Events.EventId.RaceOfficials.Endpoint)]
        [ProducesResponseType(typeof(List<RaceOfficialReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<RaceOfficialReadModel>>> GetRaceOfficials([FromRoute] int eventId)
        {
            var raceOfficials = await _raceOfficialService.GetRaceOfficials(eventId);
            return Ok(raceOfficials);
        }

        [HttpPut(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateRaceOfficial(
            [FromRoute] int eventId,
            [FromRoute] int raceOfficialId,
            [FromBody] RaceOfficialWriteModel model)
        {
            var updated = await _raceOfficialService.UpdateRaceOfficial(eventId, raceOfficialId, model);
            return updated ? Ok() : NotFound(raceOfficialId);
        }

        [HttpDelete(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteRaceOfficial([FromRoute] int eventId, [FromRoute] int raceOfficialId)
        {
            var deleted = await _raceOfficialService.DeleteRaceOfficial(eventId, raceOfficialId);
            return deleted ? NoContent() : NotFound(eventId);
        }
    }
}