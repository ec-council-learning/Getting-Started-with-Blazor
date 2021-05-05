namespace MovieApp.Server.Interfaces
{
    public interface IWatchlist
    {
        void ToggleWatchlistItem(int userId, int movieId);
        string GetWatchlistId(int userId);
    }
}
