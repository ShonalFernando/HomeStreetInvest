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
    public class FeedService
    {
        public string _ErrorMessage { get; set; } = "";
        public bool isFailed { get; set; } = false;

        public async Task<List<Feed>> GetFeeds()
        {
            string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Feed/GetFeeds";

            HttpClient client = new HttpClient();
            List<Feed> Feeds = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string RawJson = await response.Content.ReadAsStringAsync();
                    Feeds = JsonSerializer.Deserialize<List<Feed>>(RawJson);
                    isFailed = false;
                }
            }
            catch (Exception)
            {
                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }

            return Feeds;
        }

        public async Task CreateFeed(Feed Feed)
        {
            try
            {
                string RawJson = JsonSerializer.Serialize(Feed);

                StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");

                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Feed/CreateFeed";

                HttpClient client = new HttpClient();
                var response = await client.PostAsync(apiUrl, content);

            }
            catch (Exception)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }
        }

        public async Task UpdateFeed(Feed Feed)
        {
            try
            {
                string RawJson = JsonSerializer.Serialize(Feed);

                StringContent content = new StringContent(RawJson, System.Text.Encoding.UTF8, "application/json");

                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Feed/UpdateFeed";

                HttpClient client = new HttpClient();
                var response = await client.PutAsync(apiUrl, content);

            }
            catch (Exception)
            {

                isFailed = true;
                _ErrorMessage = "Something Went Wrong";
            }
        }

        public async Task DeleteFeed(Feed Feed)
        {
            try
            {
                string apiUrl = $"https://localhost:{PortSettings.RWAPI}/api/Feed/DeleteFeed/{Feed._id}";

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
