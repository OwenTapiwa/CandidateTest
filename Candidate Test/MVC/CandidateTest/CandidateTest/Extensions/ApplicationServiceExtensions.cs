using CandidateTest.Data;
using CandidateTest.Interfaces;
using CandidateTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateTest.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContextFactory<CandidateTestContext>(options => options.UseSqlServer(
                connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();

                }), ServiceLifetime.Transient);
            return services;
        }
    }
}
