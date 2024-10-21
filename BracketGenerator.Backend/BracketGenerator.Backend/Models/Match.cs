using BracketGenerator.Backend.Interfaces;

namespace BracketGenerator.Backend.Models
{
    public class Match : IMatch
    {
        public ITeam Team1 { get; set; }
        public ITeam Team2 { get; set; }
        public ITeam Winner { get; set; }
        public ITeam Loser { get; set; }
    }
}
