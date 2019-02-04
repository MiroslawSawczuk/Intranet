using System.Collections.Generic;

namespace Cqrs.Validators
{
    public interface IValidationResult
    {
        ICollection<string> Errors { get; }
        void AddError(string message);
    }
}
