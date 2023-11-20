﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MobileApp.Pages;
using MobileApp.Services;

namespace MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
            #if ANDROID
                        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            #endif
            });

#if DEBUG



#endif
            builder.Logging.AddDebug();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
