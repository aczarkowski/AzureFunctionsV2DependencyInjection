using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Startup
{
    internal class GenericBinding<T> : IBinding where T : class
    {
        private readonly ParameterInfo _parameter;
        private readonly IServiceProvider _serviceProvider;

        public GenericBinding(ParameterInfo parameter, IServiceProvider serviceProvider)
        {
            _parameter = parameter;
            _serviceProvider = serviceProvider;
        }

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            if (value == null || !_parameter.ParameterType.IsAssignableFrom(value.GetType()))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to convert value to {0}.", _parameter.ParameterType));
            }

            IValueProvider valueProvider = new ValueBinder(value, _parameter.ParameterType);
            return Task.FromResult<IValueProvider>(valueProvider);

        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            T service = _serviceProvider.GetService(typeof(T)) as T;

            return BindAsync(service, context.ValueContext);

        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = _parameter.Name
            };
        }

        public bool FromAttribute => false;


        private sealed class ValueBinder : IValueBinder
        {
            private readonly object _tracer;
            private readonly Type _type;

            public ValueBinder(object tracer, Type type)
            {
                _tracer = tracer;
                _type = type;
            }

            public Type Type => _type;

            public Task<object> GetValueAsync() => Task.FromResult(_tracer);

            public string ToInvokeString() => null;

            public Task SetValueAsync(object value, CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}
