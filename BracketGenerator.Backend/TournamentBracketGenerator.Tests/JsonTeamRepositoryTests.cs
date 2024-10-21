using BracketGenerator.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentBracketGenerator.Tests
{
    public class JsonTeamRepositoryTests
    {
        [Fact]
        public void GetTeams_ReturnsCorrectNumberOfTeams()
        {
            var repository = new JsonTeamRepository();
            var teams = repository.GetTeams("Data/countries32.json");
            Assert.Equal(32, teams.Count);
        }

        [Fact]
        public void GetTeams_ThrowsException_WhenFileNotFound()
        {
            var repository = new JsonTeamRepository();
            Assert.Throws<System.IO.FileNotFoundException>(() => repository.GetTeams("NonexistentFile.json"));
        }
    }
}
