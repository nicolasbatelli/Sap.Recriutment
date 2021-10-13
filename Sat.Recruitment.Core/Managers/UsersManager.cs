using Sat.Recriutment.Core.Data.Interfaces;
using Sat.Recruitment.Core.Entities.Interfaces;
using Sat.Recruitment.Core.Managers.Interfaces;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Managers
{
    public class UsersManager : IUsersManager
    {
        private readonly IDataProvider _dataProvider;

        public UsersManager(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task<IUser> CreateUser(IUser user)
        {
            return await _dataProvider.CreateUser(user);
        }
    }
}
