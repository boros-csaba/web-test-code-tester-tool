using DataAccessLayer.Model;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeTester
{
    public class Compiler
    {
        public string AssemblyPath { get; set; }

        public bool HasError { get; set; }

        public Compiler()
        {
        }

        public CompileResult CompileCode(string code)
        {
            CompileResult result = new CompileResult();
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = true;
            compilerParameters.IncludeDebugInformation = true;
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParameters, code);

            for (int i = 0; i < results.Errors.Count; i++)
                if (results.Errors[i].IsWarning)
                    result.Warnings.Add(results.Errors[i].ErrorText);
                else
                    result.Errors.Add(results.Errors[i].ErrorText);

            if (!result.HasErrors) AssemblyPath = results.PathToAssembly;
            else HasError = true;
            return result;
        }

        public TestCaseResult Run(TestCase testCase)
        {
            if (AssemblyPath == null) return null;


            ProcessStartInfo startInfo = new ProcessStartInfo(AssemblyPath);
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process process = Process.Start(startInfo);
            StreamWriter sw = process.StandardInput;

            foreach (string inputLine in testCase.InputLines)
                sw.WriteLine(inputLine);

            if (!process.WaitForExit(1000))
                return new TestCaseResult()
                {
                    OutputText = "Your program timed out!",
                    IsValid = false
                };

            TestCaseResult result = new TestCaseResult();
            result.OutputText = process.StandardOutput.ReadToEnd();
            result.IsValid = IsValid(result.OutputText.Split('\r'), testCase.OutputLines);
            return result;
        }

        private bool IsValid(string[] actualOutput, string[] expectedOutput)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < actualOutput.Length; i++)
                sb.Append(actualOutput[i]);
            string actualOutputText = sb.ToString().Trim(new char[] { ' ', '\r', '\n' });

            sb = new StringBuilder();
            for (int i = 0; i < expectedOutput.Length; i++)
                sb.Append(expectedOutput[i]);
            string expectedOutputText = sb.ToString().Trim(new char[] { ' ', '\r', '\n' });

            return actualOutputText.Equals(expectedOutputText);
        }
    }
}
