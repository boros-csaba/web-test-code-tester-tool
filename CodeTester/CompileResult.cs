using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTester
{
    public class CompileResult
    {
        public bool HasErrors { get { return (Errors != null && Errors.Count > 0); } }
        public bool HasWarnings { get { return (Warnings != null && Warnings.Count > 0); } }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }

        public CompileResult()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (HasErrors)
            {
                sb.AppendLine("Errors: ");
                foreach (string error in Errors) sb.AppendLine(error);
            }
            else sb.AppendLine("No errors");
            if (HasWarnings)
            {
                sb.AppendLine("Warnings: ");
                foreach (string warning in Warnings) sb.AppendLine(warning);
            }
            else sb.AppendLine("No warnings");
            return sb.ToString();
        }
    }
}
