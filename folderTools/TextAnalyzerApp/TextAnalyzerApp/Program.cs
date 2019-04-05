using System;
using System.IO;

namespace TextAnalyzerApp
{
   public class Program
   {
      //Skończyłem na:
      //Trzeba dopisać areEqual dla kolekcji headerów
      //Wystarczy dopisać Equal dla klasy header i powinno działać

      static void Main(string[] args)
      {
         //string publicHtmlPath = @"\xampp\htdocs\public_html";

         //string filePath = "/Dropbox/01_tmp_home/new 37.txt";
         //string filePath = "/Dropbox/03_files_abb/retrospective/19_03_29.txt";
          string filePath = "../../.././TextAnalyzerTests/TextFiles/TextFile01.txt";

         string[] lines = File.ReadAllLines(filePath);

         TextAnalyzer textAnalyzer = new TextAnalyzer();
         var subHeaders = textAnalyzer.AnalyzeFile(filePath);

         Console.ReadLine();
      }
   }
}
