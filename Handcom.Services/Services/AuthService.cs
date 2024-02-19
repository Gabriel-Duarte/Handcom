using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Handcom.Domain.Dto.Responses;
using Handcom.Services.Services.Base;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Handcom.Domain.Dto.Extensions;


namespace Handcom.Services.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            INotifierService notifierService,
            ILogger<AuthService> logger,
            SignInManager<ApplicationUser> signInManager) : base(notifierService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _signInManager = signInManager;
        }


        public async Task<RegisterUserResponseDto> CreateUserAsync(RegisterUserRequestDto registerUser, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existingUserWithEmail = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == registerUser.Email, cancellationToken).ConfigureAwait(false);
                if (existingUserWithEmail != null)
                    return Notify("Email inválido.", new RegisterUserResponseDto());

                ApplicationUser user = new()
                {
                    Email = registerUser.Email.Trim(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerUser.Username.Split()[0]
                };

                var userCreated = await _userManager.CreateAsync(user, registerUser.Password).ConfigureAwait(false);

                if (userCreated.Errors.Any())
                {
                    foreach (var error in userCreated.Errors)
                    {
                        Notify(error.Description);
                    }

                    return new RegisterUserResponseDto();
                }

                return new RegisterUserResponseDto(user.UserName, user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthService, CreateUserAsync: Request object => {SerializeObjectToJson(registerUser)}");
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new RegisterUserResponseDto());
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var appUser = await _userManager.FindByEmailAsync(loginRequestDto.Email).ConfigureAwait(false);
                if (appUser is null)
                    return Notify("Usuário ou senha incorretos.", new LoginResponseDto());

                         
                var result = await _signInManager.PasswordSignInAsync(appUser, loginRequestDto.Password, false, true).ConfigureAwait(false);
                if (result.Succeeded)
                    return await GetToken(appUser, cancellationToken).ConfigureAwait(false);

                if (result.IsLockedOut)
                    return Notify("Usuário bloqueado temporariamente por tentativas inválidas.", new LoginResponseDto());

                return Notify("Usuário ou senha incorretos.", new LoginResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthService, LoginAsync: Request object => {SerializeObjectToJson(loginRequestDto)}");
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new LoginResponseDto());
            }
        }

        public async Task<TokenDto?> GetRefreshTokenAsync(TokenDto token, CancellationToken cancellationToken)
        {
            try
            {
                string? accessToken = token.AccessToken
                                ?? throw new ArgumentNullException(nameof(token));

                string? refreshToken = token.RefreshToken
                                       ?? throw new ArgumentException(nameof(token));

                var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

                if (principal == null)
                {
                    return Notify("Invalid access token/refresh token", new TokenDto());
                }

                string username = principal.Identity.Name;

                var user = await _userManager.FindByNameAsync(username!);

                if (user == null || user.RefreshToken != refreshToken
                                 || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return Notify("Invalid access token/refresh token", new TokenDto());
                }

                var newAccessToken = _tokenService.GenerateAccessToken(
                                                   principal.Claims.ToList(), _configuration);

                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;

                await _userManager.UpdateAsync(user);
                return new TokenDto()
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthService, GetRefreshTokenAsync: Request object => {token}");
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new TokenDto());
            }
        }

        private async Task<LoginResponseDto> GetToken(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("UserID", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims,
                                                         _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                               out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime =
                            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(user);
           
            return new LoginResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresIn = token.ValidTo,
                UserToken = new UserTokenDto()
                {
                  Email = user.Email,
                  Id = user.Id
                }
            };
        }
    }

}
