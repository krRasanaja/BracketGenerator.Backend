using BracketGenerator.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentBracketGenerator.Tests
{
    public class RandomGeneratorTests
    {
        [Fact]
        public void Next_ReturnsValueWithinRange()
        {
            var generator = new RandomGenerator();
            var result = generator.Next(10);
            Assert.InRange(result, 0, 9);
        }
    }
}
