using System.Collections.Generic;

namespace RebuildNgtApp
{
   public class GeneralExecutor
   {
      private CmdExecutor cmdExecutor;

      GeneralExecutor(CmdExecutor cmdExecutor)
      {
         this.cmdExecutor = cmdExecutor;
      }

      public CommandResult ExecuteCommand(string command)
      {
         //Todo
         //Najpierw zrobić wczytywanie z pliku tekstowego command tak jak zapisuje notatki z wcięciami i nagłówkamiyhj

         if (command == "rebuildngt")
         {
            List<string> commands = new List<string>();

            commands.Add("rmdir /s /Q \"D:\\GIT\\NGT_Source\\Source\\NGT\\.vs");
            commands.Add("rmdir /s /Q \"D:\\GIT\\NGT_Source\\Source\\NGT\\Tests\"");
            commands.Add("rmdir /s /Q \"D:\\GIT\\NGT_Source\\Source\\NGT\\Bin\"");

            //commands.Add();

            //cmdExecutor.ExecuteCommand("cmd.exe", @"");
         }

         return new CommandResult();
      }
   }
}
