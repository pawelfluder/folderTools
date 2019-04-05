
using System.Xml.Linq;

namespace RebuildNgtApp
{
   public class testing
   {
      public void Create()
      {
         new XDocument(
            new XElement("root",
               new XElement("header", "value")
            )
         ).Save("foo.xml");
      }
   }
}
