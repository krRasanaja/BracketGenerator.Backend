using Moq;
using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Services;
using BracketGenerator.Backend.Models;

namespace TournamentBracketGenerator.Tests
{
    public class TournamentTests
    {
        private readonly Mock<IRandomGenerator> _mockRandomGenerator;
        private readonly Tournament _tournament;

        public TournamentTests()
        {
            _mockRandomGenerator = new Mock<IRandomGenerator>();
            _tournament = new Tournament(_mockRandomGenerator.Object);
            _tournament.Teams = new List<ITeam>
            {
                new Team { Name = "Team 1", Seed = 1 },
                new Team { Name = "Team 2", Seed = 2 },
                new Team { Name = "Team 3", Seed = 3 },
                new Team { Name = "Team 4", Seed = 4 },
                new Team { Name = "Team 5", Seed = 5 },
                new Team { Name = "Team 6", Seed = 6 },
                new Team { Name = "Team 7", Seed = 7 },
                new Team { Name = "Team 8", Seed = 8 }
            };
        }

        [Fact]
        public void CreateGroups_CreatesCorrectNumberOfGroups()
        {
            _tournament.CreateGroups(2);
            Assert.Equal(2, _tournament.Groups.Count);
        }

        [Fact]
        public void AssignTeamsToGroups_DistributesTeamsEvenly()
        {
            _tournament.CreateGroups(2);
            _tournament.AssignTeamsToGroups();

            Assert.Equal(4, _tournament.Groups[0].Teams.Count);
            Assert.Equal(4, _tournament.Groups[1].Teams.Count);
        }

        [Fact]
        public void SimulateGroupStage_SimulatesAllGroups()
        {
            _tournament.CreateGroups(2);
            _tournament.AssignTeamsToGroups();
            _tournament.SimulateGroupStage(_mockRandomGenerator.Object);

            Assert.All(_tournament.Groups, group => Assert.Equal(6, group.Matches.Count));
            Assert.All(_tournament.Groups, group => Assert.Equal(4, group.Standings.Count));
        }

        [Fact]
        public void CreateKnockoutStage_SelectsTopTeamsFromGroups()
        {
            _tournament.CreateGroups(2);
            _tournament.AssignTeamsToGroups();
            _tournament.SimulateGroupStage(_mockRandomGenerator.Object);
            _tournament.CreateKnockoutStage();

            Assert.Equal(4, _tournament.Teams.Count);
        }

        [Fact]
        public void SimulateTournament_CompletesTournament()
        {
            _mockRandomGenerator.Setup(r => r.Next(2)).Returns(0); // Always choose first team as winner
            _tournament.SimulateTournament(_mockRandomGenerator.Object);

            Assert.Equal(3, _tournament.Rounds.Count); // 3 rounds for 8 teams
            Assert.Single(_tournament.Rounds.Last()); // Final round should have 1 match
        }

        [Fact]
        public void GetTournamentWinner_ReturnsCorrectWinner()
        {
            _mockRandomGenerator.Setup(r => r.Next(2)).Returns(0); // Always choose first team as winner
            _tournament.SimulateTournament(_mockRandomGenerator.Object);

            Assert.Equal("Team 1", _tournament.GetTournamentWinner());
        }

        [Fact]
        public void PathToVictory_ReturnsCorrectPath()
        {
            _mockRandomGenerator.Setup(r => r.Next(2)).Returns(0); // Always choose first team as winner
            _tournament.SimulateTournament(_mockRandomGenerator.Object);

            var path = _tournament.PathToVictory();
            Assert.Equal(3, path.Count); // Quarterfinal, Semifinal, Final
            Assert.Contains("Final", path.Last());
        }
    }
}
