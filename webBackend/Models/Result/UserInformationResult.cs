using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Result
{
    public class UserInformationResult
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ClassResult> ListClass { get; set; }
    }
    public class ClassResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
    }
   
}
