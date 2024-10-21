namespace BracketGenerator.Backend.Interfaces
{
    public interface IUserInterface
    {
        int GetTournamentTypeChoice();
        void DisplayResults(ITournament tournament);
    }
}
