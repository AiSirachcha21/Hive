using System.Collections.Generic;
using Hive.Domain.Errors;

namespace Hive.Server.Application.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public string[] Failures { get; }

        public ValidationException(string[] failures)
            : base("Validation Failure", "One or more validation errors occurred")
        {
            Failures = failures;
        }
    }
}
