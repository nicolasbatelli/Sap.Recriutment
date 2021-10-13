using Sat.Recruitment.Core.Entities.Interfaces;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Managers.Interfaces
{
    public interface IUsersManager
    {
        public Task<IUser> CreateUser(IUser user);
    }
}
