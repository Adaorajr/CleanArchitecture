using System;
using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infra.Repositories;
using MediatR;
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

            #endregion

            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Middleware
            services.AddTransient<ExceptionHandlingMiddleware>();
            #endregion

            #region Mediator 
            services.AddMediatR(AppDomain.CurrentDomain.Load("CleanArchitecture.Domain"));
            #endregion
        }
    }
}