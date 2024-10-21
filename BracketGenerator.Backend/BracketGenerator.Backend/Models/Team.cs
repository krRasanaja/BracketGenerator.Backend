using BracketGenerator.Backend.Interfaces;

namespace BracketGenerator.Backend.Models
{
    public class Team : ITeam
    {
        public string Name { get; set; }
        public int Seed { get; set; }
    }
}
