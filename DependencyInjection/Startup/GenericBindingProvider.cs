using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Startup
{
    internal class GenericBindingProvider<T> : IBindingProvider where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericBindingProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ParameterInfo parameter = context.Parameter;
            if (parameter.ParameterType != typeof(T))
            {
                return Task.FromResult<IBinding>(null);
            }

            IBinding binding = new GenericBinding<T>(parameter, _serviceProvider);
            return Task.FromResult(binding);
        }
    }
}
