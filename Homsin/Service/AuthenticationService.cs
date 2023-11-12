using HomeStreetInvest.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace Homsin.Service
{
    public class AuthenticationService
    {
        public bool isLogged { get; set; }
        public string Username { get; set; }
        public string[] SessionArgs { get; set; }

        public async Task<bool> Auth(string password, string username) //Send Tuple with sessionid
        {
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(password))
            {
				var _CheckAcc = await GetUser(username);
				if(username != null)
				{
					if (username == _CheckAcc.userName)
					{
						if (password == _CheckAcc.password)
						{
							await CreateSession(username);
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
            else
            {
                return false;
            }
        }

		public async Task<string> CreateSession(string UserName)
		{
			string apiUrl = $"https://localhost:5001/api/UserAccounts/CreateSession/" + UserName;

			HttpClient client = new HttpClient();
			UserAccount? userAccount = new();
			HttpResponseMessage response = await client.GetAsync(apiUrl);
			string ResponseMessage = "";
			if (response.IsSuccessStatusCode)
			{
				string RawJson = await response.Content.ReadAsStringAsync();
				//ResponseMessage = JsonSerializer.Deserialize<string>(RawJson);
			}

			return ResponseMessage;
		}

		public async Task<UserAccount> GetUser(string UserName)
		{
			string apiUrl = $"https://localhost:5001/api/UserAccounts/AuthenticateUser/" + UserName;

			HttpClient client = new HttpClient();
			UserAccount? userAccount = new();
			HttpResponseMessage response = await client.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string RawJson = await response.Content.ReadAsStringAsync();
				userAccount = JsonSerializer.Deserialize<UserAccount>(RawJson);
			}
			return userAccount;
		}






		//Not Implimneted from here
		public async Task CreateUser(UserAccount UserAccount)
		{
			string RawJson = JsonSerializer.Serialize(UserAccount);
			StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");
			string apiUrl = $"https://localhost:7265/api/UserAccounts/CreateUser";
			HttpClient client = new HttpClient();
			var response = await client.PostAsJsonAsync(apiUrl, content);

			//Handle responses later
		}


		
		public async Task UpdateUser(UserAccount UserAccount)
		{
			string responseContent;
			string apiUrl = $"https://localhost:5001/api/UserAccounts/";
			HttpClient client = new HttpClient();
			var response = await client.PutAsJsonAsync(apiUrl, UserAccount);
		}

	}
}
