using System;

namespace MovieApp.Server.Models
{
    public partial class Watchlist
    {
        public string WatchlistId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
