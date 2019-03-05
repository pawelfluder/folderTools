using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FolderCheckerApp
{
    public class IndexTypeFinder
    {
        private string rootPath;
        private string itemsPath;
        private string templatesPath;
        private string testingPath;

        public IndexTypeFinder(string rootPath)
        {
            this.rootPath = rootPath;
            itemsPath = string.Concat(rootPath, @"\", "items");
            templatesPath = string.Concat(rootPath, @"\", "templates");
            testingPath = string.Concat(rootPath, @"\", "testing");
        }

        private bool ForbiddenPath(string path)
        {
            bool forbidden = path.Contains(itemsPath) ||
                 path.Contains(templatesPath) ||
                 path.Contains(testingPath);

            return forbidden;
        }

        public void Find(FolderItem folderItem, string path)
        {
            FindRecursively(folderItem, path);
        }

        private void FindRecursively(FolderItem folderItem, string path)
        {
            if (ForbiddenPath(path) == false)
            {
                if (CheckIfFolderExists(folderItem, path))
                {
                    string typeName = GetType(path);
                    folderItem.AddTemporaryName(typeName);

                    Enum.TryParse(typeName, out IndexType type);

                    folderItem.ChangeType(type);
                    foreach (var subFolderItem in folderItem.SubFolderItems)
                    {
                        string subFolderPath = string.Concat(path, @"\", subFolderItem.Name);
                        FindRecursively(subFolderItem, subFolderPath);
                    }
                }
            }
        }

        private bool CheckIfFolderExists(FolderItem folderItem, string path)
        {
            bool fileExists = Directory.Exists(path);
            bool correctName = Path.GetFileName(path) == folderItem.Name;
            if (fileExists && correctName)
            {
                return true;
            }

            return false;
        }

        private string GetType(string folderPath)
        {
            string indexPath = string.Concat(folderPath, @"\", "index.php");
            if (File.Exists(indexPath))
            {
                string[] lines = File.ReadAllLines(indexPath);
                List<string> foundLines = new List<string>();

                foreach (string line in lines)
                {
                    if (Condition(line))
                    {
                        foundLines.Add(line);
                    }
                }

                if (foundLines.Count == 1)
                {
                    MatchCollection matches = Regex.Matches(foundLines[0], "\"([^\"]*)\"");
                    string type = matches[0].Value.TrimStart('"').TrimEnd('"').TrimEnd('\\').TrimEnd('/');


                    return type;
                }
            }

            return string.Empty;
        }

        private bool Condition(string line)
        {
            if (line.Contains("$typeName = ") ||
                line.Contains("$typeOfIndex = ") ||
                line.Contains("define('TYPE',"))
            {
                return true;
            }

            return false;
        }
    }
}
