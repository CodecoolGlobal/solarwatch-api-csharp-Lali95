using Microsoft.AspNetCore.Mvc;
using SolarWatch.Models;
using SolarWatch.Models.Repositories;
using System;
using System.Threading.Tasks;

namespace SolarWatch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SunController : ControllerBase
    {
        private readonly SunriseSunsetRepository _sunriseSunsetRepository;

        public SunController(SunriseSunsetRepository sunriseSunsetRepository)
        {
            _sunriseSunsetRepository = sunriseSunsetRepository;
        }

        [HttpGet("times")]
        public async Task<ActionResult<SunriseSunsetRepository>> GetSunriseSunsetTimes([FromQuery] string cityName, [FromQuery] DateTime date)
        {
            try
            {
                // Get geographical coordinates for the city
                var location = await _sunriseSunsetRepository.GetGeographicalCoordinatesAsync(cityName);

                // Get sunrise and sunset times in local time
                var sunriseSunsetLocal = await _sunriseSunsetRepository.GetSunriseSunsetTimesAsync(location.Name, location.Date);

                return Ok(sunriseSunsetLocal);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("times/utc")]
        public async Task<ActionResult<SunriseSunsetRepository>> GetSunriseSunsetTimesUtc([FromQuery] string cityName, [FromQuery] DateTime date)
        {
            try
            {
                // Get geographical coordinates for the city
                var location = await _sunriseSunsetRepository.GetGeographicalCoordinatesAsync(cityName);

                // Get sunrise and sunset times in UTC
                var sunriseSunsetUtc = await _sunriseSunsetRepository.GetSunriseSunsetTimesAsync(location.Name, location.Date);

                return Ok(sunriseSunsetUtc);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
