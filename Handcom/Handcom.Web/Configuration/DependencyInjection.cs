using Blazored.LocalStorage;
using Handcom.Web.Services.Authentication;
using Handcom.Web.Services.Interface;
using Handcom.Web.Services.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace Handcom.Web.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
       IConfiguration configuration)
        {
            services.AddRadzenComponents();
            services.AddBlazoredLocalStorage();

            services.AddScoped<ThemeState>();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITopicsService, TopicsService>();
            services.AddScoped<IPostsServices, PostsServices>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            return services;
        }
    }
}
