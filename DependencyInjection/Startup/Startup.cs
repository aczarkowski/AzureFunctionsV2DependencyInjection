using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using System;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using Startup.Abstract;
using Startup.Concrete;

[assembly: WebJobsStartup(typeof(Startup.Startup), "Startup")]
namespace Startup
{
    public class Startup: IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddTransient<IWelcomeService, WelcomeService>();
            builder.Services.AddSingleton<IBindingProvider, GenericBindingProvider<IWelcomeService>>();
        }
    }
}
