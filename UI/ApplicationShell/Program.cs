namespace ApplicationShell
{
	using ApplicationShell.Security;
	using ApplicationShell.Services;
	using Blazored.LocalStorage;
	using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
	using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="Program" />.
	/// </summary>
	public class Program
	{
		//public static async Task Main(string[] args)
		//{
		//    var builder = WebAssemblyHostBuilder.CreateDefault(args);
		//    var services = builder.Services;
		//    builder.RootComponents.Add<App>("#app");
		//    services.AddBlazoredLocalStorage();
		//    services.AddAuthorizationCore();
		//    services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();
		//    services.AddScoped<IAuthService, AuthService>();




		//    builder.Services.AddHttpClient("Balosar.ServerAPI")
		//        .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
		//        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

		//    // Supply HttpClient instances that include access tokens when making requests to the server project.
		//    builder.Services.AddScoped(provider =>
		//    {
		//        var factory = provider.GetRequiredService<IHttpClientFactory>();
		//        return factory.CreateClient("Balosar.ServerAPI");
		//    });

		//    builder.Services.AddOidcAuthentication(options =>
		//    {
		//        options.ProviderOptions.ClientId = "balosar-blazor-client";
		//        options.ProviderOptions.Authority = "https://localhost:44310/";
		//        options.ProviderOptions.ResponseType = "code";

		//        // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
		//        // authentication stack is impacted by a bug that prevents it from correctly extracting
		//        // authorization error responses (e.g error=access_denied responses) from the URL fragment.
		//        // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
		//        //
		//        options.ProviderOptions.ResponseMode = "query";
		//        options.AuthenticationPaths.RemoteRegisterPath = "https://localhost:44310/Identity/Account/Register";
		//    });

		//    var host = builder.Build();
		//    return host.RunAsync();

		//    //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

		//    //await builder.Build().RunAsync();
		//}
		/// <summary>
		/// The Main.
		/// </summary>
		/// <param name="args">The args<see cref="string[]"/>.</param>
		/// <returns>The <see cref="Task"/>.</returns>
		public static Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			//    builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddAuthorizationCore();

			builder.Services.AddHttpClient("Balosar.ServerAPI")
				.ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:3000/"))
				  .AddHttpMessageHandler(sp =>
				  {
					  var handler = sp.GetService<AuthorizationMessageHandler>()
					  .ConfigureHandler(
						  authorizedUrls: new[] { "http://localhost:3000/" },
						 scopes: new[] { "api1" }
					   );
					  return handler;
				  });

			builder.Services.AddBlazoredLocalStorage();

			
			builder.Services.AddScoped<IAuthService, AuthService>();

			// Supply HttpClient instances that include access tokens when making requests to the server project.
			builder.Services.AddScoped(provider =>
			{
				var factory = provider.GetRequiredService<IHttpClientFactory>();
				return factory.CreateClient("Balosar.ServerAPI");
			});

			builder.Services.AddOidcAuthentication(options =>
			{
				options.ProviderOptions.ClientId = "balosar-blazor-client";
				options.ProviderOptions.Authority = "https://localhost:4003/";
				options.ProviderOptions.ResponseType = "code";
				options.UserOptions.ScopeClaim = "openid, profile, role";


				// Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
				// authentication stack is impacted by a bug that prevents it from correctly extracting
				// authorization error responses (e.g error=access_denied responses) from the URL fragment.
				// For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
				//
				options.ProviderOptions.ResponseMode = "query";
				options.AuthenticationPaths.RemoteRegisterPath = "https://localhost:4003/Identity/Account/Register";
			}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

			var host = builder.Build();
			return host.RunAsync();
		}
	}
}
