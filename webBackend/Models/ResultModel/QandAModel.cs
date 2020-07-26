using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models.Answer;

namespace webBackend.Models.ResultModel
{
    public class QandAModel
    {
        public webBackend.Models.Issue.Issue issue { get; set; }
        public List<webBackend.Models.Answer.Answer> Answers { get; set; }
    }
}
