using BaseRepository.Repositories;
using Intranet.Users.Models;
using System.Threading.Tasks;

namespace Intranet.Users.Repositories
{
    public interface IReadUserRepository : IReadRepository<User>
    {
        Task<User> Get(string email);
    }
}
