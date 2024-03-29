﻿using Blazored.LocalStorage;
using Handcom.Web.Model.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Handcom.Web.Services.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;


        public ApiAuthenticationStateProvider(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("AccessToken");
            var expirationToken = await _localStorage.GetItemAsync<string>("tokenExpiration");

            if (string.IsNullOrWhiteSpace(savedToken) || TokenExpirou(expirationToken))
            {
                await RefreshToken();
            }

            savedToken = await _localStorage.GetItemAsync<string>("AccessToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                MarkUserAsLoggedOut();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(
               new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        }

        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
           new Claim(ClaimTypes.Name, email)
        }, "apiauth"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async void MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("AccessToken");
            await _localStorage.RemoveItemAsync("RefreshToken");
            await _localStorage.RemoveItemAsync("tokenExpiration");
            await _localStorage.RemoveItemAsync("User");
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private bool TokenExpirou(string dataToken)
        {
            DateTime dataAtualUtc = DateTime.Now;
            DateTime dataExpiracao;

            string[] formatos = { "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'", "yyyy-MM-dd'T'HH:mm:ss'Z'" };

            foreach (var formato in formatos)
            {
                if (DateTime.TryParseExact(dataToken, formato, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dataExpiracao))
                {
                    if (dataExpiracao < dataAtualUtc)
                    {
                        return true;
                    }

                    break;
                }
            }

            return false;
        }


        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();

            if (string.IsNullOrEmpty(jwt))
            {
                return claims;
            }

            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            try
            {
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                if (keyValuePairs.TryGetValue("User", out var roles))
                {
                    if (roles is JsonElement rolesJson && rolesJson.ValueKind == JsonValueKind.Array)
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(rolesJson.GetRawText());
                        claims.AddRange(parsedRoles.Select(parsedRole => new Claim("User", parsedRole)));
                    }
                    keyValuePairs.Remove("User");
                }
                else
                {
                    claims.Add(new Claim("User", "DefaultRole"));
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }

            return claims;
        }

        public async Task RefreshToken()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                TokenDto refreshToken = new TokenDto();
                refreshToken.RefreshToken = await _localStorage.GetItemAsync<string>("RefreshToken");
                refreshToken.AccessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                if (string.IsNullOrWhiteSpace(refreshToken.AccessToken))
                {
                    MarkUserAsLoggedOut();
                    httpClient.DefaultRequestHeaders.Authorization = null;
                    return;
                }

                var refreshTokenCreateRequestDtoAsJson = JsonSerializer.Serialize(refreshToken);
                var requestContent = new StringContent(refreshTokenCreateRequestDtoAsJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/Auth/Refresh-Token", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var newToken = JsonSerializer.Deserialize<TokenDto>
                                  (await response.Content.ReadAsStringAsync(),
                                  new JsonSerializerOptions
                                  {
                                      PropertyNameCaseInsensitive = true
                                  });

                    await _localStorage.SetItemAsync("AccessToken", newToken.AccessToken);
                    await _localStorage.SetItemAsync("RefreshToken", newToken.RefreshToken);
                    await _localStorage.SetItemAsync("tokenExpiration", DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'"));

                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                }
                else
                {
                    MarkUserAsLoggedOut();
                    httpClient.DefaultRequestHeaders.Authorization = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

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
}