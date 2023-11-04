using HomeStreetInvest.HSISSXE.Drivers;
using HomeStreetInvest.HSISSXE.Helper;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace HomeStreetInvest.HSISSXE
{
    internal class Program
    {
        //HOME STREET INVEST SERVER SIDE EXECUTABLE
        static void Main(string[] args)
        {
            AppState.StartedTime = DateTime.Now;
            AppState.LastLogReadTime = DateTime.Now;
            HeadText.PrintHead();
            bool isAPIRunning = CheckAPI().GetAwaiter().GetResult();
            if(isAPIRunning)
            {
                LogService.Log("API Started", Model.LogWarning.DefaultLog);
            }
            else
            {
                LogService.Log("API Not Started", Model.LogWarning.Error);
            }

            Task.Run(LogProcess).Wait();
        }

        static async Task<bool> CheckAPI1()
        {
            Task.Delay(10000).Wait();
            //Check Start
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Set the base address of the API
                    httpClient.BaseAddress = new Uri("https://localhost:7114/");

                    // Send a GET request to the API
                    HttpResponseMessage response = await httpClient.GetAsync("api/HousingPricing/CheckAPI");

                    if (response.IsSuccessStatusCode)
                    {
                        // Request was successful
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response Content: " + content);
                        return true;
                    }
                    else
                    {
                        // Request was not successful
                        Console.WriteLine("Request failed with status code: " + response.StatusCode);
                        return false;
                    }
                }
                catch (Exception CheckAPIE)
                {

                    Console.WriteLine("Something Went Wrong : " + CheckAPIE.Message);
                    return false;
                }
            }
        }

        static async Task<bool> CheckAPI2()
        {
            Task.Delay(10000).Wait();
            //Check Start
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Set the base address of the API
                    httpClient.BaseAddress = new Uri("https://localhost:7114/");

                    // Send a GET request to the API
                    HttpResponseMessage response = await httpClient.GetAsync("api/HousingPricing/CheckAPI");

                    if (response.IsSuccessStatusCode)
                    {
                        // Request was successful
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response Content: " + content);
                        return true;
                    }
                    else
                    {
                        // Request was not successful
                        Console.WriteLine("Request failed with status code: " + response.StatusCode);
                        return false;
                    }
                }
                catch (Exception CheckAPIE)
                {

                    Console.WriteLine("Something Went Wrong : " + CheckAPIE.Message);
                    return false;
                }
            }
        }

        static async void LogProcess()
        {
            while ("a" == "a")
            {
                var Logs = LogDataStream.ReadLog();
                foreach (var log in Logs)
                {
                    if (log.exception == null && !string.IsNullOrEmpty(log.logMessage))
                    {
                        LogService.Log(log.logMessage, log.logWarning);
                    }
                    else if (log.exception != null)
                    {
                        LogService.Log(log.exception);
                    }
                }
                Task.Delay(1000).Wait();
            }
        }
    }
}