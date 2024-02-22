using Blazored.LocalStorage;
using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Services.Authentication;
using Handcom.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Handcom.Web.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(IHttpClientFactory httpClientFactory,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<Response<LoginResponse>> Login(LoginRequest loginModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                var loginAsJson = JsonSerializer.Serialize(loginModel);
                var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/Auth/Login", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var loginResult = JsonSerializer.Deserialize<Response<LoginResponse>>
                                  (await response.Content.ReadAsStringAsync(),
                                  new JsonSerializerOptions
                                  {
                                      PropertyNameCaseInsensitive = true
                                  });

                    await _localStorage.SetItemAsync("AccessToken", loginResult.Data.AccessToken);
                    await _localStorage.SetItemAsync("RefreshToken", loginResult.Data.RefreshToken);
                    await _localStorage.SetItemAsync("tokenExpiration", loginResult.Data.ExpiresIn);
                    await _localStorage.SetItemAsync("User", loginResult.Data.UserToken);

                    ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                                        .MarkUserAsAuthenticated(loginModel.Email);

                    httpClient.DefaultRequestHeaders.Authorization =
                                new AuthenticationHeaderValue("bearer",
                                                                 loginResult.Data.AccessToken);

                    return loginResult;
                }
                else
                {
                    return new Response<LoginResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public void Logout()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<Response<RegisterUserResponse>> Register(RegisterUserRequest registerUser)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                var RegisterAsJson = JsonSerializer.Serialize(registerUser);
                var requestContent = new StringContent(RegisterAsJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/Auth/Register", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var loginResult = JsonSerializer.Deserialize<Response<RegisterUserResponse>>
                                  (await response.Content.ReadAsStringAsync(),
                                  new JsonSerializerOptions
                                  {
                                      PropertyNameCaseInsensitive = true
                                  });
                    return loginResult ?? new Response<RegisterUserResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<RegisterUserResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}