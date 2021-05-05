namespace MovieApp.Server.Models
{
    public partial class WatchlistItem
    {
        public int WatchlistItemId { get; set; }
        public string WatchlistId { get; set; }
        public int MovieId { get; set; }
    }
}
