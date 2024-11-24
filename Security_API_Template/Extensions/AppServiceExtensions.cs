using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API_Template.Data.DataBase.Context;
using API_Template.Data.Interfaces;
using API_Template.Data.Repositories;
using API_Template.Api.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API_Template.Api.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlite(b => b.MigrationsAssembly("API_Template.Api"));
            });

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUser, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}
