using CodeTester;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest.Models;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Problem> problems = null;
            List<ProblemSet> problemSets = null;
            using (var context = new Context())
            {
                problems = context.Problems.ToList();
                problemSets = context.ProblemSets.Include("OrderedProblems").ToList();
            }

            return View(new HomeViewModel()
            {
                Problems = problems,
                ProblemSets = problemSets
            });
        }
    }
}