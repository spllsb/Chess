using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Chess.Tests.Services
{
    public class TournamentServiceTest
    {
        [Test]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var TournamentRepositoryMock  = new Mock<ITournamentRepository>();
            var mapperMock = new Mock<IMapper>();

            var userService = new TournamentService(TournamentRepositoryMock.Object,mapperMock.Object);
            await userService.CreateAsync("Lapska masakra",10);

            TournamentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Tournament>()),Times.Once);    
        }
    }
}