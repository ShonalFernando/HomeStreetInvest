using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homsin.Service
{
    public class FinanceService
    {
        private string FailMessage = "";

        public async Task<float[]> GetStockPredict(string stock)
        {
            string apiUrl = $"https://localhost:7024/api/Predict/GetAllStockPredictions/" + stock;

            HttpClient client = new HttpClient();
            float[] predicts = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string RawJson = await response.Content.ReadAsStringAsync();
                    predicts = JsonSerializer.Deserialize<float[] >(RawJson);
                }
            }
            catch (Exception ex)
            {
                FailMessage = "Something Went Wrong ";
                FailMessage += ex.Message;
            }

            return predicts ?? Array.Empty<float>(); ;
        }
    }
}