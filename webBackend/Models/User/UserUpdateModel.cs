using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.User
{
    public class UserUpdateModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> ListClass { get; set; }
    }
}
