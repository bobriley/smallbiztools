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
            var healthRatio = (float)smallBusinessInfo.Rent / (float)smallBusinessInfo.Sales;

            var addbacks = smallBusinessInfo.Interest + smallBusinessInfo.Depreciation + smallBusinessInfo.OwnerPersonalExpenses;
            var expenses = smallBusinessInfo.Rent + smallBusinessInfo.Utilities + smallBusinessInfo.MiscExpenses;
            var ebitda = smallBusinessInfo.Sales - expenses;
            var sde = ebitda + expenses - addbacks;

            var sdeValuation = sde * smallBusinessInfo.SDEMultiple;

            var priceDelta = smallBusinessInfo.AskingPrice - sdeValuation;

            return new JsonResult(new SmallBusinessReport{ SmallBusinessInfo = smallBusinessInfo, HealthRatio = healthRatio, 
                                                           PriceDelta = priceDelta, SDE=sde, SDEValuation = sdeValuation });
        }
    }
}