using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Models;

namespace BracketGenerator.Backend.Services
{
    public class Tournament : ITournament
    {
        public List<ITeam> Teams { get; set; }
        public List<List<IMatch>> Rounds { get; set; }
        public List<IGroup> Groups { get; set; }

        private readonly IRandomGenerator _randomGenerator;

        public Tournament(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
            Teams = new List<ITeam>();
            Rounds = new List<List<IMatch>>();
            Groups = new List<IGroup>();
        }

        public void CreateGroups(int numGroups)
        {
            for (int i = 0; i < numGroups; i++)
            {
                string groupName = $"Group {(char)('A' + i)}";
                Groups.Add(new Group { Name = groupName });
            }
        }

        public void AssignTeamsToGroups()
        {
            int teamIndex = 0;
            foreach (var group in Groups)
            {
                for (int i = 0; i < Teams.Count / Groups.Count; i++)
                {
                    group.Teams.Add(Teams[teamIndex]);
                    teamIndex++;
                }
            }
        }

        public void SimulateGroupStage(IRandomGenerator randomGenerator)
        {
            foreach (var group in Groups)
            {
                group.SimulateMatches(randomGenerator);
                group.CalculateStandings();
            }
        }

        public void CreateKnockoutStage()
        {
            var knockoutTeams = new List<ITeam>();
            foreach (var group in Groups)
            {
                knockoutTeams.Add(group.Standings[0]);
                knockoutTeams.Add(group.Standings[1]);
            }

            Teams = knockoutTeams;
            Rounds.Clear();
        }

        public void SimulateTournament(IRandomGenerator randomGenerator)
        {
            var currentRound = CreateFirstRound();
            SimulateRound(currentRound, randomGenerator);
            Rounds.Add(currentRound);

            while (currentRound.Count > 1)
            {
                currentRound = CreateNextRound(currentRound);
                SimulateRound(currentRound, randomGenerator);
                Rounds.Add(currentRound);
            }
        }

        private List<IMatch> CreateFirstRound()
        {
            var firstRound = new List<IMatch>();
            for (int i = 0; i < Teams.Count; i += 2)
            {
                firstRound.Add(new Match { Team1 = Teams[i], Team2 = Teams[i + 1] });
            }
            return firstRound;
        }

        private List<IMatch> CreateNextRound(List<IMatch> previousRound)
        {
            var nextRound = new List<IMatch>();
            for (int i = 0; i < previousRound.Count; i += 2)
            {
                nextRound.Add(new Match { Team1 = previousRound[i].Winner, Team2 = previousRound[i + 1].Winner });
            }
            return nextRound;
        }

        private void SimulateRound(List<IMatch> round, IRandomGenerator randomGenerator)
        {
            foreach (var match in round)
            {
                AdvanceTeam(match, randomGenerator);
            }
        }

        private void AdvanceTeam(IMatch match, IRandomGenerator randomGenerator)
        {
            match.Winner = randomGenerator.Next(2) == 0 ? match.Team1 : match.Team2;
            match.Loser = match.Winner == match.Team1 ? match.Team2 : match.Team1;
        }

        public string GetTournamentWinner()
        {
            return Rounds.Last()[0].Winner.Name;
        }

        public List<string> PathToVictory()
        {
            var winner = Rounds.Last()[0].Winner;
            var path = new List<string>();

            foreach (var group in Groups)
            {
                if (group.Standings.Contains(winner))
                {
                    path.Add($"{winner.Name} finished {group.Standings.IndexOf(winner) + 1} in {group.Name}");
                    break;
                }
            }

            for (int i = 0; i < Rounds.Count; i++)
            {
                var match = Rounds[i].FirstOrDefault(m => m.Winner == winner);
                if (match != null)
                {
                    string roundName = GetRoundName(i, Rounds.Count);
                    path.Add($"{roundName}: {match.Winner.Name} defeated {match.Loser.Name}");
                    winner = match.Winner;
                }
            }

            return path;
        }

        private string GetRoundName(int currentRound, int totalRounds)
        {
            int roundsRemaining = totalRounds - currentRound;

            if (roundsRemaining == 1)
                return "Final";
            if (roundsRemaining == 2)
                return "Semifinal";
            if (roundsRemaining == 3)
                return "Quarterfinal";
            else
                return $"Round {totalRounds - currentRound - 3}";
        }
    }
}
