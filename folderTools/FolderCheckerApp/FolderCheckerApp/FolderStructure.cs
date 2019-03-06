using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderCheckerApp
{
    public class FolderStructure
    {
        private bool _onlyTwoDigitFolders;

        public FolderItem Find(string path, bool onlyTwoDigitFolders = true)
        {
            _onlyTwoDigitFolders = onlyTwoDigitFolders;
            return GetSubDirectories(path);
        }

        private FolderItem GetSubDirectories(string path)
        {
            string directoryName = Path.GetFileName(path);
            FolderItem folderItem = new FolderItem(directoryName);
            SetAllSubFolders(folderItem, path);

            return folderItem;
        }

        private void SetAllSubFolders(FolderItem folderItem, string path)
        {
            string[] subFoldersNames = GetSubFoldersNames(path);
            string[] names = FilterSubFolderNames(subFoldersNames);
            folderItem.AddSubFoldersNames(names);

            foreach (var subFolderItem in folderItem.SubFolderItems)
            {
                string subFolderPath = string.Concat(path, @"\", subFolderItem.Name);
                subFolderItem.SetParent(folderItem);
                SetAllSubFolders(subFolderItem, subFolderPath);
            }
        }

        private string[] FilterSubFolderNames(string[] subFoldersNames)
        {
            if (_onlyTwoDigitFolders == true)
            {
                return subFoldersNames.Where(x => IsTwoDigitString(x)).ToArray();
            }

            return subFoldersNames;
        }

        private bool IsTwoDigitString(string name)
        {
            if (name != null &&
                name.Length == 2 &&
                char.IsDigit(name[0]) &&
                char.IsDigit(name[1]))
            {
                return true;
            }

            return false;
        }

        private string[] GetSubFoldersNames(string path)
        {
            string[] subFoldersPaths = Directory.GetDirectories(path);
            List<string> subFoldersNames = new List<string>();
            foreach (var subFoldersPath in subFoldersPaths)
            {
                string subFoldersName = Path.GetFileName(subFoldersPath);
                subFoldersNames.Add(subFoldersName);
            }

            return subFoldersNames.ToArray();
        }
    }
}
