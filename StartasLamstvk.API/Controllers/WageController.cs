using Microsoft.AspNetCore.Mvc;
using StartasLamstvk.API.Services;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Wage;
using System.Net;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public class WageController : ControllerBase
    {
        private readonly IWageService _wageService;

        public WageController(IWageService wageService)
        {
            _wageService = wageService;
        }

        [HttpPost(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Wages.Endpoint)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<int>> CreateWage(
            [FromRoute] int eventId,
            [FromRoute] int raceOfficialId,
            [FromBody] WageWriteModel model)
        {
            var wageId = await _wageService.CreateWage(eventId, raceOfficialId, model);
            return Created(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Wages.WageId.Endpoint
                .Replace(Parameters.EventId, eventId.ToString())
                .Replace(Parameters.RaceOfficialId, raceOfficialId.ToString())
                .Replace(Parameters.WageId, wageId.ToString()), wageId);
        }

        [HttpGet(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Wages.Endpoint)]
        [ProducesResponseType(typeof(List<WageReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<WageReadModel>>> GetWages(
            [FromRoute] int eventId,
            [FromRoute] int raceOfficialId)
        {
            var wages = await _wageService.GetWages(eventId, raceOfficialId);
            return Ok(wages);
        }

        [HttpPut(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Wages.WageId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateWage(
            [FromRoute] int eventId,
            [FromRoute] int raceOfficialId,
            [FromRoute] int wageId,
            [FromBody] WageWriteModel model)
        {
            var updated = await _wageService.UpdateWage(eventId, raceOfficialId, wageId, model);
            return updated ? Ok() : NotFound(wageId);
        }

        [HttpPatch(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Wages.WageId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateWageStatus(
            [FromRoute] int eventId,
            [FromRoute] int raceOfficialId,
            [FromRoute] int wageId,
            [FromBody] WageWriteModel model)
        {
            var updated = await _wageService.UpdateWage(eventId, raceOfficialId, wageId, model);
            return updated ? Ok() : NotFound(wageId);
        }

        [HttpDelete(Routes.Events.EventId.RaceOfficials.RaceOfficialId.Wages.WageId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteWage(
            [FromRoute] int eventId,
            [FromRoute] int raceOfficialId,
            [FromRoute] int wageId)
        {
            var deleted = await _wageService.DeleteWage(eventId, raceOfficialId, wageId);
            return deleted ? NoContent() : NotFound(wageId);
        }
    }
}