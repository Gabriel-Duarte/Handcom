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
    public class PostsServices : IPostsServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public PostsServices(IHttpClientFactory httpClientFactory,

            ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }
        public async Task<Response<Page<PostsResponse>>> GetListPosts(PostsRequest postsRequest)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");
                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                var apiUrl = $"api/Posts?Page={postsRequest.Page}&Size={postsRequest.Size}&Search={postsRequest.Search}&Sort={postsRequest.Sort}&Direction={postsRequest.Direction}&Topic={postsRequest.Topic}";

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var topicResult = JsonSerializer.Deserialize<Response<Page<PostsResponse>>>(
                        await response.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return topicResult ?? new Response<Page<PostsResponse>> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<Page<PostsResponse>>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<Page<PostsResponse>>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
        public async Task<Response<PostsResponse>> CreatePosts(PostsCreateRequest postsCreateRequestDto)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("ApiHandcom");

                var postsCreateRequestDtoAsJson = JsonSerializer.Serialize(postsCreateRequestDto);
                var requestContent = new StringContent(postsCreateRequestDtoAsJson, Encoding.UTF8, "application/json");

                var accessToken = await _localStorage.GetItemAsync<string>("AccessToken");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.PostAsync("api/Posts", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var updateUserResult = JsonSerializer.Deserialize<Response<PostsResponse>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                    return updateUserResult ?? new Response<PostsResponse> { IsSuccess = false, Errors = new List<string> { "Falha ao desserializar a resposta." } };
                }
                else
                {
                    return new Response<PostsResponse>
                    {
                        IsSuccess = false,
                        Errors = new List<string> { $"A API retornou um erro: {response.StatusCode}" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<PostsResponse>
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Erro durante a execução da solicitação HTTP: {ex.Message}" }
                };
            }
        }
    }
}
