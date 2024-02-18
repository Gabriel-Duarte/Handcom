using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Base;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Services.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Handcom.Services.Services
{
    public class PostsService : BaseService, IPostsService
    {
        private readonly ILogger<PostsService> _logger;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IPostsRepository _postsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PostsService(
           INotifierService notifierService,
           ILogger<PostsService> logger,
           IAspNetUserService aspNetUserService,
           IPostsRepository postsRepository,
           UserManager<ApplicationUser> userManager,
           IMapper mapper) : base(notifierService)
        {
            _logger = logger;
            _aspNetUserService = aspNetUserService;
            _postsRepository = postsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Page<Posts>> GetAsync(PostsPage pagination, CancellationToken cancellationToken)
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
                return Notify("Ocorreu um erro.", new Page<Posts>());
            }
        }

        public async Task<Posts> CreateAsync(PostsCreateRequestDto postsCreateRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (postsCreateRequestDto.Title is null)
                    return Notify("Titulo não encontrado.", new Posts());

                if (postsCreateRequestDto.Content is null)
                    return Notify("Conteudo não encontrado.", new Posts());

                if (postsCreateRequestDto.TopicId == Guid.Empty)
                    return Notify("Topico nao encontrado não encontrado.", new Posts());


                var posts = _mapper.Map<Posts>(postsCreateRequestDto);
                posts.AuthorId =  _aspNetUserService.GetUserId().ToString();
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
