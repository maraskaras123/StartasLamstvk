using Microsoft.AspNetCore.Mvc;
using StartasLamstvk.API.Services;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models;
using StartasLamstvk.Shared.Models.Enum;
using StartasLamstvk.Shared.Models.User;
using System.Net;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Routes.Users.Endpoint)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserWriteModel model)
        {
            var userId = await _userService.CreateUser(model);
            return Created(Routes.Users.UserId.Endpoint.Replace(Parameters.UserId, userId.ToString()), userId);
        }

        [HttpGet(Routes.Users.Endpoint)]
        [ProducesResponseType(typeof(List<UserReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UserReadModel>>> GetUsers(
            [FromQuery] EnumRole roleId,
            [FromQuery] List<int> userIds)
        {
            var users = await _userService.GetUsers(roleId, userIds);
            return Ok(users);
        }

        [HttpGet(Routes.Users.UserId.Endpoint)]
        [ProducesResponseType(typeof(UserReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserReadModel>> GetUser([FromRoute] int userId)
        {
            var user = await _userService.GetUser(userId);
            return user is not null ? Ok(user) : NotFound(userId);
        }

        [HttpPut(Routes.Users.UserId.Endpoint)]
        [ProducesResponseType(typeof(UserReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserWriteModel model)
        {
            var updated = await _userService.UpdateUser(userId, model);
            return updated ? Ok() : NotFound(userId);
        }

        [HttpPatch(Routes.Users.UserId.Lasf.LasfCategoryId.Endpoint)]
        [ProducesResponseType(typeof(UserReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateLasfCategory(
            [FromRoute] int userId,
            [FromRoute] EnumLasfCategory lasfCategoryId)
        {
            var updated = await _userService.UpdateLasfCategory(userId, lasfCategoryId);
            return updated ? Ok() : NotFound(userId);
        }

        [HttpPatch(Routes.Users.UserId.Moto.MotoCategoryId.Endpoint)]
        [ProducesResponseType(typeof(UserReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateMotoCategory(
            [FromRoute] int userId,
            [FromRoute] EnumMotoCategory motoCategoryId)
        {
            var updated = await _userService.UpdateMotoCategory(userId, motoCategoryId);
            return updated ? Ok() : NotFound(userId);
        }

        [HttpDelete(Routes.Users.UserId.Endpoint)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            var deleted = await _userService.DeleteUser(userId);
            return deleted ? NoContent() : NotFound(userId);
        }
    }
}