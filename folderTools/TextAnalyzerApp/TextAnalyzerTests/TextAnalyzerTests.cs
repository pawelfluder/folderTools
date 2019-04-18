using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

using TextAnalyzerApp;

namespace TextAnalyzerTests
{
   [TestFixture]
   public class TextAnalyzerTests
   {
      [TestCaseSource("GetTestCases")]
      public void TextAnalyzerTest1(TestCase testCase)
      {
          var lines = testCase.InputLines;

         var textAnalyzer = new TextAnalyzer();
         var analyzerHeaders = textAnalyzer.FindHeaders(lines);
         
         Assert.AreEqual(testCase.ExpectedResultedHeaders, analyzerHeaders);

         //HeadersAreEqual(headers2, headers);
         //var result = headers2.SequenceEqual(headers);
         //var a = headers2.All(headers.Contains) && headers2.Count == headers.Count;
        }

       public static List<TestCase> GetTestCases()
      {


         var testRootDirectory = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName;
          var textFilesDirectory = testRootDirectory + @"\" + "TextFiles";

          List<TestCase> TestCases = new List<TestCase>();
          string[] filesPaths = Directory.GetFiles(textFilesDirectory, "*.txt", SearchOption.AllDirectories);
          var listOfResultedHeaders = GetResultedHeaders();
          for (int i = 0; i < filesPaths.Length; i++)
          {
              var lines = File.ReadAllLines(filesPaths[i]);
              TestCases.Add(new TestCase(lines, listOfResultedHeaders[i]));
          }

          return TestCases;
      }

       public static List<List<Header>> GetResultedHeaders()
       {
           var result = new List<List<Header>>();

           //TextFileO1.txt
           result.Add(new List<Header>()
           {
               new Header("Usefull", new List<string>()
               {
                   "Are we collected?",
               }),
           });

           //TextFile02.txt
           result.Add(new List<Header>()
           {
               new Header("tittle", new List<string>()
               {
                   "rebuildplugin",
               }),
               new Header("delete plugin folders and packages", new List<string>()
               {
                   "rmdir /s /Q \"D:\\git\\NGT_Source\\Source\\PluginReferenceImpl\\IEDPlugin\\.vs\"",
                   "rmdir /s /Q \"D:\\GIT\\NGT_Source\\Source\\PluginReferenceImpl\\IEDPlugin\\bin\"",
                   "cd /d \"D:\\GIT\\NGT_Source\\Source\\PluginReferenceImpl\\IEDPlugin",
                   "CleanupPackages",
               }),
               new Header("restore plugin nuget packages", new List<string>()
               {
                   "D:\\UseFullFiles\\nuget.exe restore",
                   "rmdir /s /Q \"D:\\GIT\\NGT_Source\\Source\\PluginReferenceImpl\\IEDPlugin\\bin\"",
                   "D:\\GIT\\NGT_Source\\Source\\PluginReferenceImpl\\IEDPlugin\\IedPluginReferenceImpl.sln",
               }),
           });

           return result;
       }
   }
}
