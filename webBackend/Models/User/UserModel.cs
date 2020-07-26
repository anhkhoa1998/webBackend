using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.User
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountType Type { get; set; }
     
    }
}
