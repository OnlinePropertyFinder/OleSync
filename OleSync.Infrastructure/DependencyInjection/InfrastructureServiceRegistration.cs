using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.People.Repositories;
using OleSync.Domain.Shared.Services;
using OleSync.Infrastructure.Boards;
using OleSync.Infrastructure.Committees;
using OleSync.Infrastructure.People;
using OleSync.Infrastructure.Persistence.Context;
using System.Reflection;

namespace OleSync.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region context
            services.AddDbContext<OleSyncContext>(options =>
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            #endregion

            services.AddAutoMapper(cfg =>
            {
                cfg.AddExpressionMapping();
            },

            Assembly.GetExecutingAssembly());

            #region Repository registrations
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IBoardCommitteeRepository, BoardCommitteeRepository>();
            services.AddScoped<ICommitteeRepository, CommitteeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IFileService, FileService>();
            #endregion

            return services;
        }
    }
}
