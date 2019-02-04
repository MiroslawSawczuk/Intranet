using System.Collections.Generic;

namespace Cqrs.Validators
{
    internal class ValidationResult : IValidationResult
    {
        public ICollection<string> Errors { get; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string message)
        {
            Errors.Add(message);
        }
    }
}
