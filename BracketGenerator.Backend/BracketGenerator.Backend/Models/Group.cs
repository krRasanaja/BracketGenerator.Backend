using BracketGenerator.Backend.Interfaces;

namespace BracketGenerator.Backend.Models
{
    public class Group : IGroup
    {
        public string Name { get; set; }
        public List<ITeam> Teams { get; set; } = new List<ITeam>();
        public List<IMatch> Matches { get; set; } = new List<IMatch>();
        public List<ITeam> Standings { get; set; } = new List<ITeam>();

        public void SimulateMatches(IRandomGenerator randomGenerator)
        {
            for (int i = 0; i < Teams.Count; i++)
            {
                for (int j = i + 1; j < Teams.Count; j++)
                {
                    var match = new Match { Team1 = Teams[i], Team2 = Teams[j] };
                    match.Winner = randomGenerator.Next(2) == 0 ? match.Team1 : match.Team2;
                    match.Loser = match.Winner == match.Team1 ? match.Team2 : match.Team1;
                    Matches.Add(match);
                }
            }
        }

        public void CalculateStandings()
        {
            var points = new Dictionary<ITeam, int>();
            foreach (var team in Teams)
            {
                points[team] = Matches.Count(m => m.Winner == team) * 3 +
                               Matches.Count(m => m.Team1 == team && m.Winner == null || m.Team2 == team && m.Winner == null);
            }

            Standings = Teams.OrderByDescending(t => points[t]).ToList();
        }
    }
}
