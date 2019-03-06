using FolderCheckerApp;
using NUnit.Framework;

namespace FolderCheckerTests
{
    [TestFixture()]
    class ConsoleTreePrinterTest
    {
        [Test]
        public void CreateTreeTest()
        {
            ConsoleTreePrinter consoleTreePrinter = new ConsoleTreePrinter();
            
            FolderItem folderItem0 = new FolderItem("public_html");
            FolderItem folderItem1 = new FolderItem("01");
            FolderItem folderItem2 = new FolderItem("02");
            FolderItem folderItem3 = new FolderItem("03");

            folderItem0.AddSubFolder(folderItem1);
            folderItem1.SetParent(folderItem0);

            folderItem1.AddSubFolder(folderItem2);
            folderItem2.SetParent(folderItem1);

            folderItem2.AddSubFolder(folderItem3);
            folderItem3.SetParent(folderItem2);

            var gg = consoleTreePrinter.CreateTree(folderItem3);


        }
    }
}
