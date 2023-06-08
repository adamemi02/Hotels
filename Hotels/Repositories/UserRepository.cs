using Hotels.Repositories.GenericRepository;
using Hotels.DataBase;
using Hotels.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DataBaseContext context) : base(context) { }

        public IEnumerable<User?> FindByFirstName(string FirstName)
        {
            return _table.Where(u => u.FirstName == FirstName);
        }

        public IEnumerable<User?> FindByLastName(string LastName)
        {
            return _table.Where(u => u.LastName == LastName);
        }

        public User? FindByEmail(string Email)
        {
            return _table.FirstOrDefault(u => u.Email == Email);
        }

        public User? FindByUsername(string Username)
        {
            return _table.FirstOrDefault(u => u.Username == Username);
        }

    }
}
