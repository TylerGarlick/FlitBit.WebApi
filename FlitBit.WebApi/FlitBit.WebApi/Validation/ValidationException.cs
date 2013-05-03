using System;
using System.Collections.Generic;

namespace FlitBit.WebApi
{
    public class ValidationException : Exception
    {
        public IEnumerable<IValidationEntityError> Errors { get; set; }

        public ValidationException(IEnumerable<IValidationEntityError> errors)
        {
            Errors = errors;
        }
    }
}