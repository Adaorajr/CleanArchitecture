using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using CleanArchitecture.Infra.Repositories;
using CleanArchitecture.Infra.Repositories.Context;
using CleanArchitecture.Infra.Repositories.ContextDois;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            ContextDependencyInjection(services);
            ContextDoisDependencyInjection(services);
            #region Middleware
            services.AddTransient<ExceptionHandlingMiddleware>();
            #endregion
            #region Mediator 
            services.AddMediatR(AppDomain.CurrentDomain.Load("CleanArchitecture.Domain"));
            #endregion
        }

        private static void ContextDependencyInjection(IServiceCollection services)
        {
            #region Repositories
            services.AddTransient<IProductRepository, ProductRepository>();

            // Unit Of Work
            services.AddTransient<IUnitOfWorkContext, UnitOfWorkContext>();
            #endregion
        }

        private static void ContextDoisDependencyInjection(IServiceCollection services)
        {
            #region Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            // Unit Of Work
            services.AddTransient<IUnitOfWorkContextDois, UnitOfWorkContextDois>();
            #endregion
        }
    }
}