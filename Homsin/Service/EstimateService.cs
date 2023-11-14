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
    public class EstimateService
    {
        public string _ErrorMessage { get; set; } = "";
        public bool isFailed { get; set; } = false;

        public async Task<List<Estimate>> GetEstimates()
        {
            string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Estimates/GetEstimates";

            HttpClient client = new HttpClient();
            List<Estimate>? Estimates = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string RawJson = await response.Content.ReadAsStringAsync();
                    Estimates = JsonSerializer.Deserialize<List<Estimate>>(RawJson);
                    isFailed = false;
                }
            }
            catch (Exception)
            {
                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }

            return Estimates;
        }

        public async Task CreateEstimate(Estimate Estimate)
        {
            try
            {
                string RawJson = JsonSerializer.Serialize(Estimate);

                StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");

                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Estimates/CreateEstimate";

                HttpClient client = new HttpClient();
                var response = await client.PostAsync(apiUrl, content);

            }
            catch (Exception)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }
        }

        public async Task UpdateEstimate(Estimate Estimate)
        {
            try
            {
                string RawJson = JsonSerializer.Serialize(Estimate);

                StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");

                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Estimates/UpdateEstimate";

                HttpClient client = new HttpClient();
                var response = await client.PutAsJsonAsync(apiUrl, content);

            }
            catch (Exception)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }
        }

        public async Task DeleteEstimate(Estimate Estimate)
        {
            try
            {
                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Estimates/DeleteEstimate/{Estimate._id}";

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
