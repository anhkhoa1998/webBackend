using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.User
{
    public class UserInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> ListClass { get; set; }
    }
}
