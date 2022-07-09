using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monarchs.Common.Constants;
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
        private readonly ILogger<MonarchsController> _logger;

        public MonarchsController(IMonarchsCache monarchsCache, ILogger<MonarchsController> logger)
        {
            _monarchsCache = monarchsCache;
            _logger = logger;
        }

        [HttpGet("GetNumberOfMonarchs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNumberOfMonarchs()
        {
            var monarches = await _monarchsCache.GetAll();
            if (monarches != null)
                return Ok(monarches.Count());

            _logger.LogInformation(LogMessages.CouldNotFindAnyMonarch);
            return NotFound(LogMessages.CouldNotFindAnyMonarch);
        }

        [HttpGet("GetLongestRulingMonarch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Normal")]
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

            _logger.LogInformation(LogMessages.CouldNotFindAnyMonarch);
            return NotFound(LogMessages.CouldNotFindAnyMonarch);
        }

        [HttpGet("GetLongestRulingHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Normal")]
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

            _logger.LogInformation(LogMessages.CouldNotFindAnyHouse);
            return NotFound(LogMessages.CouldNotFindAnyHouse);
        }

        [HttpGet("GetMostCommonFirstName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator,Normal")]
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

            _logger.LogInformation(LogMessages.CouldNotFindOutCommonName);
            return NotFound(LogMessages.CouldNotFindOutCommonName);
        }
    }
}
