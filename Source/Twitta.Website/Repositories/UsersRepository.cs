using System.Linq;
using Dapper;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public User GetItem(long userId)
        {
            const string sql = "SELECT * FROM Users WHERE UserId = @userId";
            using (var connection = Utilities.Database.GetProfiledOpenConnection())
            {
                return connection.Query<User>(sql, new {userId}).FirstOrDefault();
            }
        }

        public void Insert(User user)
        {
            using (var connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var duplicate = GetItem(user.UserId);
                if (duplicate == null)
                    connection.Insert(user);
            }
        }
    }
}