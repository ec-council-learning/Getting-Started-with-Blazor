using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;
using MovieApp.Shared.Dto;
using System.Linq;

namespace MovieApp.Server.DataAccess
{
    public class UserDataAccessLayer : IUser
    {
        readonly MovieDBContext _dbContext;

        public UserDataAccessLayer(MovieDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool RegisterUser(UserMaster userData)
        {
            bool isUserNameAvailable = CheckUserAvailabity(userData.Username);

            try
            {
                if (isUserNameAvailable)
                {
                    _dbContext.UserMasters.Add(userData);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        bool CheckUserAvailabity(string userName)
        {
            string user = _dbContext.UserMasters.FirstOrDefault(x => x.Username == userName)?.ToString();

            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserLogin AuthenticateUser(UserLogin loginCredentials)
        {
            UserLogin user = new();
            var userDetails = _dbContext.UserMasters
                .FirstOrDefault(u => u.Username == loginCredentials.Username && u.Password == loginCredentials.Password);

            if (userDetails != null)
            {
                user = new UserLogin
                {
                    Username = userDetails.Username,
                    UserId = userDetails.UserId,
                    UserTypeName = userDetails.UserTypeName
                };
            }
            return user;
        }

        public UserLogin GetCurrentUser(string username)
        {
            UserLogin user = new();
            var userDetails = _dbContext.UserMasters.FirstOrDefault(u => u.Username == username);

            if (userDetails != null)
            {
                user = new UserLogin
                {
                    Username = userDetails.Username,
                    UserId = userDetails.UserId,
                    UserTypeName = userDetails.UserTypeName
                };
            }

            return user;
        }

        public bool isUserExists(int userId)
        {
            string user =
                _dbContext.UserMasters.FirstOrDefault(x => x.UserId == userId)?.ToString();

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
