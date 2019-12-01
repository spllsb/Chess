
using System.Net.Http;
using Chess.Api;
using Newtonsoft.Json;
using Chess.Infrastructure.DTO;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Chess.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UsersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            // Arrange
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            // Act
            var email = "user1@email.com";
            var response = await _client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(responseString);

            user.Email.Should().BeEquivalentTo(email);
        }


    }
}