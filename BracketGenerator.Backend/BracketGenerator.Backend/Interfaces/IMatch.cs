namespace BracketGenerator.Backend.Interfaces
{
    public interface IMatch
    {
        ITeam Team1 { get; }
        ITeam Team2 { get; }
        ITeam Winner { get; set; }
        ITeam Loser { get; set; }
    }
}
