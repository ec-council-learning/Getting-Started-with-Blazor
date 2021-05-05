using System.ComponentModel.DataAnnotations;

namespace MovieApp.Shared.Dto
{
    public class UserLogin
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string UserTypeName { get; set; }
    }
}
