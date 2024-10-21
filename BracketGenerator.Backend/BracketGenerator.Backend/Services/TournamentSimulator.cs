using BracketGenerator.Backend.Interfaces;

namespace BracketGenerator.Backend.Services
{
    public class TournamentSimulator : ITournamentSimulator
    {
        private readonly IRandomGenerator _randomGenerator;

        public TournamentSimulator(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public void SimulateTournament(ITournament tournament)
        {
            tournament.AssignTeamsToGroups();
            tournament.SimulateGroupStage(_randomGenerator);
            tournament.CreateKnockoutStage();
            tournament.SimulateTournament(_randomGenerator);
        }
    }
}
