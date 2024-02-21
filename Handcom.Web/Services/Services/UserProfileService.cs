using Blazored.LocalStorage;
using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Handcom.Web.Services.Services
{
    public class UserProfileService: IUserProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public UserProfileService(IHttpClientFactory httpClientFactory,

            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task<Response<UserProfileResponse>> GetUserProfile()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync("api/UserProfile");

                if (response.IsSuccessStatusCode)
                {
                    var topicResult = JsonSerializer.Deserialize<Response<UserProfileResponse>>(
                        await response.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return topicResult ?? new Response<UserProfileResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<UserProfileResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<UserProfileResponse>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
        public async Task<Response<UserProfileResponse>> UpdateUserProfile(UpdateUserProfileRequest updateUserProfileRequest)
        {
            try
            {

                // Crie um cliente HttpClient
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");

                var userCreateRequestDtoAsJson = JsonSerializer.Serialize(updateUserProfileRequest);
                var requestContent = new StringContent(userCreateRequestDtoAsJson, Encoding.UTF8, "application/json");

                // Obtenha o token de acesso armazenado localmente
                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                // Add the access token to the Authorization header
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Send the request using PutAsync and the multipart content
                var response = await httpClient.PutAsync("api/UserProfile", requestContent);

                // Verifique se a resposta foi bem-sucedida antes de tentar desserializar

                var updateUserResult = JsonSerializer.Deserialize<Response<UserProfileResponse>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                return updateUserResult;

            }
            catch (Exception ex)
            {
                return new Response<UserProfileResponse>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
    }
}
