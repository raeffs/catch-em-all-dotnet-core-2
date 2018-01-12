using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;

namespace Reafftec.CatchEmAll.WebJobs
{
    public class JobActivator : IJobActivator
    {
        private readonly IServiceProvider service;

        public JobActivator(IServiceProvider service)
        {
            this.service = service;
        }

        public T CreateInstance<T>()
        {
            return this.service.GetService<T>();
        }
    }
}
