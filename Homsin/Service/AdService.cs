using HomeStreetInvest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homsin.Service
{
    public class AdService
    {
        public string _ErrorMessage { get; set; } = "";
        public bool isFailed { get; set; } = false;

        public async Task<List<Advertisment>> GetAds()
        {
            string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Ads/GetAds";

            HttpClient client = new HttpClient();
            List<Advertisment> Advertisments = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string RawJson = await response.Content.ReadAsStringAsync();
                    Advertisments = JsonSerializer.Deserialize<List<Advertisment>>(RawJson);
                    isFailed = false;
                }
            }
            catch (Exception)
            {
                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }

            return Advertisments;
        }

        public async Task CreateAd(Advertisment Advertisment)
        {
            try
            {
                string RawJson = JsonSerializer.Serialize(Advertisment);

                StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");

                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Ads/CreateAd";
                //api/Ads/CreateAd
                HttpClient client = new HttpClient();
                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    _ErrorMessage ="";
                }
                else
                {
                    _ErrorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";

                    string responseContent = await response.Content.ReadAsStringAsync();
                    _ErrorMessage += " :" + responseContent + RawJson;
                }
            }
            catch (Exception Exe)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong " + Exe.Message;
            }
        }

        public async Task UpdateAd(Advertisment Advertisment)
        {
            try
            {
                string RawJson = JsonSerializer.Serialize(Advertisment);

                StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");

                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Ads/UpdateAd";

                HttpClient client = new HttpClient();
                var response = await client.PutAsJsonAsync(apiUrl, content);

            }
            catch (Exception)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }
        }

        public async Task DeleteAd(Advertisment Advertisment)
        {
            try
            {
                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Ads/DeleteAd/{Advertisment._id}";

                HttpClient client = new HttpClient();
                var response = await client.DeleteAsync(apiUrl);

            }
            catch (Exception)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }
        }
    }
}
