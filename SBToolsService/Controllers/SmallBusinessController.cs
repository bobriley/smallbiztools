using Microsoft.AspNetCore.Mvc;
using SBToolsService.POCOs;

namespace SBToolsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmallBusinessController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public SmallBusinessController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPut(Name = "ProcessSmallBusinesInfo")]
        public IActionResult ProcessBusinessInfo(SmallBusinessInfo smallBusinessInfo)
        {
            return new JsonResult(new SmallBusinessReport{ SmallBusinessInfo = smallBusinessInfo, ReportValue=10});
        }
    }
}