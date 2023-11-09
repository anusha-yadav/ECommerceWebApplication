using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceContext _context;

        public UserRepository(ECommerceContext context)
        {
            _context = context;
        }

        public Users GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public Users GetUserByUsernameAndEmail(string username,string email)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username &&  user.Email==email);
        }

        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public Users GetUserByUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = GetUserByUsername(username);
            if(user != null)
            {
                user.Password = Encrypt(password);
                return true;
            }
            return false;
        }

        public string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        public List<Users> Users()
        {
            return _context.Users.ToList();
        }


    }
}
