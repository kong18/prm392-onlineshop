using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Validation
{
    public class ValidatorProvider : IValidatorProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValidator<T> GetValidator<T>()
        {
            return _serviceProvider.GetService<IValidator<T>>()!;
        }
    }
}
