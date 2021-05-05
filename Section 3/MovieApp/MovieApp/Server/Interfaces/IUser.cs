using MovieApp.Server.Models;
using MovieApp.Shared.Dto;

namespace MovieApp.Server.Interfaces
{
    public interface IUser
    {
        bool RegisterUser(UserMaster user);

        UserLogin AuthenticateUser(UserLogin loginCredentials);

        UserLogin GetCurrentUser(string username);

        bool isUserExists(int userId);
    }
}
