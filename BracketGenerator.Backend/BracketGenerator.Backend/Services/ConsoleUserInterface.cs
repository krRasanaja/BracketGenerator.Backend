using BracketGenerator.Backend.Interfaces;

namespace BracketGenerator.Backend.Services
{
    public class ConsoleUserInterface : IUserInterface
    {
        public int GetTournamentTypeChoice()
        {
            Console.WriteLine("Choose tournament type (1 for World Cup, 2 for NCAA Soccer): ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayResults(ITournament tournament)
        {
            Console.WriteLine("Tournament Winner: " + tournament.GetTournamentWinner());
            Console.WriteLine("\nPath to Victory:");
            var path = tournament.PathToVictory();
            foreach (var stage in path)
            {
                Console.WriteLine(stage);
            }

            Console.ReadLine();
        }
    }
}
