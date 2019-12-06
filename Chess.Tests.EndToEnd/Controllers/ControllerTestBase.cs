using System.Net.Http;
using Chess.Api;
using Xunit;

namespace Chess.Tests.EndToEnd.Controllers
{
    public abstract class ControllerTestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected HttpClient Client;

        public ControllerTestBase(CustomWebApplicationFactory<Startup> factory)
        {
            // Arrange
            Client = factory.CreateClient();
        }
    }
}