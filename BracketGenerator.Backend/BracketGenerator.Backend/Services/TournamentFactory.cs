using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Models;

namespace BracketGenerator.Backend.Services
{
    public class TournamentFactory : ITournamentFactory
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IRandomGenerator _randomGenerator;

        public TournamentFactory(ITeamRepository teamRepository, IRandomGenerator randomGenerator)
        {
            _teamRepository = teamRepository;
            _randomGenerator = randomGenerator;
        }

        public ITournament CreateTournament(int choice)
        {
            var tournament = new Tournament(_randomGenerator);
            string jsonFilePath = choice == 1 ? "Data/countries32.json" : "Data/countries64.json";

            try
            {
                tournament.Teams = _teamRepository.GetTeams(jsonFilePath);
            }
            catch (Exception)
            {
                tournament.Teams = GenerateTeams(choice == 1 ? 32 : 64);
            }

            tournament.CreateGroups(choice == 1 ? 8 : 16);
            return tournament;
        }

        private List<ITeam> GenerateTeams(int numTeams)
        {
            var teams = new List<ITeam>();
            for (int i = 1; i <= numTeams; i++)
            {
                teams.Add(new Team { Name = $"Team {i}", Seed = i });
            }
            return teams;
        }
    }
}
