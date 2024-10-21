using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TournamentBracketGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();
            var tournamentSimulator = serviceProvider.GetService<ITournamentSimulator>();
            var userInterface = serviceProvider.GetService<IUserInterface>();

            int choice = userInterface.GetTournamentTypeChoice();
            var tournament = tournamentFactory.CreateTournament(choice);

            tournamentSimulator.SimulateTournament(tournament);

            userInterface.DisplayResults(tournament);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IRandomGenerator, RandomGenerator>();
            services.AddSingleton<ITeamRepository, JsonTeamRepository>();
            services.AddSingleton<ITournamentFactory, TournamentFactory>();
            services.AddSingleton<ITournamentSimulator, TournamentSimulator>();
            services.AddSingleton<IUserInterface, ConsoleUserInterface>();
            return services.BuildServiceProvider();
        }
    }
}