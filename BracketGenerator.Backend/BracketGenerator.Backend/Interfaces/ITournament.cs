namespace BracketGenerator.Backend.Interfaces
{
    public interface ITournament
    {
        List<ITeam> Teams { get; }
        List<List<IMatch>> Rounds { get; }
        List<IGroup> Groups { get; }
        void CreateGroups(int numGroups);
        void AssignTeamsToGroups();
        void SimulateGroupStage(IRandomGenerator randomGenerator);
        void CreateKnockoutStage();
        void SimulateTournament(IRandomGenerator randomGenerator);
        string GetTournamentWinner();
        List<string> PathToVictory();
    }
}
