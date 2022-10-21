using Microsoft.AspNetCore.Components.WebView.Maui;
using AssetManager.Data;
using AssetManager.Classes;
using Microsoft.AspNetCore.Components.Authorization;

namespace AssetManager;

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
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		
		builder.Services.AddSingleton<WeatherForecastService>();

		builder.Services.AddAuthorizationCore();
		builder.Services.AddScoped<CustomAuthenticationProvider>();
		builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationProvider>());

		return builder.Build();
	}
}

