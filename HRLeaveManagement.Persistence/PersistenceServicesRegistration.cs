using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //pass the connection string into the parameter
            services.AddDbContext<LeaveManagementDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("LeaveManagementConnectionString")));

            // services.AddSingletion
            // singleton - one instance of this service will exist throughout the application
            // ex: logger service, database transaction

            // services.AddTransient
            // transient - new instance is provided to every controller and everyservice

            // services.AddScoped
            // scoped - same within a request, different accross different requests
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;
        }
    }
}
