using HomeStreetInvest.Model;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homsin.Service
{
    public class MarketAuxService
    {
        public string FailMessage { get; set; } = "";

        public async Task<Root> GetFinanceNews()
        {
            string apiUrl = $"https://api.marketaux.com/v1/news/all?symbols=TSLA,AMZN,MSFT&filter_entities=true&language=en&api_token={APITokens.MarketAuxKey}"  ;

            HttpClient client = new HttpClient();
            Root Root = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string RawJson = await response.Content.ReadAsStringAsync();
                    FailMessage = RawJson;
                    try
                    {
                        Root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(RawJson);
                    }
                    catch (Exception dsE)
                    {

                        FailMessage = dsE.Message;
                    }
                }
                else
                {
                    FailMessage = "Something Went Wrong 2";
                }
            }
            catch (Exception)
            {
                FailMessage = "Something Went Wrong";
            }

            return new Root();
        }

    }
}
