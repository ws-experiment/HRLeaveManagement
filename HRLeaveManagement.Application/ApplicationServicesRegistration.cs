using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            //Traverse for every MappingProfile that has inheritance
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
