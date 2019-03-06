using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderCheckerApp
{
    public class IndexUpdater
    {
        private string rootPath;
        private string itemsPath;
        private string templatesPath;
        private string testingPath;

        public IndexUpdater(string rootPath)
        {
            this.rootPath = rootPath;
            itemsPath = string.Concat(rootPath, @"\", "items");
            templatesPath = string.Concat(rootPath, @"\", "templates");
            testingPath = string.Concat(rootPath, @"\", "testing");
        }

        private bool ForbiddenPath(string path)
        {
            return path.Contains(itemsPath) ||
                 path.Contains(templatesPath) ||
                 path.Contains(testingPath);
        }

        public void Update(FolderItem folderItem, string folderPath)
        {            
            UpdateRecursively(folderItem, folderPath);                       
        }

        public void UpdateRecursively(FolderItem folderItem, string folderPath)
        {
            if (ForbiddenPath(folderPath) == false)
            {
                CopyIndex(folderItem, folderPath);
                foreach (var subFolderItem in folderItem.SubFolderItems)
                {
                    string subFolderPath = string.Concat(folderPath, @"\", subFolderItem.Name);
                    UpdateRecursively(subFolderItem, subFolderPath);
                }
            }
        }

        public void CopyIndex(FolderItem folderItem, string folderPath)
        {
            var type = folderItem.Type;
            var typeName = type.ToString();
            
            if (type != IndexType.unknown)
            {
                string itemsPath = string.Concat(rootPath, @"\", "items");
                string itemTypePath = String.Concat(itemsPath, @"\", typeName);

                string lastFolderPath = FindLastFolder(itemTypePath);
                string lastFolderPathForIndex = string.Concat(lastFolderPath, @"\", "index.php");
                string folderPathForIndex = string.Concat(folderPath, @"\", "index.php");

                File.Delete(folderPathForIndex);
                File.Copy(lastFolderPathForIndex, folderPathForIndex);
            }
        }

        private string FindLastFolder(string itemTypePath)
        {
            List<string> subDirectories = Directory.GetDirectories(itemTypePath).ToList();
            subDirectories.Sort();
            string lastFolderPath = subDirectories.Last();

            return lastFolderPath;
        }
    }
}
