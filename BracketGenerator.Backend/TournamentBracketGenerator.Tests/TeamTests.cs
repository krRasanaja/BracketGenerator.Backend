using BracketGenerator.Backend.Models;

namespace TournamentBracketGenerator.Tests
{
    public class TeamTests
    {
        [Fact]
        public void Team_Properties_SetCorrectly()
        {
            var team = new Team { Name = "Test Team", Seed = 1 };
            Assert.Equal("Test Team", team.Name);
            Assert.Equal(1, team.Seed);
        }
    }
}
