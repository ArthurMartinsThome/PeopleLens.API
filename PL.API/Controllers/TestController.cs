using Microsoft.AspNetCore.Mvc;
using PL.Application.Interface;
using PL.Domain.Dto.CreateTestDto;

namespace PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTests()
        {
            var result = await _testService.Search(new Domain.Model.Filter.TestFilter());

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpGet("test-full")]
        public async Task<IActionResult> getTestFull([FromQuery] int testId)
        {
            var result = await _testService.GetTestFull(testId);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTest([FromBody] TestCreateRequestDto obj)
        {
            var result = await _testService.CreateTest(obj);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }
    }
}