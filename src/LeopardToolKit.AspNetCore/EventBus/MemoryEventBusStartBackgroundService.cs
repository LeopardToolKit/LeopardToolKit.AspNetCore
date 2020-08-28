using LeopardToolKit.EventBus;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeopardToolKit.AspNetCore.EventBus
{
    internal class MemoryEventBusStartBackgroundService : IHostedService
    {
        private readonly MemoryEventDispatcher memoryEventDispatcher;

        public MemoryEventBusStartBackgroundService(MemoryEventDispatcher memoryEventDispatcher)
        {
            this.memoryEventDispatcher = memoryEventDispatcher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.memoryEventDispatcher.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.memoryEventDispatcher.Stop();
            return Task.CompletedTask;
        }
    }
}
