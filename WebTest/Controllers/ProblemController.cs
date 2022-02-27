using CodeTester;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebTest.Models;

namespace WebTest.Controllers
{
    public class ProblemController : Controller
    {
        public ActionResult Index(int problemId)
        {
            Problem problem = null;
            using (var context = new Context())
                problem = context.Problems.FirstOrDefault(p => p.ProblemId == problemId);

            return View(new ProblemViewModel()
            {
                Problem = problem,
                Submission = new Submission(problem.Template)
            });
        }

        public ActionResult IndexWithProblemSet(int orderedProblemId)
        {

            using (var context = new Context())
            {
                OrderedProblem orderedProblem = context.OrderedProblems.Include("Problem").Include("ProblemSet.OrderedProblems").FirstOrDefault(o => o.Id == orderedProblemId);
                ProblemSet problemSet = orderedProblem.ProblemSet;
                OrderedProblem nextOrderedProblem = problemSet.OrderedProblems.FirstOrDefault(p => p.Order == orderedProblem.Order + 1);
                return View(new ProblemViewModel()
                {
                    Problem = orderedProblem.Problem,
                    Submission = new Submission(orderedProblem.Problem.Template),
                    ProblemSetTitle = problemSet.Name,
                    NrOfCurrentProblem = orderedProblem.Order,
                    NrOfProblems = problemSet.OrderedProblems.Count,
                    OrderedProblemId = orderedProblem.Id,
                    NextOrderedProblemId = (nextOrderedProblem == null) ? -1 : nextOrderedProblem.Id
                });
            }
        }

        [HttpPost]
        public ActionResult EvaluateCode(ProblemViewModel model)
        {
            Compiler compiler = new Compiler();
            CompileResult compileResult = compiler.CompileCode(model.Submission.Code);
            Problem problem = null;
            using (var context = new Context())
                problem = context.Problems.Include("AnswerKeys").FirstOrDefault(p => p.ProblemId == model.Problem.ProblemId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---- Compiling results: -----------------------");
            sb.AppendLine(compileResult.ToString());

            bool isValid = true;
            if (!compiler.HasError)
            {
                int testNr = 0;
                foreach (TestCase testCase in problem.GenerateTestCases())
                {
                    TestCaseResult output = compiler.Run(testCase);
                    if (!output.IsValid) isValid = false;
                    sb.AppendLine(string.Format("-----Testcase #{0}: ------------------------", ++testNr));
                    sb.AppendLine(string.Format("Input: {0}", testCase.InputLines));
                    sb.AppendLine(string.Format("Your output: {0}", output.OutputText.TrimEnd(new char[] { '\r', '\n', ' ' })));
                    sb.AppendLine(string.Format("Expected output: {0}", testCase.OutputLines));
                    sb.AppendLine();
                }
            }

            model.Submission.Result = sb.ToString();
            model.Submission.IsError = compiler.HasError || !isValid;
            model.Problem = problem;

            if (!model.Submission.IsError)
            {
                if (Session["SolvedProblems"] == null) Session["SolvedProblems"] = new List<int>();
                if (!((List<int>)Session["SolvedProblems"]).Contains(model.Problem.ProblemId))
                    ((List<int>)Session["SolvedProblems"]).Add(model.Problem.ProblemId);
            }

            if (model.NextOrderedProblemId != 0 && model.OrderedProblemId != 0)
                return View("IndexWithProblemSet", model);
            else
                return View("Index", model);
        }
    }
}