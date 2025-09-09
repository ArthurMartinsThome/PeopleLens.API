using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Application.Interface;
using PL.Domain.Model;
using PL.Domain.Model.Filter;

namespace PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Company obj)
        {
            var result = await _companyService.Insert(obj);
            
            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] CompanyFilter filter)
        {
            var result = await _companyService.Search(filter);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> SearchPaged([FromQuery] CompanyFilter filter, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _companyService.SearchPaged(filter, skip, take);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Company obj)
        {
            var result = await _companyService.Update(obj);

            return StatusCode(result.StatusCode.GetHashCode(), result);
        }
    }
}