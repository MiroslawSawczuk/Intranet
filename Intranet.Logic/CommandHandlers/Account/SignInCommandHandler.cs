using Cqrs.Commands;
using Cqrs.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Intranet.Logic.CommandHandlers.Account
{
    public class SignInCommand : ICommand
    {
    }

    internal class SignInCommandHandler : CommandHandlerBase<SignInCommand>
    {
        public override void Execute(SignInCommand command)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = "/post-login",
            };

            Body = new ChallengeResult(new List<string> { "Microsoft" }, authenticationProperties);
        }
    }
}
