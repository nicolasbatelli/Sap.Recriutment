using AutoFixture;
using Moq;
using Sat.Recriutment.Core.Data.Interfaces;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Entities.Interfaces;
using Sat.Recruitment.Core.Managers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.CoreTests
{
    [CollectionDefinition("UsersManagerTests", DisableParallelization = true)]
    public class UsersManagerTests
    {
        [Fact]
        public async Task CreateUser_IsSuccess()
        {
            // arrange
            var fixture = new Fixture();
            Mock<IDataProvider> mockDataProvider = new Mock<IDataProvider>();
            fixture.Customize<User>(c => c.With(x => x.Email, "asdasd@asdasd.com"));
            var user = fixture.Create<User>();
            mockDataProvider.Setup(x => x.CreateUser(It.IsAny<IUser>())).ReturnsAsync(user).Verifiable();

            var usersManager = new UsersManager(mockDataProvider.Object);

            // act
            var resultUser = await usersManager.CreateUser(user);

            // Assert
            mockDataProvider.Verify(x => x.CreateUser(It.IsAny<IUser>()), Times.Once);
            Assert.Equal(user, resultUser);
        }

        [Fact]
        public async Task CreateUser_ThrowsException_UserIsDuplicated()
        {
            // arrange
            var fixture = new Fixture();
            Mock<IDataProvider> mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(x => x.CreateUser(It.IsAny<IUser>())).ThrowsAsync(new Exception("User is duplicated")).Verifiable();

            var usersManager = new UsersManager(mockDataProvider.Object);

            fixture.Customize<User>(c => c.With(x => x.Email, "asdasd@asdasd.com"));
            var user = fixture.Create<User>();

            // act & assert
            await Assert.ThrowsAsync<Exception>(async () => await usersManager.CreateUser(user));
        }
    }
}
