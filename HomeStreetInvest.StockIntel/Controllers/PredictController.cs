using HomeStreetInvest.Model;
using HomeStreetInvest.StockIntel.Service;
using HomeStreetInvest.StockIntel.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeStreetInvest.StockIntel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        StockIntelService _StockIntelService;
        LogService _LogService;
        public PredictController(StockIntelService stockIntelService, LogService logService)
        {
            _StockIntelService = stockIntelService;
            _LogService = logService;
        }

        //Predict
        [HttpGet("CheckAPI")]
        public async Task<IActionResult> CheckAPI()
        {
            return Ok();
        }

        //Predict
        [HttpGet("GetAllStockPredictions/{stock}")]
        public async Task<IActionResult> GetAllStockPredictions(Stock stock)
        {
            try
            {
                var StockPrices = _StockIntelService.Predict(45, stock);
                _LogService.Log($"Stock Prediction Complete | Stock {stock} :: All Posible Predictions ", LogWarning.Info);
                return Ok(StockPrices);
            }
            catch (Exception e1)
            {
                _LogService.Log($"Stock {e1.Message} ", LogWarning.Error);
                return Problem();
            }
        }

        //Predict
        [HttpPost("GetDayPrediction")]
        public async Task<IActionResult> GetDayPrediction([FromBody] StockPredictionRequest StockPredictionRequest)
        {
            if (StockPredictionRequest.daterequested > DateTime.Now.DayOfYear)
            {
                try
                {
                    var horizon = StockPredictionRequest.daterequested - (new DateOnly(2023,11,1)).DayOfYear;
                    var StockPred = _StockIntelService.Predict(horizon + 1, StockPredictionRequest.stock);
                    _LogService.Log($"Stock Prediction Complete | Stock {StockPredictionRequest.stock} :: {StockPred[horizon]} ", LogWarning.Info);
                    return Ok(StockPred[horizon]);
                }
                catch (Exception e2)
                {
                    _LogService.Log($"Stock {e2.Message} ", LogWarning.Error);
                    return Problem();
                }
            }
            else
            {
                _LogService.Log("The request Date is not valid", LogWarning.Info);
                return BadRequest("The request Date is not valid");
            }
        }

    }
}
