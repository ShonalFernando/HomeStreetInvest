using HomeStreetInvest.HousingIntel.Services;
using HomeStreetInvest.Model;
using HousingIntel.Helper;
using HousingIntel.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;

namespace HousingIntel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingPricingController : ControllerBase
    {
        HouseIntelService _HouseIntelService;
        LogService _LogService;
        LogDataStream _LogDataStream;

        public HousingPricingController(LogDataStream logDataStream, HouseIntelService houseIntelService, LogService logService)
        {
            _HouseIntelService = houseIntelService;
            _LogService = logService;
            _LogDataStream = logDataStream;
        }

        //Predict
        [HttpGet("CheckAPI")]
        public async Task<IActionResult> CheckAPI()
        {
            return Ok();
        }

            //Predict
            [HttpPost("Predict")]
        public async Task<IActionResult> Predict(HousePrice HousePrice)
        {
            LogModel model = new LogModel();
            model._id = ObjectId.GenerateNewId();

            try
            {
                var ResultScore = _HouseIntelService.Predict(HousePrice);
                if(ResultScore == -1)
                {
                    _LogService.Log("Something Wrong with the Data Input", LogWarning.Error);
                    return Problem("Something Wrong with the Data Input");
                }
                else if (ResultScore == 0)
                {
                    _LogService.Log("Abnormal Value, Model or Input Error", LogWarning.Info);
                    return Ok(0);
                }
                else
                {
                    _LogService.Log($"Price Prediction Complete | {ResultScore} ", LogWarning.Info);
                    return Ok(ResultScore);
                }
            }
            catch (Exception ControllerExc)
            {
                _LogService.Log(ControllerExc.Message, LogWarning.Error);
                return Problem(ControllerExc.Message);
            }
            
        }
    }
}
