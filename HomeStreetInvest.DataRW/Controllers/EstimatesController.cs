using HomeStreetInvest.DataRW.Service;
using HomeStreetInvest.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HomeStreetInvest.DataRW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimatesController : Controller
    {
        EstimatesDataService _EstimatesDataService;

        public EstimatesController(EstimatesDataService estimatesDataService)
        {
            _EstimatesDataService = estimatesDataService;
        }

        //Get all the Estimates
        [HttpGet("GetEstimates")]
        public async Task<IActionResult> GetEstimates()
        {
            try
            {
                return Ok(await _EstimatesDataService.GetAsync());
            }
            catch (Exception EstimateE)
            {
                return Problem(EstimateE.Message);
            }
        }

        [HttpPost("CreateEstimate")]
        public async Task<IActionResult> CreateEstimate([FromBody] Estimate _Estimate)
        {
            try
            {
                _Estimate._id = ObjectId.GenerateNewId();
                await _EstimatesDataService.CreateAsync(_Estimate);
                return Ok("Succesfully Created Estimate");
            }
            catch (Exception EstimateCE)
            {
                return Problem(EstimateCE.Message);
            }
        }

        [HttpPut("UpdateEstimate")]
        public async Task<IActionResult> UpdateEstimate([FromBody] Estimate _Estimate)
        {
            try
            {
                await _EstimatesDataService.UpdateAsync(_Estimate._id, _Estimate);
                return Ok("Succesfully Updated Estimate");
            }
            catch (Exception EstimateUE)
            {
                return Problem(EstimateUE.Message);
            }
        }

        [HttpDelete("DeleteEstimate/{EstimateID}")]
        public async Task<IActionResult> DeleteEstimate(ObjectId EstimateID)
        {
            try
            {
                await _EstimatesDataService.RemoveAsync(EstimateID);
                return Ok("Succesfully Deleted Estimate");
            }
            catch (Exception EstimateDE)
            {
                return Problem(EstimateDE.Message);
            }
        }
    }
}
