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
    public class CommentsService : ICommentsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public CommentsService(IHttpClientFactory httpClientFactory,

            ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;

        }

        public async Task<Response<Page<CommentsResponse>>> GetListComments(CommentsRequest commentsRequest)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                var apiUrl = $"api/Comments?Page={commentsRequest.Page}&Size={commentsRequest.Size}&Search={commentsRequest.Search}&Sort={commentsRequest.Sort}&Direction={commentsRequest.Direction}";

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var commentsResult = JsonSerializer.Deserialize<Response<Page<CommentsResponse>>>(
                        await response.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return commentsResult ?? new Response<Page<CommentsResponse>> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<Page<CommentsResponse>>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<Page<CommentsResponse>>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
        public async Task<Response<CommentsResponse>> CreateComments(CommentsCreateRequest commentsCreateRequest)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");

                var commentsCreateRequestDtoAsJson = JsonSerializer.Serialize(commentsCreateRequest);
                var requestContent = new StringContent(commentsCreateRequestDtoAsJson, Encoding.UTF8, "application/json");

                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.PostAsync("api/Comments", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var commentsResult = JsonSerializer.Deserialize<Response<CommentsResponse>>(
                   await response.Content.ReadAsStringAsync(),
                   new JsonSerializerOptions
                   {
                       PropertyNameCaseInsensitive = true
                   });

                    return commentsResult ?? new Response<CommentsResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<CommentsResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<CommentsResponse>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
    }
}

