using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebTest.Models;

namespace WebTest.Controllers
{
    public class AdminController : Controller
    {

        #region login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = FormsAuthentication.Authenticate(model.UserName, model.Password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return Redirect(Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password!");
                    return View();
                }
            }
            return View();
        }
        #endregion

        #region problem
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            using (var context = new Context())
            {
                return View(context.Problems.ToList());
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult EditView(int problemId)
        {
            using (var context = new Context())
            {
                return View("Edit", context.Problems.FirstOrDefault(p => p.ProblemId == problemId));
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult NewProblem()
        {
            Problem problem = new Problem();
            return View("Edit", problem);
        }

        [Authorize(Users = "admin")]
        public ActionResult Edit(Problem problem)
        {
            using (var context = new Context())
            {
                if (ModelState.IsValid)
                {
                    Problem originalProblem = context.Problems.FirstOrDefault(p => p.ProblemId == problem.ProblemId);
                    if (originalProblem == null)
                    {
                        originalProblem = new Problem();
                        context.Problems.Add(originalProblem);
                    }

                    originalProblem.Title = problem.Title;
                    originalProblem.Description = problem.Description;
                    originalProblem.Template = problem.Template;

                    context.SaveChanges();
                }
                if (ModelState.IsValid) return View("Index", context.Problems.ToList());
                else return View(problem);
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult DeleteProblem(int problemId)
        {
            using (var context = new Context())
            {
                context.Problems.Remove(context.Problems.FirstOrDefault(p => p.ProblemId == problemId));
                context.SaveChanges();
                return View("Index", context.Problems.ToList());
            }
        }
        #endregion

        #region problemSet
        [Authorize(Users = "admin")]
        public ActionResult ProblemSetIndex()
        {
            using (var context = new Context())
            {
                return View("ProblemSet", context.ProblemSets.Include("OrderedProblems.Problem").ToList());
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult ProblemSetEditView(int problemSetId)
        {
            using (var context = new Context())
            {
                EditProblemSetViewModel viewModel = new EditProblemSetViewModel()
                {
                    ProblemSet = context.ProblemSets.Include("OrderedProblems.Problem").FirstOrDefault(p => p.Id == problemSetId),
                    Problems = context.Problems.ToList()
                };
                return View("EditProblemSetView", viewModel);
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult NewProblemSet()
        {
            using (var context = new Context())
            {
                EditProblemSetViewModel viewModel = new EditProblemSetViewModel()
                {
                    ProblemSet = new ProblemSet()
                    {
                        OrderedProblems = new List<OrderedProblem>()
                    },
                    Problems = context.Problems.ToList()
                };
                return View("EditProblemSetView", viewModel);
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult EditProblemSet(ProblemSet problemSet, string problemsOrder)
        {
            using (var context = new Context())
            {
                if (ModelState.IsValid)
                {
                    ProblemSet originalProblemSet = context.ProblemSets.Include("OrderedProblems").FirstOrDefault(p => p.Id == problemSet.Id);
                    if (originalProblemSet == null)
                    {
                        originalProblemSet = new ProblemSet();
                        context.ProblemSets.Add(originalProblemSet);
                    }

                    originalProblemSet.Name = problemSet.Name;
                    originalProblemSet.Description = problemSet.Description;
                    if (originalProblemSet.OrderedProblems == null)
                        originalProblemSet.OrderedProblems = new List<OrderedProblem>();
                    context.OrderedProblems.RemoveRange(originalProblemSet.OrderedProblems);
                    originalProblemSet.OrderedProblems.Clear();
                    int problemOrder = 0;
                    string[] problemIds = problemsOrder.Split(',');
                    foreach (string problemIdString in problemIds)
                    {
                        int problemId = 0;
                        if (!int.TryParse(problemIdString, out problemId)) continue;
                        if (!context.Problems.Any(p => p.ProblemId == problemId)) continue;
                        OrderedProblem orderedProblem = new OrderedProblem()
                        {
                            ProblemId = problemId,
                            Order = ++problemOrder
                        };
                        originalProblemSet.OrderedProblems.Add(orderedProblem);
                    }

                    context.SaveChanges();
                }
                if (ModelState.IsValid) return View("ProblemSet", context.ProblemSets.Include("OrderedProblems.Problem").ToList());
                else
                {
                    EditProblemSetViewModel viewModel = new EditProblemSetViewModel()
                    {
                        ProblemSet = problemSet,
                        Problems = context.Problems.ToList()
                    };
                    return View("EditProblemSetView", viewModel);
                }
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult DeleteProblemSet(int problemSetId)
        {
            using (var context = new Context())
            {
                context.ProblemSets.Remove(context.ProblemSets.FirstOrDefault(p => p.Id == problemSetId));
                context.SaveChanges();
                return View("ProblemSet", context.ProblemSets.Include("OrderedProblems.Problem").ToList());
            }
        }
        #endregion

        #region answerKey
        [Authorize(Users = "admin")]
        public ActionResult AnswerKey(int problemId)
        {
            using (var context = new Context())
            {
                return View(context.Problems.Include("AnswerKeys").FirstOrDefault(p => p.ProblemId == problemId));
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult NewAnswerKey(int problemId)
        {
            using (var context = new Context())
            {
                Problem problem = context.Problems.Include("AnswerKeys").FirstOrDefault(p => p.ProblemId == problemId);
                AnswerKey answerKey = new AnswerKey()
                {
                    ProblemId = problemId
                };
                problem.AnswerKeys.Add(answerKey);
                context.SaveChanges();

                return View("AnswerKey", problem);
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult SaveAnswerKey(int answerKeyId, int problemId, bool fixedRowOrder, string input, string output)
        {
            using (var context = new Context())
            {
                AnswerKey originalAnswerKey = context.AnswerKeys.FirstOrDefault(a => a.AnswerKeyId == answerKeyId);

                originalAnswerKey.Input = input;
                originalAnswerKey.Output = output;
                originalAnswerKey.FixedRowOrder = fixedRowOrder;

                context.SaveChanges();
                return View("AnswerKey", context.Problems.Include("AnswerKeys").FirstOrDefault(p => p.ProblemId == problemId));
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult DeleteAnswerKey(int answerKeyId)
        {
            using (var context = new Context())
            {
                AnswerKey answerKey = context.AnswerKeys.FirstOrDefault(a => a.AnswerKeyId == answerKeyId);
                int problemId = answerKey.ProblemId;
                context.AnswerKeys.Remove(answerKey);
                context.SaveChanges();
                return View("AnswerKey", context.Problems.Include("AnswerKeys").FirstOrDefault(p => p.ProblemId == problemId));
            }
        }
        #endregion
 
    }
}