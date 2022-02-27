using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class AnswerKey
    {
        public int AnswerKeyId { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public bool FixedRowOrder { get; set; }

        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
    }
}
