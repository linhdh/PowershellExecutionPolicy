using System.Diagnostics;
using System.Text;

namespace PowershellExecutionPolicy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var process = new Process();
            var outputStringBuilder = new StringBuilder();
            var errorStringBuilder = new StringBuilder();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = "-Command \"& {Get-ExecutionPolicy -list}\"";
            process.OutputDataReceived += (sender, args) => outputStringBuilder.AppendLine(args.Data);
            process.ErrorDataReceived += (sender, args) => errorStringBuilder.AppendLine(args.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit(10000);
            Console.WriteLine("Output: " + outputStringBuilder.ToString());
            Console.WriteLine("Error: " + errorStringBuilder.ToString());
        }
    }
}