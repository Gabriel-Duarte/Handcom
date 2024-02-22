using Blazored.LocalStorage;
using Handcom.Web.Model.Extensions;
using Handcom.Web.Model.Request;
using Handcom.Web.Model.Responses;
using Handcom.Web.Pagination.Base;
using Handcom.Web.Services.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Handcom.Web.Services.Services
{
    public class TopicsService : ITopicsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public TopicsService(IHttpClientFactory httpClientFactory,

            ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<Response<Page<TopicsResponse>>> GetListTopics(TopicsRequest topicsRequest)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                var apiUrl = $"api/Topics?Page={topicsRequest.Page}&Size={topicsRequest.Size}&Search={topicsRequest.Search}&Sort={topicsRequest.Sort}&Direction={topicsRequest.Direction}";

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var topicResult = JsonSerializer.Deserialize<Response<Page<TopicsResponse>>>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return topicResult ?? new Response<Page<TopicsResponse>> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<Page<TopicsResponse>>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<Page<TopicsResponse>>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
        public async Task<Response<TopicsResponse>> TopicCreate(TopicCreateRequest topicCreateRequest)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");

                var topicCreateRequestDtoAsJson = JsonSerializer.Serialize(topicCreateRequest);
                var requestContent = new StringContent(topicCreateRequestDtoAsJson, Encoding.UTF8, "application/json");

                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.PostAsync("api/Topics", requestContent);
                if (response.IsSuccessStatusCode)
                {

                    var updateUserResult = JsonSerializer.Deserialize<Response<TopicsResponse>>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                    return updateUserResult ?? new Response<TopicsResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<TopicsResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<TopicsResponse>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
        public async Task<Response<TopicsResponse>> UpdateTopic(TopicsUpdateRequest topicsUpdateRequest)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");

                var topicRequestDtoAsJson = JsonSerializer.Serialize(topicsUpdateRequest);
                var requestContent = new StringContent(topicRequestDtoAsJson, Encoding.UTF8, "application/json");

                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.PutAsync("api/Topics", requestContent);
                if (response.IsSuccessStatusCode)
                {

                    var updateUserResult = JsonSerializer.Deserialize<Response<TopicsResponse>>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                    return updateUserResult ?? new Response<TopicsResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<TopicsResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<TopicsResponse>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
    }
}
