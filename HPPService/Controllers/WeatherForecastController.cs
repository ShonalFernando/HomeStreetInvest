using HPPService.Service;
using Microsoft.AspNetCore.Mvc;

namespace HPPService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ValuePredictor _ValuePredictor;

        public WeatherForecastController(ValuePredictor valuePredictor)
        {
            _ValuePredictor = valuePredictor;
        }

        [HttpGet("{Username}")]
        public async Task<IActionResult> Get(string Username)
        {
            return Ok(_ValuePredictor.Predict());
        }
    }
}