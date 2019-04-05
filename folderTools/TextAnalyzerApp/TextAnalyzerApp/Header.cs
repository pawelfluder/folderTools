using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalyzerApp
{
   public class Header : IEquatable<Header>
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

      public static bool operator ==(Header obj1, Header obj2)
      {
         return obj1.Equals(obj2);
      }

      public static bool operator !=(Header obj1, Header obj2)
      {
         return !obj1.Equals(obj2);
      }

      //public override int GetHashCode()
      //{
      //   return Header.GetHashCode();
      //}

      public bool Equals(Header other)
      {
         var namesAreEqual = Name == other.Name;
          var contentsAreEqual = true;
          var contentCount = Content.Count;
          var otherContentCount = other.Content.Count;
          if (contentCount == otherContentCount)
          {
              for (int i = 0; i < contentCount; i++)
              {
                  contentsAreEqual = contentCount == otherContentCount;
                  if (contentsAreEqual == false)
                  {
                      break;
                  }
                }
          }
          
          bool subHeadersAreEqual = true;
          var subHeadersCount = SubHeaders.Count;
          var otherSubHeaderCount = other.SubHeaders.Count;
          if (subHeadersCount == otherSubHeaderCount && namesAreEqual && contentsAreEqual)
          {
              for (int i = 0; i < subHeadersCount; i++)
              {
                  subHeadersAreEqual = SubHeaders[i].Equals(other.SubHeaders[i]);
                  if (subHeadersAreEqual == false)
                  {
                      break;
                  }
              }
          }

          bool result = namesAreEqual & contentsAreEqual & subHeadersAreEqual;
          return result;
      }

      public override bool Equals(object obj)
      {
         if (!(obj is Header))
         {
            return false;
         }

         var other = (Header) obj;

         return this.Equals(other);
      }
   }
}
