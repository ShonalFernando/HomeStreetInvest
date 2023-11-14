using HomeStreetInvest.DataRW.Service;
using HomeStreetInvest.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HomeStreetInvest.DataRW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : Controller
    {
        AdDataService _AdDataService;

        public AdsController(AdDataService adDataService)
        {
            _AdDataService = adDataService;
        }

        //Get all the Ads
        [HttpGet("GetAds")]
        public async Task<IActionResult> GetAds()
        {
            try
            {
                return Ok(await _AdDataService.GetAsync());
            }
            catch (Exception AdE)
            {
                return Problem(AdE.Message);
            }
        }

        [HttpPost("CreateAd")]
        public async Task<IActionResult> CreateAd([FromBody] Advertisment _Advertisment)
        {
            try
            {
                _Advertisment._id = ObjectId.GenerateNewId();
                _Advertisment.AdID = _Advertisment._id.ToString();
                await _AdDataService.CreateAsync(_Advertisment);
                await Console.Out.WriteLineAsync( "Added");
                return Ok("Succesfully Created Advertisment");
            }
            catch (Exception AdCE)
            {
                await Console.Out.WriteLineAsync(AdCE.Message);
                return Problem(AdCE.Message);
            }
        }

        [HttpPut("UpdateAd/{AdID}")]
        public async Task<IActionResult> UpdateAd([FromBody] Advertisment _Advertisment, string AdID)
        {

            var AllAds = await _AdDataService.GetAsync();

            foreach(var ads in AllAds)
            {
                if(ads.AdID == AdID)
                {
                    _Advertisment._id = ads._id;

                    try
                    {
                        await _AdDataService.UpdateAsync(_Advertisment._id, _Advertisment);
                        return Ok("Succesfully Updated Advertisment");
                    }
                    catch (Exception AdUE)
                    {
                        return Problem(AdUE.Message);
                    }
                }
                else
                {
                    return BadRequest("Nothing to Update");
                }
            }
            return Problem();
        }

        [HttpDelete("DeleteAd/{AdID}")]
        public async Task<IActionResult> DeleteAd(ObjectId AdID)
        {
            try
            {
                await _AdDataService.RemoveAsync(AdID);
                return Ok("Succesfully Deleted Advertisment");
            }
            catch (Exception AdDE)
            {
                return Problem(AdDE.Message);
            }
        }
    }
}
