using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class ProblemViewModel
    {
        public Problem Problem { get; set; }
        public Submission Submission { get; set; }
        public string ProblemSetTitle { get; set; }
        public int NrOfCurrentProblem { get; set; }
        public int NrOfProblems { get; set; }
        public int OrderedProblemId { get; set; }
        public int NextOrderedProblemId { get; set; }
    }
}
