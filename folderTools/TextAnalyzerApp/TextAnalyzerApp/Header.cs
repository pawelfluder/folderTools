using System;
using System.Collections.Generic;

namespace TextAnalyzerApp
{
   public class Header
   {
      public string Name { get; }

      public List<Header> SubHeaders { get; }

      public List<string> Content { get; }

      public Header(List<Header> subHeaders)
      {
         SubHeaders = subHeaders;
         Content = new List<string>();
      }

      public Header(List<string> content)
      {
         SubHeaders = new List<Header>();
         Content = content;
      }

      public Header(string name, List<string> content, List<Header> subHeaders)
      {
         Name = name;
         SubHeaders = new List<Header>();
         Content = new List<string>();
      }

      public Header(string name, List<string> content)
      {
         Name = name;
         SubHeaders = new List<Header>();
         Content = new List<string>(content);
      }

      public void AddSubHeaders(List<Header> subHeaders)
      {
         SubHeaders.AddRange(subHeaders);
      }

      public void AddSubHeaders(List<string> content)
      {
         Content.AddRange(content);
      }
   }
}
