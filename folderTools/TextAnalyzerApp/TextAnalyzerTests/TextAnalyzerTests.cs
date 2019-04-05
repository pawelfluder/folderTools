using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

using TextAnalyzerApp;

namespace TextAnalyzerTests
{
   [TestFixture]
   public class TextAnalyzerTests
   {
      [Test]
      public void TextAnalyzerTest1()
      {
         var lines = GetTextFileLines01();

         var textAnalyzer = new TextAnalyzer();
         var headers = textAnalyzer.FindHeaders(lines);

         var headers2 = new List<Header>()
         {
            new Header("Usefull",new List<string>()
            {
               "Are we collected?",
            }),
         };
         
         Assert.AreEqual(headers2, headers);

         //HeadersAreEqual(headers2, headers);
         //var result = headers2.SequenceEqual(headers);
         //var a = headers2.All(headers.Contains) && headers2.Count == headers.Count;
        }

      private string[] GetTextFileLines01()
      {
         var workingDirectory1 = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName;
         var filePath01 = workingDirectory1 + @"\" + @"TextFiles\TextFile01.txt";
         var lines = File.ReadAllLines(filePath01);
         return lines;
      }
   }
}
