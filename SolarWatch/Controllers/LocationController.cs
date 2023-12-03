using Microsoft.AspNetCore.Mvc;

namespace SolarWatch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LocationController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


}