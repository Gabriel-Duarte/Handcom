using AutoMapper;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.DataAccess.Pagination.Page;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Dto.Responses;
using Handcom.Domain.Models;
using Handcom.Services.Interfaces;
using Handcom.Services.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Services
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        private readonly ILogger<UserProfileService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileService(
           INotifierService notifierService,
           ILogger<UserProfileService> logger,
           IHttpContextAccessor httpContextAccessor,
             UserManager<ApplicationUser> userManager) : base(notifierService)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserProfileResponseDto> GetUserProfile(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value);
                if (user != null)
                {
                    UserProfileResponseDto userResponseDto = new UserProfileResponseDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        ImagePath = user.ImagePath
                    };
                    return userResponseDto;
                }
                return Notify("Usuario não encontrado.", new UserProfileResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new UserProfileResponseDto());
            }
        }

        public async Task<UserProfileResponseDto> UpdateUserProfile(UpdateUserProfileRequestDto updateUserRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(updateUserRequestDto.UserName))
                    return Notify("Nome não encontrado.", new UserProfileResponseDto());

                var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value);
                if (user != null)
                {
                    user.UserName = updateUserRequestDto.UserName;
                    user.ImagePath = updateUserRequestDto.ImagePath;
                   
                    var result = await _userManager.UpdateAsync(user);
                    if (result != null)
                    return new UserProfileResponseDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        ImagePath = user.ImagePath
                    };
                }

                return Notify("Usuario não poode ser atualizado.", new UserProfileResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Notify("Ocorreu um erro.", new UserProfileResponseDto());
            }
        }
    }
}
