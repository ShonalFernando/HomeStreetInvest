using HomeStreetInvest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homsin.Service
{
    //House Intel API Consumer Service

    public class HouseIntelService
    {
        public HousePrice housePrice { get; set; } = new HousePrice ();
        public int currentWizardPage { get; set; }

        //Also Get Preferences from dictionary to map Currency , etc Later

        public async Task<float> GetPrediction()
        {
            string jsonData = JsonSerializer.Serialize(housePrice);

            string apiUrl = "http://localhost:5001/predict";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    float _estimatedPrice = JsonSerializer.Deserialize<float>(responseContent);
                    return _estimatedPrice;
                }
                catch (HttpRequestException ex)
                {
                    return -1;
                }
            }
        }

        public void DestroySessionData()
        {
            housePrice.bsmtFinSF2 = 0;
            housePrice.id = 0;
            housePrice.yearBuilt= 0;
            housePrice.totalBsmtSF = 0;
            housePrice.lotArea = 0;
            housePrice.mSSubClass= 0;
            housePrice.yearRemodAdd = 0;
            housePrice.overallCond= 0;
            currentWizardPage =0;
        }

        public enum WizardPage
        {
            Zoning,
            Location,
            Category, 
            Exterior,  
            Numericals,            
            Results
        }
    }
}
