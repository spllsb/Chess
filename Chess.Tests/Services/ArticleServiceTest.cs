using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Chess.Tests.Services
{
    public class ArticleServiceTest
    {
        [Test]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var ArticleRepositoryMock  = new Mock<IArticleRepository>();
            var mapperMock = new Mock<IMapper>();

            var userService = new ArticleService(ArticleRepositoryMock.Object,mapperMock.Object);
            await userService.CreateAsync("articel about IT","Bla bla bla bal","Juzio");

            ArticleRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Article>()),Times.Once);    
        }
    }
}
