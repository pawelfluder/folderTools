using System;
using System.Diagnostics;
using TextAnalyzerApp;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

namespace RebuildNgtApp
{
   class Program
   {
      static void Main(string[] args)
      {
         CmdExecutor cmdExecutor = new CmdExecutor();

         while (true)
         {
            var testing = new testing();
            testing.Create();

            string filePath = "../../../commands/rebuildplugin.txt";

            TextAnalyzer textAnalyzer = new TextAnalyzer();
            var headers = textAnalyzer.AnalyzeFile(filePath);


            //var input = Console.ReadLine();
            //  //string appName = "C:\\Program Files\\Git\\git-bash.exe";
            //  //string appName = "C:\\Program Files(x86)\\Microsoft Visual Studio\\2017\\Professional\\Common7\\Tools\\VsDevCmd.bat";
            ////string appName = "cmd.exe";
            //  string appName = "C:\\Windows\\System32\\cmd.exe";
            //  string arguments = "/k \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\Common7\\Tools\\VsDevCmd.bat\"";
            //CommandResult commandResult = cmdExecutor.ExecuteCommand(appName, input, arguments, 5000);
            //  Console.WriteLine(commandResult.Output);
         }
      }
   }
}
