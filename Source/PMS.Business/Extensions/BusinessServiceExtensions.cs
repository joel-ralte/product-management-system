using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PMS.Business.Services;
using PMS.Business.Services.Interfaces;
using PMS.Business.UnitOfWork.Interfaces;
using PMS.Repository.Helpers;
using PMS.Repository.Helpers.Interfaces;
using PMS.Repository.Repositories;
using PMS.Repository.Repositories.Interfaces;

namespace PMS.Business.Extensions
{
    /// <summary>
    /// Provides extension methods for registering business services with the dependency injection container.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class BusinessServiceExtensions
    {
        /// <summary>
        /// Registers business services, repositories, helpers, and unit of work with the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddScoped<IIdGeneratorHelper, IdGeneratorHelper>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductOperationsRepository, ProductOperationsRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}