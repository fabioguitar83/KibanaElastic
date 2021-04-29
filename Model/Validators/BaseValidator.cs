using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace KibanaSerilog.Domain.Validators
{
    public abstract class BaseValidator
    {
        [NotMapped]
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        protected BaseValidator()
        {
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        public List<string> ReturnValidationErrors()
        {
            return ValidationResult.Errors.Select(p => p.ErrorMessage).ToList();
        }

        public virtual string ReturnValidationErrorsToString()
        {
            return ValidationResult.Errors.Select(p => p.ErrorMessage).Aggregate((i, j) => i + Environment.NewLine + j);
        }
    }
}
