using System.Diagnostics;
using System.Text;

namespace PowershellExecutionPolicy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string getMachinePolicy = "-Command \"& {Get-ExecutionPolicy -Scope MachinePolicy}\"";
            const string getUserPolicy = "-Command \"& {Get-ExecutionPolicy -Scope UserPolicy}\"";
            const string getProcessEP = "-Command \"& {Get-ExecutionPolicy -Scope Process}\"";
            const string getCurrentUserEP = "-Command \"& {Get-ExecutionPolicy -Scope CurrentUser}\"";
            const string getLocalMachineEP = "-Command \"& {Get-ExecutionPolicy -Scope LocalMachine}\"";

            //const string setMachinePolicy = "-Command \"& { Set-ExecutionPolicy -Scope MachinePolicy -ExecutionPolicy Undefined }\"";
            //const string setUserPolicy = "-Command \"& { Set-ExecutionPolicy -Scope UserPolicy -ExecutionPolicy Undefined }\"";
            const string setProcessEP = "-Command \"& { Set-ExecutionPolicy -Scope Process -ExecutionPolicy Undefined }\"";
            const string setCurrentUserEP = "-Command \"& { Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Undefined }\"";
            const string setLocalMachineEP = "-Command \"& { Set-ExecutionPolicy -Scope LocalMachine -ExecutionPolicy Undefined }\"";
            //const string getEPList = "-Command \"& {Get-ExecutionPolicy -list}\"";

            string[] ArgumentStrings = new string[] {
                getMachinePolicy, 
                getUserPolicy, 
                getProcessEP, 
                getCurrentUserEP, 
                getLocalMachineEP,
                setProcessEP, 
                setCurrentUserEP,
                setLocalMachineEP
            };            

            Array.ForEach(ArgumentStrings, (str) =>
            {
                var process = new Process();
                var outputStringBuilder = new StringBuilder();
                var errorStringBuilder = new StringBuilder();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Verb = "RunAs";
                process.StartInfo.FileName = "powershell";
                process.OutputDataReceived += (sender, args) => outputStringBuilder.AppendLine(args.Data);
                process.ErrorDataReceived += (sender, args) => errorStringBuilder.AppendLine(args.Data);
                process.StartInfo.Arguments = str;
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit(1000);
                Console.WriteLine("Output: " + outputStringBuilder.ToString());
                Console.WriteLine("Error: " + errorStringBuilder.ToString());
            });
        }
    }
}