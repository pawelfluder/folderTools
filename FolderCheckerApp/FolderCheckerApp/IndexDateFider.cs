using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderCheckerApp
{
    public class IndexDateFinder
    {
        public void Find(FolderItem folderItem, string folderPath)
        {
            AddDateRecursively(folderItem, folderPath);
        }

        public void AddDateRecursively(FolderItem folderItem, string folderPath)
        {
            Date date = Copmare(folderItem, folderPath);
            folderItem.ChangeSourceDate(date);
            foreach (var subFolderItem in folderItem.SubFolderItems)
            {
                string subFolderPath = string.Concat(folderPath, @"\", subFolderItem.Name);
                AddDateRecursively(subFolderItem, subFolderPath);
            }
        }

        public Date Copmare(FolderItem folderItem, string folderPath)
        {
            var type = folderItem.Type;
            
            if (type != IndexType.unknown)
            {
                var typeName = type.ToString();

                string folderPathForIndex = string.Concat(folderPath, @"\", "index.php");
                string itemsPath = @"\xampp\htdocs\public_html\items";
                string itemTypePath = string.Concat(itemsPath, @"\", typeName);

                string[] lines = File.ReadAllLines(folderPathForIndex);

                return FindDateCreation(itemTypePath, lines);
            }

            return new Date();
        }

        private Date FindDateCreation(string itemTypePath, string[] lines)
        {
            List<string> subDirectories = Directory.GetDirectories(itemTypePath).OrderByDescending(i => i).ToList();

            List<string> matchSubDirectories = new List<string>();
            string lastFolderPath = subDirectories.Last();

            foreach (string subDirectory in subDirectories)
            {
                string subDirectoryName = Path.GetFileName(subDirectory);
                string itemTypePathForIndex = String.Concat(itemTypePath, @"\", subDirectoryName, @"\", "index.php");
                string[] subLines = File.ReadAllLines(itemTypePathForIndex);

                if (ArraysEqual<string>(lines, subLines))
                {
                    matchSubDirectories.Add(subDirectoryName);
                }
            }

            if (matchSubDirectories.Count == 1)
            {
                string sub = matchSubDirectories[0];
                var year = sub.Substring(0, 2).ToArray();
                var month = sub.Substring(3, 2).ToArray();
                var day = sub.Substring(6, 2).ToArray();

                Date date = new Date(year, month, day);
                return date;
            }

            return new Date();
        }



        static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }
    }
}
