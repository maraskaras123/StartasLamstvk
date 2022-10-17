using Microsoft.AspNetCore.Mvc;
using StartasLamstvk.API.Services;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;
using StartasLamstvk.Shared.Models.RacePreference;
using System.Net;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public class PreferenceController : ControllerBase
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        [HttpPost(Routes.Users.UserId.RacePreference.Endpoint)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<int>> CreateRacePreference(
            [FromRoute] int userId,
            [FromBody] RacePreferenceWriteModel model)
        {
            var preferenceId = await _preferenceService.CreateRacePreference(userId, model);
            return Created(Routes.Users.UserId.RacePreference.PreferenceId.Endpoint
                .Replace(Parameters.UserId, userId.ToString())
                .Replace(Parameters.PreferenceId, preferenceId.ToString()), preferenceId);
        }

        [HttpPost(Routes.Users.UserId.Preference.RaceTypeId.Endpoint)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<int>> CreatePreference(
            [FromRoute] int userId,
            [FromRoute] EnumRaceType raceTypeId)
        {
            var preferenceId = await _preferenceService.CreatePreference(userId, raceTypeId);
            return Created(Routes.Users.UserId.RacePreference.PreferenceId.Endpoint
                .Replace(Parameters.UserId, userId.ToString())
                .Replace(Parameters.PreferenceId, preferenceId.ToString()), preferenceId);
        }

        [HttpGet(Routes.Users.UserId.RacePreference.Endpoint)]
        [ProducesResponseType(typeof(List<RacePreferenceReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RacePreferenceReadModel>> GetUserRacePreferences([FromRoute] int userId)
        {
            var preferences = await _preferenceService.GetRacePreferences(userId);
            return Ok(preferences);
        }

        [HttpGet(Routes.Users.UserId.Preference.Endpoint)]
        [ProducesResponseType(typeof(List<RacePreferenceReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RacePreferenceReadModel>> GetUserPreferences([FromRoute] int userId)
        {
            var preferences = await _preferenceService.GetPreferences(userId);
            return Ok(preferences);
        }

        [HttpDelete(Routes.Users.UserId.RacePreference.PreferenceId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteRacePreference([FromRoute] int preferenceId)
        {
            var deleted = await _preferenceService.DeleteRacePreference(preferenceId);
            return deleted ? NoContent() : NotFound(preferenceId);
        }

        [HttpDelete(Routes.Users.UserId.Preference.PreferenceId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePreference([FromRoute] int preferenceId)
        {
            var deleted = await _preferenceService.DeletePreference(preferenceId);
            return deleted ? NoContent() : NotFound(preferenceId);
        }
    }
}