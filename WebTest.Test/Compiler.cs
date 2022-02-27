using CodeTester;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebTest.Test
{
    [TestFixture]
    public class Compiler
    {
        string textCode = "using System; namespace First { public class Program { public static void Main() { int x = int.Parse(Console.ReadLine()); int result = 0; for (int i = 0; i < x; i++){ result += (int)Math.Sqrt(1); } Console.WriteLine(result); } } }";
        List<string> files = new List<string>();

        [Test]
        public void StressTest()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => RunSingleTest());
                //RunSingleTest();
            }


                File.WriteAllLines(@"c:\ide.txt", files);
        }

        void RunSingleTest()
        {
            CodeTester.Compiler compiler = new CodeTester.Compiler();
            CompileResult result = compiler.CompileCode(textCode);

            files.Add(compiler.AssemblyPath);
            Assert.IsFalse(result.HasErrors);
        }
    }
}
