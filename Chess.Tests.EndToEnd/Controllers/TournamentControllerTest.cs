using System;
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
        var tournamentsName = "Camido";
        var turnamentId = "ff1a5a46-1781-465f-9468-e3c98c54d73c";
        //When
        var response = await Client.GetAsync($"tournaments/{turnamentId}");
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var tournament = JsonConvert.DeserializeObject<TournamentDto>(responseString);
        
        //Then
        tournament.Name.Should().BeEquivalentTo(tournamentsName);
        }
    }
}