using Blazored.LocalStorage;
using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Services.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Handcom.Web.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public UserProfileService(IHttpClientFactory httpClientFactory,

            ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
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

                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");

                var userCreateRequestDtoAsJson = JsonSerializer.Serialize(updateUserProfileRequest);
                var requestContent = new StringContent(userCreateRequestDtoAsJson, Encoding.UTF8, "application/json");

                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.PutAsync("api/UserProfile", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var updateUserResult = JsonSerializer.Deserialize<Response<UserProfileResponse>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                    return updateUserResult ?? new Response<UserProfileResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
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
    }
}
