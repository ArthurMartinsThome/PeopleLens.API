using Microsoft.AspNetCore.Mvc;
using PL.Application.Interface;
using PL.Domain.Model;
using PL.Domain.Model.Filter;

namespace PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] UserFilter filter)
        {
            var result = await _userService.Search(filter);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> SearchPaged([FromQuery] UserFilter filter, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _userService.SearchPaged(filter, skip, take);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User obj)
        {
            var result = await _userService.Update(obj);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }
    }
}