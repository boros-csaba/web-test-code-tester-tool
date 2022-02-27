using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TestData
    {
        public void GenerateTestData()
        {
            GenerateProblems();
            GenerateAnswerKeys();
            GenerateProblemSets();
        }

        private void GenerateProblems()
        {
            using (var context = new Context())
            {
                context.Problems.Add(new Problem()
                {
                    ProblemId = 1,
                    Title = "Elso feladat",
                    Description = "Meg kell oldani a feladatot"
                });
                context.Problems.Add(new Problem()
                {
                    ProblemId = 2,
                    Title = "Masodik feladat",
                    Description = "Meg kell oldani a feladatot"
                });
                context.Problems.Add(new Problem()
                {
                    ProblemId = 3,
                    Title = "Harmadik feladat",
                    Description = "Meg kell oldani a feladatot"
                });

                context.SaveChanges();
            }
        }

        private void GenerateAnswerKeys()
        {
            using (var context = new Context())
            {
                Problem problem = context.Problems.Include("AnswerKeys").FirstOrDefault(p => p.ProblemId == 1);

                problem.AnswerKeys.Add(new AnswerKey()
                {
                    AnswerKeyId = 1,
                    Input = "14",
                    Output = "28"
                });
                problem.AnswerKeys.Add(new AnswerKey()
                {
                    AnswerKeyId = 2,
                    Input = "2",
                    Output = "4"
                });
                problem.AnswerKeys.Add(new AnswerKey()
                {
                    AnswerKeyId = 3,
                    Input = "1234",
                    Output = "2468"
                });

                context.SaveChanges();
            }
        }

        private void GenerateProblemSets()
        {
            using (var context = new Context())
            {
                ProblemSet problemSet = new ProblemSet();

                problemSet.Name = "First problem set";
                List<Problem> problems = context.Problems.Where(p => p.ProblemId < 3).ToList();
                problemSet.OrderedProblems = new List<OrderedProblem>();
                foreach (Problem problem in problems)
                    problemSet.OrderedProblems.Add(
                        new OrderedProblem()
                        {
                            Order = problem.ProblemId,
                            Problem = problem
                        });                           

                context.ProblemSets.Add(problemSet);
                context.SaveChanges();
            }
        }
    }
}
