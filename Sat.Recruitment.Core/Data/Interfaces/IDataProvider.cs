using Sat.Recruitment.Core.Entities.Interfaces;
using System.Threading.Tasks;

namespace Sat.Recriutment.Core.Data.Interfaces
{
    public interface IDataProvider
    {
        public Task<IUser> CreateUser(IUser user);
    }
}

