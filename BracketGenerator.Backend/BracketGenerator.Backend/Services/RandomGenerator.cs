using BracketGenerator.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Backend.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }
}
