using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetPracticeCore.Data;
using Microsoft.EntityFrameworkCore;

namespace NetPracticeCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNetPracticeCore(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IConfederacionRepository, ConfederacionRepository>();
            services.AddScoped<IDeporteRepository, DeporteRepository>();

            return services;
        }
    }
}
