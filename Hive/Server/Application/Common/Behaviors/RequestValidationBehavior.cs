using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Hive.Server.Application.Common.Exceptions.ValidationException;

namespace Hive.Server.Application.Common.Behaviors
{
    /// <summary>
    /// Adds Fluent Validation to request pipeline
    /// </summary>
    /// <see cref="https://code-maze.com/cqrs-mediatr-fluentvalidation/"/>
    /// <typeparam name="TRequest">Request Type</typeparam>
    /// <typeparam name="TResponse">Response Type</typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults
                    .SelectMany(r => r.Errors).Where(f => f != null)
                    .Select(prop => prop.ErrorMessage)
                    .ToArray();

                if (failures.Any())
                    throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
