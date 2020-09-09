using LeopardToolKit.AspNetCore.Quartz;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtension
    {
        private static bool hostServiceAdded = false;

        public static IServiceCollection ConfigQuartzJobAndAutoStart<TJob>(this IServiceCollection services, Func<JobBuilder, IJobDetail> configJobDetail = null, Func<TriggerBuilder, ITrigger> configTrigger = null, ServiceLifetime jobLifetime = ServiceLifetime.Transient)
            where TJob : class, IJob
        {
            services.ConfigQuartzJob<TJob>(configJobDetail, configTrigger, jobLifetime);

            services.AutoStartQuartzJob();

            return services;
        }

        public static IServiceCollection AutoStartQuartzJob(this IServiceCollection services)
        {
            if (!hostServiceAdded)
            {
                hostServiceAdded = true;
                services.AddHostedService<QuartzStartBackgroundService>();
            }

            return services;
        }
    }
}
