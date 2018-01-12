using System;
using Microsoft.Extensions.DependencyInjection;
using Raefftec.CatchEmAll.DAL;

namespace Reafftec.CatchEmAll.WebJobs
{
    public class ContextFactory
    {
        private readonly IServiceProvider service;

        public ContextFactory(IServiceProvider service)
        {
            this.service = service;
        }

        public Context GetContext()
        {
            return this.service.GetService<Context>();
        }
    }
}
