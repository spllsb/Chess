using Chess.Api;
using Chess.Infrastructure.DTO;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Chess.Tests.EndToEnd.Controllers
{
    public class TournamentControllerTest : ControllerTestBase
    {
        public TournamentControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async void given_valid_tournament_name_should_exist()
        {
        //Given
        var tournamentsName = "Kadeci";

        //When
        var response = await Client.GetAsync($"tournaments/{tournamentsName}");
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var tournament = JsonConvert.DeserializeObject<TournamentDto>(responseString);
        
        //Then
        tournament.Name.Should().BeEquivalentTo(tournamentsName);
        }
    }
}