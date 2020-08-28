using LeopardToolKit.AspNetCore.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtension
    {
        public static IServiceCollection AddAndAutoStartMemoryEventBus(this IServiceCollection services, Assembly[] handlerAssemblies)
        {
            services.AddMemoryEventBus(handlerAssemblies);
            services.AddHostedService<MemoryEventBusStartBackgroundService>();

            return services;
        }
    }
}
