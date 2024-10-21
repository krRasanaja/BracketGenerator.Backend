using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentBracketGenerator.Tests
{
    public class GroupTests
    {
        private readonly Mock<IRandomGenerator> _mockRandomGenerator;
        private readonly Group _group;

        public GroupTests()
        {
            _mockRandomGenerator = new Mock<IRandomGenerator>();
            _group = new Group
            {
                Name = "Group A",
                Teams = new List<ITeam>
                {
                    new Team { Name = "Team 1", Seed = 1 },
                    new Team { Name = "Team 2", Seed = 2 },
                    new Team { Name = "Team 3", Seed = 3 },
                    new Team { Name = "Team 4", Seed = 4 }
                }
            };
        }

        [Fact]
        public void SimulateMatches_CreatesCorrectNumberOfMatches()
        {
            _group.SimulateMatches(_mockRandomGenerator.Object);
            Assert.Equal(6, _group.Matches.Count); 
        }

        [Fact]
        public void CalculateStandings_OrdersTeamsCorrectly()
        {
            _mockRandomGenerator.Setup(r => r.Next(2)).Returns(0); 
            _group.SimulateMatches(_mockRandomGenerator.Object);
            _group.CalculateStandings();

            Assert.Equal("Team 1", _group.Standings[0].Name);
            Assert.Equal("Team 4", _group.Standings[3].Name);
        }
    }
}
