using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces.Services;
using CleanArchitecture.Domain.Services;
using CleanArchitecture.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Api.Configurations
{
    public static class Config
    {
        public static IServiceCollection DependenciesResolvers(this IServiceCollection services)
        {
            DependencyInjection(services);

            return services;
        }

        private static void DependencyInjection(IServiceCollection services)
        {
            #region Services           
            services.AddScoped<IProductService, ProductService>();
            #endregion

            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion

            #region Middleware
            services.AddTransient<ExceptionHandlingMiddleware>();
            #endregion
        }
    }
}