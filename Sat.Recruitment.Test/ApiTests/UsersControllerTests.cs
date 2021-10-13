using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Common;
using Sat.Recruitment.Core.Entities.Interfaces;
using Sat.Recruitment.Core.Managers.Interfaces;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserControllerTests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        [Fact]
        public async Task CreateUser_IsSuccess()
        {
            // arrange
            var fixture = new Fixture();
            Mock<IUsersManager> mockUsersManager = new Mock<IUsersManager>();

            var userController = new UsersController(mockUsersManager.Object);
            fixture.Customize<UserModel>(c => c.With(x => x.Email, "asdasd@asdasd.com"));
            var user = fixture.Create<UserModel>();

            mockUsersManager.Setup(x => x.CreateUser(It.IsAny<IUser>())).ReturnsAsync(user).Verifiable();

            // act
            var actionResult = await userController.CreateUser(user);

            // Assert
            var resultObject = GetObjectResultContent<UserModel>(actionResult);
            Assert.NotNull(resultObject);
            Assert.Equal(resultObject, user);
        }

        [Fact]
        public async Task CreateUser_returnsError_UserIsDuplicated()
        {
            // arrange
            var fixture = new Fixture();
            Mock<IUsersManager> mockUsersManager = new Mock<IUsersManager>();
            mockUsersManager.Setup(x => x.CreateUser(It.IsAny<IUser>())).Throws(new Exception("User Is Duplicated")).Verifiable();

            var userController = new UsersController(mockUsersManager.Object);
            fixture.Customize<UserModel>(c => c.With(x => x.Email, "asdasd@asdasd.com"));
            var user = fixture.Create<UserModel>();

            // act
            var actionResult = await userController.CreateUser(user);

            // Assert;
            var resultObject = GetObjectResultContent<ResultModel>(actionResult);
            Assert.False(resultObject.IsSuccess);
            Assert.Equal("User Is Duplicated", resultObject.Errors.First().ToString());
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
