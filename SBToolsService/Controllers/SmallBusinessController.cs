using Microsoft.AspNetCore.Mvc;
using SBToolsService.POCOs;
using SBToolsService.RequestObjects;
using SBToolsService.ServiceInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace SBToolsService.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class SmallBusinessController : ControllerBase
    {
        private readonly ILogger<SmallBusinessController> _logger;
        private ITokenService _tokenService;

        public SmallBusinessController(ILogger<SmallBusinessController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPut()]
        public IActionResult ProcessBusinessInfo(SmallBusinessInfoRequestData requestData)
        {
            float healthRatio = 0;
            float priceDelta = 0;
            float sde = 0;
            float sdeValuation = 0;

            if(requestData == null)
            {
                requestData = new SmallBusinessInfoRequestData();
            }

            if (_tokenService.IsTokenValid(requestData.Token))
            {
                healthRatio = (float)requestData.SmallBusinessInfo.Rent / (float)requestData.SmallBusinessInfo.Sales;

                var addbacks = requestData.SmallBusinessInfo.Interest + requestData.SmallBusinessInfo.Depreciation + requestData.SmallBusinessInfo.OwnerPersonalExpenses;
                var expenses = requestData.SmallBusinessInfo.Rent + requestData.SmallBusinessInfo.Utilities + requestData.SmallBusinessInfo.MiscExpenses;

                var ebitda = requestData.SmallBusinessInfo.Sales - expenses;
                
                sde = ebitda + expenses - addbacks;

                sdeValuation = sde * requestData.SmallBusinessInfo.SDEMultiple;

                priceDelta = requestData.SmallBusinessInfo.AskingPrice - sdeValuation;
            }
            
            return new JsonResult(new SmallBusinessReport{ SmallBusinessInfo = requestData.SmallBusinessInfo, HealthRatio = healthRatio, 
                                                           PriceDelta = priceDelta, SDE=sde, SDEValuation = sdeValuation });
        }
    }
}