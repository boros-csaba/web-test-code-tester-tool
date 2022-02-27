using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class TestCase
    {
        public string[] InputLines { get; set; }
        public string[] OutputLines { get; set; }

        public string Output { get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < OutputLines.Length; i++)
                    sb.AppendLine(OutputLines[i]);
                return sb.ToString();
            } }
    }
}
