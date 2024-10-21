namespace BracketGenerator.Backend.Interfaces
{
    public interface ITournamentFactory
    {
        ITournament CreateTournament(int choice);
    }
}
