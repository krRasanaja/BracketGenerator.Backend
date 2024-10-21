using BracketGenerator.Backend.Interfaces;
using BracketGenerator.Backend.Models;
using System.Text.Json;

namespace BracketGenerator.Backend.Services
{
    public class JsonTeamRepository : ITeamRepository
    {
        public List<ITeam> GetTeams(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Team>>(jsonData).Cast<ITeam>().ToList();
        }
    }
}
