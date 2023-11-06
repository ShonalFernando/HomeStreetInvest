using HomeStreetInvest.Model;
using HomeStreetInvest.UserAccounts.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HomeStreetInvest.UserAccounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : Controller
    {
        AccountsDataService _AccountsDataService;
        SessionService _SessionService;

        public UserAccountsController(AccountsDataService accountsDataService, SessionService sessionService)
        {
            _AccountsDataService = accountsDataService;
            _SessionService = sessionService;
        }

        //Checker
        [HttpGet("CheckAPI")]
        public async Task<IActionResult> CheckAPI()
        {
            return Ok();
        }

        //SessionCreator : Fires By FrontEnd after Verification

        [HttpGet("CreateSession/{Username}")]
        public async Task<IActionResult> CreateSession(string Username)
        {
            List<UserAccount> userAccounts = await _AccountsDataService.GetAsync();
            UserAccount? UserAccount = userAccounts.Find(u => u.userName.Equals(Username));

            if (UserAccount == null)
            {
                return BadRequest("No User found with the Username");
            }
            else
            {
                try
                {
                    string SessionID = _SessionService.GenerateSessionID(UserAccount.userName, UserAccount._id);
                    UserAccount.sessionID = SessionID;
                    await _AccountsDataService.UpdateAsync(UserAccount._id, UserAccount);
                    return Ok(SessionID);
                }
                catch (Exception CSe)
                {

                    return Problem(CSe.Message);
                }
            }
        }

        //Throught this API every password is encrypted
        [HttpGet("AuthenticateUser/{Username}")]
        public async Task<IActionResult> AuthenticateUser(string Username)
        {
            List<UserAccount> userAccounts = await _AccountsDataService.GetAsync();
            UserAccount? UserAccount = userAccounts.Find(u => u.userName.Equals(Username));

            if (UserAccount == null)
            {
                return BadRequest("No User found with the Username");
            }
            else if (UserAccount.accountStatus == AccountStatus.Banned)
            {
                return BadRequest("Sorry, Your Account is Banned");
            }
            else if (UserAccount.accountStatus == AccountStatus.Invalid)
            {
                return Problem("Sorry, There is something wrong with you Account. Please contact Admin");
            }
            else if (UserAccount.accountStatus == AccountStatus.Delete)
            {
                return BadRequest("The account is deleted");
            }
            else
            {
                try
                {
                    return Ok(UserAccount);
                }
                catch (Exception AUe)
                {
                    return Problem(AUe.Message);
                }
            }
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserAccount newUserAccount)
        {
            //The password strength and other validations are handled by the front end

            List<UserAccount> userAccounts = await _AccountsDataService.GetAsync();

            if (userAccounts.Any(a => a.email.Equals(newUserAccount.email)))
            {
                return BadRequest("A user with the same email already exist");
            }
            else if (userAccounts.Any(a => a.userName.Equals(newUserAccount.userName)))
            {
                return BadRequest("A user with the same username already exist");
            }
            else
            {
                try
                {
                    newUserAccount._id = ObjectId.GenerateNewId();
                    await _AccountsDataService.CreateAsync(newUserAccount);
                    return Ok("Account Succesfully Created");
                }
                catch (Exception CUe)
                {
                    return Problem(CUe.Message);
                }
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserAccount UpdatedUserAccount)
        {
            //Should only allow user to change email, others can be done if needed not by the user but by program or admin
            List<UserAccount> userAccounts = await _AccountsDataService.GetAsync();

            UserAccount? userAccount = userAccounts.Find(a => a._id.Equals(UpdatedUserAccount));
            if (userAccount == null)
            {
                return BadRequest("User Not Found to Update");
            }
            else
            {
                try
                {
                    await _AccountsDataService.UpdateAsync(UpdatedUserAccount._id, UpdatedUserAccount);
                    return Ok("Successfully Updated User");
                }
                catch (Exception UUe)
                {

                    return Problem(UUe.Message);
                }
            }
        }

        [HttpPut("DeleteUser/{Username}")]
        public async Task<IActionResult> DeleteUser(string Username)
        {
            List<UserAccount> userAccounts = await _AccountsDataService.GetAsync();

            UserAccount? userAccount = userAccounts.Find(a => a.userName.Equals(Username));

            if (userAccount == null)
            {
                return BadRequest("User Not Found to Delete");
            }
            else
            {
                try
                {
                    userAccount.accountStatus = AccountStatus.Delete;
                    await _AccountsDataService.UpdateAsync(userAccount._id, userAccount);
                    return Ok("Successfully Deleted User");
                }
                catch (Exception UUe)
                {

                    return Problem(UUe.Message);
                }
            }

        }
    }
}
