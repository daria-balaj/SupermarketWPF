using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{
    public enum Type
    {
        Cashier,
        Administrator
    }

    public class User
    {
        [Key]
        
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Type Role;
        public User() { }
        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            Password = password;
            Role = isAdmin ? Type.Administrator : Type.Cashier;
        }
    }
}
