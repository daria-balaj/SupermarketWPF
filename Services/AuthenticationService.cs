using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Services
{
    public class AuthenticationService
    {
        private readonly DataContext _context;
        public AuthenticationService(DataContext context)
        {
            _context = context;
        }

        public User? Authenticate(string username, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
