using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace Handcom.Services.Services
{
    public class PostsService : BaseService, IPostsService
    {
        private readonly ILogger<PostsService> _logger;
        private readonly IPostsRepository _postsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostsService(
           INotifierService notifierService,
           ILogger<PostsService> logger,
           IPostsRepository postsRepository,
           UserManager<ApplicationUser> userManager,
           IMapper mapper,
           IHttpContextAccessor httpContextAccessor) : base(notifierService)
        {
            _logger = logger;
            _postsRepository = postsRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Page<PostsResponseDto>> GetAsync(PostsPage pagination, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!string.IsNullOrWhiteSpace(pagination.Search))
                {
                    var user = await _userManager.FindByNameAsync(pagination.Search);
                    pagination.Search = user != null ? user.Id : pagination.Search;
                }

                return await _postsRepository.GetPostsAsync(pagination, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CompanyService, GetAsync: Request object => {SerializeObjectToJson(pagination)}");
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Page<PostsResponseDto>());
            }
        }

        public async Task<Posts> CreateAsync(PostsCreateRequestDto postsCreateRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(postsCreateRequestDto.Title))
                    return Notify("Titulo não encontrado.", new Posts());

                if (string.IsNullOrWhiteSpace(postsCreateRequestDto.Content))
                    return Notify("Conteudo não encontrado.", new Posts());

                if (postsCreateRequestDto.TopicId == Guid.Empty)
                    return Notify("Topico nao encontrado não encontrado.", new Posts());


                var posts = _mapper.Map<Posts>(postsCreateRequestDto);
                posts.AuthorId =
                    _httpContextAccessor
                    .HttpContext?
                    .User.Claims
                    .FirstOrDefault(c => c.Type == "UserID")?.Value;
                posts.CreatedAt = DateTime.Now;

                return await _postsRepository.CreateAsync(posts, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new Posts());
            }
        }
    }
}
