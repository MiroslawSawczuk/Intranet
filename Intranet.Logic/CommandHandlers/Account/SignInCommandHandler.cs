using System.Collections.Generic;
using Cqrs.Commands;
using Cqrs.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Logic.CommandHandlers.Account
{
    public class SignInCommand : ICommand
    {
    }
    
    internal class SignInCommandHandler : CommandHandlerBase<SignInCommand>
    {
        public override void Validate(SignInCommand command, IValidationResult validationResult)
        {
        }

        public override void Execute(SignInCommand command)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = "/login-post"
            };

            Body = new ChallengeResult(new List<string> {"Microsoft"}, authenticationProperties);
        }
    }
}
