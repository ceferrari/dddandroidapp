using System;
using App.Domain.Interfaces.Repositories;
using App.Infra.Data.Contexts;
using App.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using App.Application.Interfaces;
using App.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.IoC
{
    public class Bootstrapper
    {
        public static IServiceProvider RegisterServices(IServiceCollection services, string dbPath = null)
        {
            if (!string.IsNullOrEmpty(dbPath))
            {
                services.AddEntityFrameworkSqlite().AddDbContext<SqliteContext>(x => x.UseSqlite($"Filename={dbPath}"));
                services.AddScoped<IRepository, Repository<SqliteContext>>();
            }

            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services.BuildServiceProvider();
        }
    }
}
