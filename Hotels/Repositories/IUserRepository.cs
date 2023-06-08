using Hotels.Models;

namespace Hotels.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User?> FindByFirstName(string FirstName);
        public IEnumerable<User?> FindByLastName(string LastName);
        public User? FindByEmail(string Email);
        public User? FindByUsername(string Username);
       
    }
}
