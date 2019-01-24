using System;
using System.IO;

namespace TextAnalyzerApp
{
   public class Program
   {
      static void Main(string[] args)
      {
         //string publicHtmlPath = @"\xampp\htdocs\public_html";

         string filePath = "/Dropbox/01_tmp_home/new 37.txt";

         string[] lines = File.ReadAllLines(filePath);

         TextAnalyzer textAnalyzer = new TextAnalyzer();
         textAnalyzer.AnalyzeFile(filePath);

         Console.ReadLine();
      }
   }
}
