namespace Winnosuke.Services
{
    public interface IGarageAccessService
    {
        Task<List<int>> GetGarageIdsForUserAsync(int userId);
    }
}
