namespace BracketGenerator.Backend.Interfaces
{
    public interface ITeamRepository
    {
        List<ITeam> GetTeams(string filePath);
    }
}
