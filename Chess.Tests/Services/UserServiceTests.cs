using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Chess.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock  = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object,mapperMock.Object);
            await userService.RegisterAsync("user11@email.com","user11","secret");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Once);    
        }
    }
}