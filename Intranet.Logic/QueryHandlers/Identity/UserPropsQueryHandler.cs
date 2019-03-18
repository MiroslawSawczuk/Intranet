﻿using Cqrs.Queries;
using Intranet.Authentication.Tokens;
using Intranet.Users.Repositories;
using System.Threading.Tasks;

namespace Intranet.Logic.QueryHandlers.Identity
{
    public class UserPropsQuery : IQuery<UserPropsDto>
    {
    }

    internal class UserPropsQueryHandler : AsyncQueryHandlerBase<UserPropsQuery, UserPropsDto>
    {
        private readonly ITokenUser tokenUser;
        private readonly IReadUserRepository readUserRepository;

        public UserPropsQueryHandler(ITokenUser tokenUser, IReadUserRepository readUserRepository)
        {
            this.tokenUser = tokenUser;
            this.readUserRepository = readUserRepository;
        }

        public override async Task<UserPropsDto> ExecuteAsync(UserPropsQuery query)
        {
            return await readUserRepository.GetAsync(
                where: u => u.Id.Equals(tokenUser.Id),
                select: u => new UserPropsDto
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
        }
    }

    public class UserPropsDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
