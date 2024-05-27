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
        Cashier = 0,
        Administrator = 1
    }

    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Type Role { get; set; }
        //public bool IsAdmin { get; set; }
        public User() { }
        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            Password = password;
            Role = isAdmin ? Type.Administrator : Type.Cashier;
        }
    }
}
