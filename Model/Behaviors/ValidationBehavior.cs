using FluentValidation;
using FluentValidation.Results;
using KibanaSerilog.Domain.Commands;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KibanaSerilog.Domain.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
              where TRequest : IRequest<TResponse> where TResponse : BaseResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
               .Select(v => v.Validate(request))
               .SelectMany(result => result.Errors)
               .Where(f => f != null)
               .ToList();

            return failures.Any()
                     ? Errors(failures)
                     : next();
        }

        private static Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            var response = new BaseResponse(HttpStatusCode.BadRequest, failures.Select(p => p.ErrorMessage));
            
            return Task.FromResult(response as TResponse);
        }

    }
}
