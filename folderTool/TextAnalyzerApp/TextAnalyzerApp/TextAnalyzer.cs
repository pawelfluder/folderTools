using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace TextAnalyzerApp
{
   public class TextAnalyzer
   {
      public void AnalyzeFile(string filePath)
      {
         string[] lines = File.ReadAllLines(filePath);

         FindHeaders(lines);
      }

      public void FindHeaders(string[] lines)
      {
         List<int> headersNumbers = new List<int>();
         for (int i = 0; i < lines.Length; i++)
         {
            if (IsHeader(lines[i]))
            {
               headersNumbers.Add(i);
            }
         }

         GetHeaders(lines, headersNumbers);
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

         for (int i = 0; i < headersNumbers.Count-1; i++)
         {
            string name = lines[headersNumbers.ElementAt(i)];
            string[] content = lines.SubArray(headersNumbers[i]+1, headersNumbers[i+1]);
            Header subHeader = new Header(name, content.ToList());
            subHeaders.Add(subHeader);
         }

         return subHeaders;
      }
   }
}
