using System.Collections;
using System.Collections.Generic;
using Header = TextAnalyzerApp.Header;

namespace TextAnalyzerTests
{
    public class TestCase : IEnumerable<TestCase>
    {

        public string[] InputLines { get; }

        public List<Header> ExpectedResultedHeaders { get; }

        public TestCase(string[] inputLines, List<Header> expectedResultedHeaders)
        {
            InputLines = inputLines;
            ExpectedResultedHeaders = expectedResultedHeaders;
        }

        //public void AddInputs(string[] input)
        //{
        //    inputLines.Add(input);
        //}

        List<TestCase> myTestCases = new List<TestCase>();

        public TestCase this[int index]
        {
            get { return myTestCases[index]; }
            set { myTestCases.Insert(index, value); }
        }

        public IEnumerator<TestCase> GetEnumerator()
        {
            return myTestCases.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}