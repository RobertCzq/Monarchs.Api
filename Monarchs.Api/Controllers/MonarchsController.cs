using Microsoft.AspNetCore.Mvc;
using Monarchs.Common.Interfaces;
using System.Net.Mime;

namespace Monarchs.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MonarchsController : ControllerBase
    {
        private readonly IMonarchsDataStore _monarchsDataStore;

        public MonarchsController(IMonarchsDataStore monarchsDataStore)
        {
            _monarchsDataStore = monarchsDataStore;
        }

        [HttpGet("GetNumberOfMonarchs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNumberOfMonarchs()
        {
            var nrOfMonarchs = await _monarchsDataStore.GetNumberOfMonarchs();
            if (nrOfMonarchs != null)
                return Ok(nrOfMonarchs);

            return NotFound("Could not find any monarchs!");
        }
    }
}
