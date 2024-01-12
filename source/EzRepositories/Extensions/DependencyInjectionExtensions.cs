using EzRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEzRepository<TEntity>(
            this IServiceCollection services,
            ServiceLifetime repositoryLifetime = ServiceLifetime.Scoped)
            where TEntity : class, IEntity
        {
            services.Add(
                    new ServiceDescriptor(
                            typeof(IRepository<TEntity>),
                            typeof(Repository<TEntity>),
                            repositoryLifetime
                        )
                );

            return services;
        }

        public static IServiceCollection AddEzRepository<TEntity, TDbContext>(
             this IServiceCollection services,
             ServiceLifetime repositoryLifetime = ServiceLifetime.Scoped
            )
            where TEntity : class, IEntity
            where TDbContext : DbContext
        {
            services.Add(
                    new ServiceDescriptor(
                        typeof(IRepository<TEntity>),
                        serviceProvider =>
                        {
                            var dbcontext = serviceProvider.GetRequiredService<TDbContext>();
                            return ActivatorUtilities.CreateInstance<Repository<TEntity>>(serviceProvider, [dbcontext]);
                        }, repositoryLifetime
                    )
                );

            return services;
        }

        public static IServiceCollection AddCustomEzRepository<TEntity, TCustomRepository>(
             this IServiceCollection services,
             ServiceLifetime repositoryLifetime = ServiceLifetime.Scoped
            )
            where TEntity : class, IEntity
            where TCustomRepository : IRepository<TEntity>
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(IRepository<TEntity>),
                    typeof(TCustomRepository),
                    repositoryLifetime
                )
             );

            return services;
        }
    }
}
