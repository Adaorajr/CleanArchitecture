using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Handlers.Notifications;
using CleanArchitecture.Domain.Interfaces.Queries;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using CleanArchitecture.Infra.Queries;
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
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Domain.Pipelines.ValidateCommand<,>));
            services.AddMediatR(AppDomain.CurrentDomain.Load("CleanArchitecture.Domain"));

            services.AddScoped<IDomainNotificationMediatorService, DomainNotificationMediatorService>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            #endregion
        }

        private static void ContextDependencyInjection(IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            //Queries
            services.AddScoped<ICustomerQueries, CustomerQueries>();
            services.AddScoped<IProductQueries, ProductQueries>();

            // Unit Of Work
            services.AddScoped<IUnitOfWorkContext, UnitOfWorkContext>();
            #endregion
        }

        private static void ContextDoisDependencyInjection(IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Unit Of Work
            services.AddScoped<IUnitOfWorkContextDois, UnitOfWorkContextDois>();
            #endregion
        }
    }
}