using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace TextAnalyzerApp
{
   public class TextAnalyzer
   {
      public List<Header> AnalyzeFile(string filePath)
      {
         string[] lines = File.ReadAllLines(filePath);

         var subHeaders = FindHeaders(lines);
         return subHeaders;
      }

      public List<Header> FindHeaders(string[] lines)
      {
         List<int> headersNumbers = new List<int>();
         for (int i = 0; i < lines.Length; i++)
         {
            if (IsHeader(lines[i]))
            {
               headersNumbers.Add(i);
            }
         }

         var subHeaders = GetHeaders(lines, headersNumbers);
         return subHeaders;
      }

      private static bool IsHeader(string line)
      {
         if (line.Length >= 2)
         {
            return line.Substring(0, 2) == "//";
         }

         return false;
      }

      public List<Header> GetHeaders(string[] lines, List<int> headersNumbers)
      {
         List<Header> subHeaders = new List<Header>();
         headersNumbers.Add(lines.Length);

         var previousHeaderNumer = headersNumbers.ElementAt(0);

            foreach (var headerNumber in headersNumbers.Skip(1))
            {
               string name = CorrectHeaderName(lines[previousHeaderNumer]);
               int oneAfterHeaderLineNumber = previousHeaderNumer + 1;
               int numberOfContentLines = headerNumber - oneAfterHeaderLineNumber;

               var content = GetContent(lines, oneAfterHeaderLineNumber, numberOfContentLines);

               Header subHeader = new Header(name, content);
               subHeaders.Add(subHeader);
         }

         return subHeaders;
      }

      private List<string> GetContent(string[] lines, int oneAfterHeaderLineNumber, int numberOfContentLines)
      {
         var content = lines.SubArray(oneAfterHeaderLineNumber, numberOfContentLines).ToList();
         var correctedContent = CorrectContent(content);
         return correctedContent;
      }

      private List<string> CorrectContent(List<string> content)
      {
         var result = content.Select(l => l.Replace("\t", "")).ToList();
         return result;
      }

      private string CorrectHeaderName(string headerName)
      {
         var result = headerName.Remove(0, 2);
         return result;
      }

      private static int gg(List<int> headersNumbers, int i)
      {
         
         return headersNumbers[i + 1] ;
      }
   }
}
