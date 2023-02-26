using Microsoft.AspNetCore.Mvc;
using SBToolsService.POCOs;

namespace SBToolsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmallBusinessController : ControllerBase
    {
        private readonly ILogger<SmallBusinessController> _logger;

        public SmallBusinessController(ILogger<SmallBusinessController> logger)
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