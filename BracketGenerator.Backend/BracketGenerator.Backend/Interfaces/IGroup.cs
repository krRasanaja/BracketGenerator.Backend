namespace BracketGenerator.Backend.Interfaces
{
    public interface IGroup
    {
        string Name { get; }
        List<ITeam> Teams { get; }
        List<IMatch> Matches { get; }
        List<ITeam> Standings { get; }
        void SimulateMatches(IRandomGenerator randomGenerator);
        void CalculateStandings();
    }
}
