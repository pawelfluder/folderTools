using System;
using System.IO;
using System.Linq;

namespace FolderCheckerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Przenieść templates na poziom items i oczyścić items
            //Spróbować usunąć wiecej z index.php np. zostawić samą metodę engine
            //Poprawić metody na duże litery
            //Skończyłem
            //printPageView() jest niepotrzebnie w common, a w text usunałem dużo method i nie wyświetla - trzeba to już dobrze rozkminić

            //Przemyśleć strukturę i napisać testy dla klas
            //Napisać wyświetlanie problemów - odwrotność do rekursywności, dodanie parenta
            //Opis typów do czego służą i jakie mają funkcje


            string publicHtml = "public_html";
            string rootPath = @"\xampp\htdocs";

            CreateTestingFolder(rootPath);

            //string publicHtmlPath = @"\xampp\htdocs\public_html\items\templates";

            ConsoleHelper _consoleHelper = new ConsoleHelper();
            FolderStructure finder = new FolderStructure();
            ConsoleTreePrinter consoleTreePrinter = new ConsoleTreePrinter();
            IndexTypeFinder indexTypeFinder = new IndexTypeFinder(rootPath);
            IndexUpdater indexUpdater = new IndexUpdater(rootPath);
            IndexDateFinder indexDateFinder = new IndexDateFinder(rootPath);

            FolderItem folderItem = finder.Find(rootPath, false);
            indexTypeFinder.Find(folderItem, rootPath);
            indexDateFinder.Find(folderItem, rootPath);
            consoleTreePrinter.Print(folderItem);

            bool doYouWantToUpdate = _consoleHelper.DoYouWantToUpdate();

            if (doYouWantToUpdate)
            {
                indexUpdater.Update(folderItem, rootPath);
                indexTypeFinder.Find(folderItem, rootPath);
                indexDateFinder.Find(folderItem, rootPath);
                consoleTreePrinter.Print(folderItem);
            }
            //Thank you we finish in 10 seconds or you can close console window manually
            Console.ReadLine();


            
        }

        private static void CreateTestingFolder(string path)
        {
            string testingFolderPath = path + "\\" + "testing";
            if (Directory.Exists(testingFolderPath))
            {
                Directory.Delete(testingFolderPath, true);                
            }

            Directory.CreateDirectory(testingFolderPath);

            foreach (IndexType type in (IndexType[])Enum.GetValues(typeof(IndexType)))
            {
                if (type != IndexType.unknown)
                {
                    string itemPath = path + "\\" + "items" + "\\" + type;
                    string lastFolder = FindLastFolder(itemPath);
                    string lastindexFolder = itemPath + "\\" + lastFolder;
                    string destinationDirectory = testingFolderPath + "\\" + type;
                    DirectoryCopy(lastindexFolder, destinationDirectory, true);
                }
            }
        }

        private static string FindLastFolder(string itemsPath)
        {
            var directories = Directory.GetDirectories(itemsPath);
            var lastDirectory = directories.Last();
            return Path.GetFileName(lastDirectory);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
