using Sat.Recriutment.Core.Data.Interfaces;
using Sat.Recriutment.Data.Constants;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recriutment.Data.Providers
{
    public class DataProvider : IDataProvider
    {
        private string _path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

        public async Task<IUser> CreateUser(IUser user)
        {

            var _users = await ReadUsersFromFile();
            var isDuplicated = _users.Any(us => ((us.Email == user.Email || us.Phone == user.Phone) && (us.Name == user.Name && us.Address == user.Address)));

            if (!isDuplicated)
            {
                await WriteUserInFile(user);
                return user;
            }
            else
            {
                throw new Exception("User is Duplicated");
            }
        }

        private async Task<bool> WriteUserInFile(IUser user)
        {
            using (FileStream fs = new FileStream(_path, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                await sw.WriteAsync(new StringBuilder(Environment.NewLine + user.Name + ',' + user.Email + ',' + user.Phone + ',' + user.Address + ',' + user.UserType + ',' + user.Money.ToString()));
                return true;
            }
        }
        private async Task<IEnumerable<IUser>> ReadUsersFromFile()
        {
            var usersList = new List<User>();

            using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var entries = line.Split(',');
                    var user = new User
                    {
                        Name = entries[(int)UserProperties.NAME],
                        Email = entries[(int)UserProperties.EMAIL],
                        Phone = entries[(int)UserProperties.PHONE],
                        Address = entries[(int)UserProperties.ADDRESS],
                        UserType = entries[(int)UserProperties.USER_TYPE],
                        Money = decimal.Parse(entries[(int)UserProperties.MONEY]),
                    };
                    usersList.Add(user);
                }
            }

            return usersList;
        }
    }
}
