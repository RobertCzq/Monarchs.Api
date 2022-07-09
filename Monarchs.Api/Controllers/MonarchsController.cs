using Microsoft.AspNetCore.Mvc;
using Monarchs.Common.Interfaces;
using Monarchs.Common.ViewModels;
using System.Net.Mime;

namespace Monarchs.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MonarchsController : ControllerBase
    {
        private readonly IMonarchsCache _monarchsCache;

        public MonarchsController(IMonarchsCache monarchsCache)
        {
            _monarchsCache = monarchsCache;
        }

        [HttpGet("GetNumberOfMonarchs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNumberOfMonarchs()
        {
            var monarches = await _monarchsCache.GetAll();
            if (monarches != null)
                return Ok(monarches.Count());

            return NotFound("Could not find any monarch!");
        }

        [HttpGet("GetLongestRulingMonarch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLongestRulingMonarch()
        {
            var monarches = await _monarchsCache.GetAll();
            if (monarches != null && monarches.Any())
            {
                var longestRulingMonarch = monarches.OrderByDescending(m => m.NrOfYearsRuled).FirstOrDefault();
                if (longestRulingMonarch != null)
                {
                    return Ok(new MonarchViewModel(longestRulingMonarch.FullName, longestRulingMonarch.NrOfYearsRuled));
                }
            }

            return NotFound("Could not find any monarch!");
        }

        [HttpGet("GetLongestRulingHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLongestRulingHouse()
        {
            var monarches = await _monarchsCache.GetAll();
            if (monarches != null && monarches.Any())
            {
                var longestRulingHouse = monarches.GroupBy(m => m.House)
                                                 .Select(houseGroup => new HouseViewModel(houseGroup.Key, 
                                                                                          houseGroup.ToList().Sum(item => item.NrOfYearsRuled)))
                                                 .OrderByDescending(hvm => hvm.NrOfYearsRuled)
                                                 .FirstOrDefault();
                if (longestRulingHouse != null)
                {
                    return Ok(longestRulingHouse);
                }
            }

            return NotFound("Could not find any house!");
        }

        [HttpGet("GetMostCommonFirstName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMostCommonFirstName()
        {
            var monarches = await _monarchsCache.GetAll();
            if (monarches != null && monarches.Any())
            {
                var mostCommonFirstName = monarches.GroupBy(m => m.FirstName)
                                                   .OrderByDescending(gr => gr.ToList().Count)
                                                   .FirstOrDefault();
                if (mostCommonFirstName != null)
                {
                    return Ok(mostCommonFirstName.Key);
                }
            }

            return NotFound("Could not find out the most common first name!");
        }
    }
}
