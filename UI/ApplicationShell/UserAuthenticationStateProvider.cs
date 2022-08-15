using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

// TODO:  Review this class Needs to come back on it
/// <summary>
/// Defines the <see cref="UserAuthenticationStateProvider" />.
/// </summary>
public class UserAuthenticationStateProvider : AuthenticationStateProvider
{
	/// <summary>
	/// Defines the _httpClient.
	/// </summary>
	private readonly HttpClient _httpClient;

	/// <summary>
	/// Defines the _localStorage.
	/// </summary>
	private readonly ILocalStorageService _localStorage;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserAuthenticationStateProvider"/> class.
	/// </summary>
	/// <param name="httpClient">The httpClient<see cref="HttpClient"/>.</param>
	/// <param name="localStorage">The localStorage<see cref="ILocalStorageService"/>.</param>
	public UserAuthenticationStateProvider()
	{
		//_httpclient = httpclient;
		//_localstorage = localstorage;
	}

	/// <summary>
	/// The GetAuthenticationStateAsync.
	/// </summary>
	/// <returns>The <see cref="Task{AuthenticationState}"/>.</returns>
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var storageValue = await _localStorage.GetItemAsync<string>("oidc.user:https:///localhost:5003/:balosar-blazor-client");
		var savedTokenObject = JsonSerializer.Deserialize<dynamic>(storageValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

		// var savedTokenObject = (JsonConvert.DeserializeObject<dynamic>(storageValue));
		var savedToken = savedTokenObject.id_Token;
		if (string.IsNullOrWhiteSpace(savedToken))
		{
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

		return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
		return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(""), "jwt")));
	}

	/// <summary>
	/// The MarkUserAsAuthenticated.
	/// </summary>
	/// <param name="token">The token<see cref="string"/>.</param>
	public void MarkUserAsAuthenticated(string token)
	{
		var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
		var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
		NotifyAuthenticationStateChanged(authState);
	}

	/// <summary>
	/// The MarkUserAsLoggedOut.
	/// </summary>
	public void MarkUserAsLoggedOut()
	{
		var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
		var authState = Task.FromResult(new AuthenticationState(anonymousUser));
		NotifyAuthenticationStateChanged(authState);
	}

	/// <summary>
	/// The ParseClaimsFromJwt.
	/// </summary>
	/// <param name="jwt">The jwt<see cref="string"/>.</param>
	/// <returns>The <see cref="IEnumerable{Claim}"/>.</returns>
	private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
	{
		var claims = new List<Claim>();
		var payload = jwt.Split('.')[1];
		var jsonBytes = ParseBase64WithoutPadding(payload);
		var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

		keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

		if (roles != null)
		{
			if (roles.ToString().Trim().StartsWith("["))
			{
				var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

				foreach (var parsedRole in parsedRoles)
				{
					claims.Add(new Claim(ClaimTypes.Role, parsedRole));
				}
			}
			else
			{
				claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
			}

			keyValuePairs.Remove(ClaimTypes.Role);
		}

		claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

		return claims;
	}

	/// <summary>
	/// The ParseBase64WithoutPadding.
	/// </summary>
	/// <param name="base64">The base64<see cref="string"/>.</param>
	/// <returns>The <see cref="byte[]"/>.</returns>
	private byte[] ParseBase64WithoutPadding(string base64)
	{
		switch (base64.Length % 4)
		{
			case 2: base64 += "=="; break;
			case 3: base64 += "="; break;
		}
		return Convert.FromBase64String(base64);
	}
}
