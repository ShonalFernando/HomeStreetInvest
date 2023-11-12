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
		public bool isFailed { get; set; } = false;
		public string FailMessage { get; set; }

        public bool isLogged { get; set; }
        public string Username { get; set; }
        public string[] SessionArgs { get; set; }

        public async Task<bool> Auth(string username, string password) //Send Tuple with sessionid
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
                            isFailed = false;
							FailMessage = "";
							Username = username;
                            return true;
						}
						else
						{
                            FailMessage = "Password is Incorrect";
                            return false;
						}
					}
					else
					{
                        FailMessage = "No User found with the the Username";
                        return false;
					}
				}
				else
				{
					FailMessage = "No User found with the the Username";
                    return false;
				}
			}
            else
            {
                FailMessage = "Please enter valid credentials";
                return false;
            }
        }

		public async Task<string> CreateSession(string UserName)
		{
			string apiUrl = $"https://localhost:{PortSettings.AuthAPI}/api/UserAccounts/CreateSession/" + UserName;
			string ResponseMessage = "";

			try
			{
				HttpClient client = new HttpClient();
				UserAccount? userAccount = new();
				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					string RawJson = await response.Content.ReadAsStringAsync();
					//ResponseMessage = JsonSerializer.Deserialize<string>(RawJson);
				}

			}
			catch (Exception)
			{
                FailMessage = "Something Went Wrong in creating Session";
				isLogged = false;
                isFailed = true;
            }

			return ResponseMessage;
		}

		public async Task<UserAccount> GetUser(string UserName)
		{
			string apiUrl = $"https://localhost:{PortSettings.AuthAPI}/api/UserAccounts/AuthenticateUser/" + UserName;

			HttpClient client = new HttpClient();
			UserAccount? userAccount = new();
			try
			{
				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					string RawJson = await response.Content.ReadAsStringAsync();
					userAccount = JsonSerializer.Deserialize<UserAccount>(RawJson);
				}
			}
			catch (Exception)
			{
                isFailed = true;
				FailMessage = "Something Went Wrong";
			}

			return userAccount;
		}






		//Not Implimneted from here
		public async Task CreateUser(UserAccount UserAccount)
		{
			try
			{
				string RawJson = JsonSerializer.Serialize(UserAccount);
				StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");
				string apiUrl = $"https://localhost:{PortSettings.AuthAPI}/api/UserAccounts/CreateUser";
				HttpClient client = new HttpClient();
				var response = await client.PostAsJsonAsync(apiUrl, content);

			}
			catch (Exception)
			{

				isFailed = true;
				FailMessage = "Something Went Wrong";
			}
			//Handle responses later
		}


		
		public async Task UpdateUser(UserAccount UserAccount)
		{
			string responseContent;
			string apiUrl = $"https://localhost:{PortSettings.AuthAPI}/api/UserAccounts/";
			HttpClient client = new HttpClient();
			var response = await client.PutAsJsonAsync(apiUrl, UserAccount);
		}

	}
}
