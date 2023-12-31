﻿using Homsin.Data;
using Homsin.Service;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Homsin
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<HouseIntelService>();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<FinanceService>();
            builder.Services.AddScoped<FeedService>();
            builder.Services.AddScoped<EstimateService>();
            builder.Services.AddScoped<AdService>();
            builder.Services.AddScoped<FeedDataService>();
            builder.Services.AddScoped<EstimatesDataService>();
            builder.Services.AddScoped<AdDataService>();
            builder.Services.AddScoped<MarketAuxService>();
            builder.Services.AddScoped<MessageDataService>();
            builder.Services.AddScoped<AccountsDataService>();
            builder.Services.AddScoped<EncryptionService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}