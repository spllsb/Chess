
using System.Net.Http;
using Chess.Api;
using Newtonsoft.Json;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Chess.Infrastructure.DTO;

namespace Chess.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestBase
    {
        public UsersControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        public async Task given_valid_email_user_should_exist()
        {
            // Act
            var email = "user1@email.com";
            var response = await Client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(responseString);

            user.Email.Should().BeEquivalentTo(email);
        }


    }
}