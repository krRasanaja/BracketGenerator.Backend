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
    public class ConsoleUserInterfaceTests
    {
        [Fact]
        public void GetTournamentTypeChoice_ReturnsValidInput()
        {
            var inputConsole = new MockConsole("1\n");
            Console.SetIn(inputConsole);

            var outputConsole = new StringWriter();
            Console.SetOut(outputConsole);

            var ui = new ConsoleUserInterface();
            var result = ui.GetTournamentTypeChoice();

            Assert.Equal(1, result);
        }

        [Fact]
        public void DisplayResults_WritesToConsole()
        {
            var mockTournament = new Mock<ITournament>();
            mockTournament.Setup(t => t.GetTournamentWinner()).Returns("Winner Team");
            mockTournament.Setup(t => t.PathToVictory()).Returns(new List<string> { "Stage 1", "Stage 2" });

            var inputConsole = new MockConsole("\n");
            Console.SetIn(inputConsole);

            var outputConsole = new StringWriter();
            Console.SetOut(outputConsole);

            var ui = new ConsoleUserInterface();
            ui.DisplayResults(mockTournament.Object);

            var consoleOutput = outputConsole.ToString();

            Assert.Contains("Tournament Winner: Winner Team", consoleOutput);
            Assert.Contains("Path to Victory:", consoleOutput);
            Assert.Contains("Stage 1", consoleOutput);
            Assert.Contains("Stage 2", consoleOutput);
        }
    }

    public class MockConsole : TextReader
    {
        private readonly StringReader _input;

        public MockConsole(string input)
        {
            _input = new StringReader(input);
        }

        public override string ReadLine()
        {
            return _input.ReadLine();
        }
    }
}
