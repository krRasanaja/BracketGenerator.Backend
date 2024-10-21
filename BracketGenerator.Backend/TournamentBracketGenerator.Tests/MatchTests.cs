using BracketGenerator.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentBracketGenerator.Tests
{
    public class MatchTests
    {
        [Fact]
        public void Match_Properties_SetCorrectly()
        {
            var team1 = new Team { Name = "Team 1", Seed = 1 };
            var team2 = new Team { Name = "Team 2", Seed = 2 };
            var match = new Match { Team1 = team1, Team2 = team2 };

            Assert.Equal(team1, match.Team1);
            Assert.Equal(team2, match.Team2);
            Assert.Null(match.Winner);
            Assert.Null(match.Loser);
        }
    }
}
