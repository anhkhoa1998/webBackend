using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Project
{
    public class ProjectModel
    {
        public List<Todo> Todo { get; set; }
        public List<Todo> Doing { get; set; }
        public List<Todo> Done { get; set; }
        public string userId { get; set; }
    }
}
