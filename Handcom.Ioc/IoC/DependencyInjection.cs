using Handcom.Data.Data.Repositories;
using Handcom.Data.Data.Uow;
using Handcom.Data.Data.Uow.Interface;
using Handcom.Domain.DataAccess.Interfaces;
using Handcom.Domain.Models;
using Handcom.Ioc.Configuration;
using Handcom.Services.Interfaces;
using Handcom.Services.Services;
using Handcom.Services.Services.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Ioc.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
       IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
                         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
                        ), b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddJwt(configuration);
            services.AddAuthorization();
           
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton(MappingConfig.RegisterMaps().CreateMapper());

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INotifierService, NotifierService>();
            services.AddScoped<ITopicsService, TopicsService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<ICommentsService, CommentsService>();

            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IAspNetUserService, AspNetUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
