using System;

namespace IndexTypeFinderApp
{
   public class ConsoleHelper
   {
      public bool DoYouWantToUpdate()
      {
         bool result;
         while (true)
         {
            Console.WriteLine("Do you want to update? Type Yes Or No");
            var answer = Console.ReadLine();

            if (answer == "Yes")
            {
               result = true;
               break;
            }

            if (answer == "No")
            {
               result = false;
               break;
            }
         }

         return result;
      }
   }
}