using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class Submission
    {
        public string Code { get; set; }
        public string Result { get; set; }
        public bool IsError { get; set; }

        public Submission()
        {

        }

        public Submission(string template)
        {
            Code = template; //"using System; \r\n\r\nnamespace First \r\n{ \r\n   public class Program \r\n   { \r\n      public static void Main() \r\n      { \r\n         Console.WriteLine(int.Parse(Console.ReadLine()) * 2); \r\n      } \r\n   } \r\n} ";
        }
    }
}
