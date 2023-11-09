using E_Commerce_WebApplication.Models;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Users GetUserById(int id);
        Users GetUserByUsernameAndEmail(string username,string email);
        void AddUser(Users user);
        Users GetUserByUsername(string username);
        bool AuthenticateUser(string username,string password);
        string Encrypt(string clearText);
        List<Users> Users();
    }
}
