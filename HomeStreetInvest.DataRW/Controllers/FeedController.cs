using HomeStreetInvest.DataRW.Service;
using HomeStreetInvest.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HomeStreetInvest.DataRW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : Controller
    {
        FeedDataService _FeedDataService;

        public FeedController(FeedDataService feedDataService)
        {
            _FeedDataService = feedDataService;
        }

        //Get all the Feeds
        [HttpGet("GetFeeds")]
        public async Task<IActionResult> GetFeeds()
        {
            try
            {
                return Ok(await _FeedDataService.GetAsync());
            }
            catch (Exception FeedE)
            {
                return Problem(FeedE.Message);
            }
        }

        [HttpPost("CreateFeed")]
        public async Task<IActionResult> CreateFeed([FromBody] Feed _FeedPost)
        {
            try
            {
                _FeedPost._id = ObjectId.GenerateNewId();
                _FeedPost.FeID = _FeedPost._id.ToString();
                await _FeedDataService.CreateAsync(_FeedPost);
                return Ok("Succesfully Created FeedPost");
            }
            catch (Exception FeedCE)
            {
                return Problem(FeedCE.Message);
            }
        }

        [HttpPut("UpdateFeed")]
        public async Task<IActionResult> UpdateFeed([FromBody] Feed _FeedPost)
        {
            try
            {
                await _FeedDataService.UpdateAsync(_FeedPost._id, _FeedPost);
                return Ok("Succesfully Updated FeedPost");
            }
            catch (Exception FeedUE)
            {
                return Problem(FeedUE.Message);
            }
        }

        [HttpDelete("DeleteFeed/{FeedID}")]
        public async Task<IActionResult> DeleteFeed(ObjectId FeedID)
        {
            try
            {
                await _FeedDataService.RemoveAsync(FeedID);
                return Ok("Succesfully Deleted Feed");
            }
            catch (Exception FeedDE)
            {
                return Problem(FeedDE.Message);
            }
        }
    }
}
