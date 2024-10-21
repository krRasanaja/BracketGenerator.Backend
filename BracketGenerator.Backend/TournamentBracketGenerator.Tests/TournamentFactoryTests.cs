using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Models;
using BracketGenerator.Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentBracketGenerator.Tests
{
    public class TournamentFactoryTests
    {
        private readonly Mock<ITeamRepository> _mockTeamRepository;
        private readonly Mock<IRandomGenerator> _mockRandomGenerator;
        private readonly TournamentFactory _factory;

        public TournamentFactoryTests()
        {
            _mockTeamRepository = new Mock<ITeamRepository>();
            _mockRandomGenerator = new Mock<IRandomGenerator>();
            _factory = new TournamentFactory(_mockTeamRepository.Object, _mockRandomGenerator.Object);
        }

        [Theory]
        [InlineData(1, 32, 8)]
        [InlineData(2, 64, 16)]
        public void CreateTournament_CreatesCorrectTournament(int choice, int expectedTeams, int expectedGroups)
        {
            _mockTeamRepository.Setup(r => r.GetTeams(It.IsAny<string>()))
                .Returns(Enumerable.Range(1, expectedTeams).Select(i => new Team { Name = $"Team {i}", Seed = i }).Cast<ITeam>().ToList());

            var tournament = _factory.CreateTournament(choice);

            Assert.Equal(expectedTeams, tournament.Teams.Count);
            Assert.Equal(expectedGroups, tournament.Groups.Count);
        }

        [Fact]
        public void CreateTournament_GeneratesTeams_WhenRepositoryFails()
        {
            _mockTeamRepository.Setup(r => r.GetTeams(It.IsAny<string>())).Throws(new Exception());

            var tournament = _factory.CreateTournament(1);

            Assert.Equal(32, tournament.Teams.Count);
            Assert.Equal(8, tournament.Groups.Count);
        }
    }
}
