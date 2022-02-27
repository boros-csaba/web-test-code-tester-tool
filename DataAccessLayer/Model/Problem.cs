using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Problem
    {
        public int ProblemId { get; set; }
        [Required(ErrorMessage = "A cím nem lehet üres")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A leírás nem lehet üres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A séma nem lehet üres")]
        public string Template { get; set; }

        public List<AnswerKey> AnswerKeys { get; set; }

        public Problem()
        {
            Template = DefaultTemplate;
        }

        public List<TestCase> GenerateTestCases(int maxNrOfTestCases = 10)
        {
            List<TestCase> result = new List<TestCase>();

            foreach (AnswerKey answerKey in AnswerKeys)
            {
                if (answerKey.FixedRowOrder)
                    result.Add(new TestCase()
                    {
                        InputLines = answerKey.Input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries),
                        OutputLines = answerKey.Output.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    });
                else
                {
                    string[] inputLines = answerKey.Input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] outputLines = answerKey.Output.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < inputLines.Length; i++)
                        result.Add(new TestCase()
                        {
                            InputLines = new string[] { inputLines[i] },
                            OutputLines = new string[] { (outputLines.Length > i ? outputLines[i] : string.Empty) }
                        });
                }
            }
            Random rand = new Random();
            return result.OrderBy(x => rand.Next()).ToList();
        }

        public const string DefaultTemplate = "using System; \r\n\r\nnamespace First \r\n{ \r\n   public class Program \r\n   { \r\n      public static void Main() \r\n      { \r\n          \r\n      } \r\n   } \r\n} ";
    }
}
