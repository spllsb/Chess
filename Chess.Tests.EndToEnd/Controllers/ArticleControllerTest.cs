using Chess.Api;
using Chess.Infrastructure.DTO;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Chess.Tests.EndToEnd.Controllers
{
    public class ArticleControllerTest : ControllerTestBase
    {
        public ArticleControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }
        [Fact]
        public async void given_valid_tournament_name_should_exist()
        {
        //Given
        var articleTitle = "nowy artykow";
        var articleGuid = "0d78f06a-c8ea-4495-828d-6a2bae99d6b4";

        //When
        var response = await Client.GetAsync($"articles/{articleGuid}");
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var article = JsonConvert.DeserializeObject<ArticleDto>(responseString);
        
        //Then
        article.Title.Should().BeEquivalentTo(articleTitle);
        }
    }
}