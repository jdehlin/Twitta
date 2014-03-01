using Twitta.Website.Models;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface IUsersRepository
    {
        User GetItem(long userId);
        void Insert(User user);
    }
}
