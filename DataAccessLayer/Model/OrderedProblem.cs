using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class OrderedProblem
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
        public int ProblemSetId { get; set; }
        public ProblemSet ProblemSet { get; set; }
    }
}
