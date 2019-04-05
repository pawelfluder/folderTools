﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RebuildNgtApp
{
    public class CmdExecutor
    {
        private StringBuilder Output { get; }

        public CmdExecutor()
        {
            Output = new StringBuilder();
        }

        public CommandResult ExecuteCommand(string appName, string command, string arguments, int timeout)
        {
            Output.Clear();

            CommandResult commandResult = new CommandResult();
            try
            {
                 using (Process p = new Process())
                 {
                     p.StartInfo = new ProcessStartInfo()
                     {
                         FileName = appName,
                         Arguments = arguments,
                         RedirectStandardOutput = true,
                         RedirectStandardInput = true,
                        RedirectStandardError = true,
                         UseShellExecute = false,
                         WorkingDirectory = @"d:\",
                     };
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;


               // event handlers for output & error
               p.OutputDataReceived += p_OutputDataReceived;
                     

                     // start process
                     p.Start();

                     p.StandardInput.Write(command + p.StandardInput.NewLine);

                     p.BeginOutputReadLine();
                     p.WaitForExit(100);
                    
                       commandResult.Output = Output.ToString();
                    
                    p.StandardInput.Write("exit" + p.StandardInput.NewLine);
                    p.WaitForExit(100);
                    if (p.HasExited)
                    {
                       commandResult.ExitCode = p.ExitCode;
                     }
               }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

           ProcessOutput(ref commandResult, command);
           


            return commandResult;
        }

       private void ProcessOutput(ref CommandResult commandResult, string command)
       {
          if (commandResult.Output != null)
          {
             commandResult.Output = RemoveBeginning(commandResult.Output, command);
         }
          
       }

       private string RemoveBeginning(string input, string command)
       {
          var temp = input.Split(new string[] { command }, StringSplitOptions.None).ToList();
          temp.RemoveAt(0);
          //char tabChar = '\t';
          var result = String.Join(String.Empty, temp).TrimStart();
          //result =  ReplaceString(result, "")
          return result;
       }

       private string ReplaceString(string input, string toReplace, string replacement)
       {
          var sb = new StringBuilder(input);
          sb.Replace(toReplace, replacement);

          return sb.ToString();
       }

       private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Output.Append(e.Data);
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
           if (p == null || e.Data == null)
           {
              return;
           }

           var result = e.Data.TrimStart().TrimEnd() + "\n";

           if (result != "\n")
           {
               Output.Append(result);
            }
        }
    }
}
