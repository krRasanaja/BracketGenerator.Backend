using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentBracketGenerator.Tests
{
    public class TournamentSimulatorTests
    {
        private readonly Mock<IRandomGenerator> _mockRandomGenerator;
        private readonly TournamentSimulator _simulator;
        private readonly Mock<ITournament> _mockTournament;

        public TournamentSimulatorTests()
        {
            _mockRandomGenerator = new Mock<IRandomGenerator>();
            _simulator = new TournamentSimulator(_mockRandomGenerator.Object);
            _mockTournament = new Mock<ITournament>();
        }

        [Fact]
        public void SimulateTournament_CallsAllSimulationMethods()
        {
            _simulator.SimulateTournament(_mockTournament.Object);

            _mockTournament.Verify(t => t.AssignTeamsToGroups(), Times.Once);
            _mockTournament.Verify(t => t.SimulateGroupStage(It.IsAny<IRandomGenerator>()), Times.Once);
            _mockTournament.Verify(t => t.CreateKnockoutStage(), Times.Once);
            _mockTournament.Verify(t => t.SimulateTournament(It.IsAny<IRandomGenerator>()), Times.Once);
        }
    }
}
