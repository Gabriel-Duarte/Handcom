using Blazored.LocalStorage;
using Handcom.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Handcom.Web.Model.Extensions;
using Handcom.Web.Pagination.Base;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Handcom.Web.Model.Responses;
using Handcom.Web.Model.Request;

namespace Handcom.Web.Services.Services
{
    public class TopicsService : ITopicsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public TopicsService(IHttpClientFactory httpClientFactory,

            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
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

    }
}
