using System;

namespace IndexTypeFinderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Przemyśleć strukturę i napisać testy dla klas
            //Napisać wyświetlanie problemów - odwrotność do rekursywności, dodanie parenta
            string publicHtmlPath = @"\xampp\htdocs\public_html";
            
            ConsoleHelper _consoleHelper = new ConsoleHelper();
            FolderStructure finder = new FolderStructure();
            ConsoleTreePrinter consoleTreePrinter = new ConsoleTreePrinter();
            IndexTypeFinder indexTypeFinder = new IndexTypeFinder();
            IndexUpdater indexUpdater = new IndexUpdater();
            IndexDateFinder indexDateFinder = new IndexDateFinder();

            FolderItem folderItem = finder.Find(publicHtmlPath);
            indexTypeFinder.Find(folderItem, publicHtmlPath);
            indexDateFinder.Find(folderItem, publicHtmlPath);
            consoleTreePrinter.Print(folderItem);

            bool doYouWantToUpdate = _consoleHelper.DoYouWantToUpdate();

            if (doYouWantToUpdate)
            {
                indexUpdater.Update(folderItem, publicHtmlPath);
                indexTypeFinder.Find(folderItem, publicHtmlPath);
                indexDateFinder.Find(folderItem, publicHtmlPath);
                consoleTreePrinter.Print(folderItem);
            }
            //Thank you we finish in 10 seconds or you can close console window manually
            Console.ReadLine();
        }
    }
}
