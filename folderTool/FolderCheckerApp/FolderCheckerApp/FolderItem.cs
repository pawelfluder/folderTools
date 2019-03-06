using System.Collections.Generic;
using System.IO;

namespace FolderCheckerApp
{
    public class FolderItem
    {
        public string Name { get; }
        public List<FolderItem> SubFolderItems { get; }
        public IndexType Type { get; private set; }
        public Date SourceDate { get; private set; }
        public List<string> TemporaryNames { get; }
        public FolderItem Parent { get; private set; }

        public FolderItem(string name)
        {
            Name = name;
            SubFolderItems = new List<FolderItem>();
            Type = IndexType.unknown;
            SourceDate = new Date();
            TemporaryNames = new List<string>();
        }

        public FolderItem(FolderItem input) : this(input.Name)
        {
            SourceDate.ChangeDate(input.SourceDate); 
            AddTemporaryNames(input.TemporaryNames);
            AddSubFolder(input.SubFolderItems);

            //reference
            Parent = input.Parent;
        }

        public void CleanSubFolders()
        {
            SubFolderItems.Clear();
        }

        public void SetParent(FolderItem parent)
        {
            Parent = parent;
        }

        public void AddTemporaryName(string name)
        {
            TemporaryNames.Add(name);
        }

        public void AddTemporaryNames(List<string> nameList)
        {
            TemporaryNames.AddRange(nameList);
        }

        public void AddSubFoldersNames(string[] subFoldersNames)
        {
            foreach (var subFolderName in subFoldersNames)
            {
                SubFolderItems.Add(new FolderItem(subFolderName));
            }
        }
        public void AddSubFolder(List<FolderItem> subFoldersNames)
        {
            SubFolderItems.AddRange(subFoldersNames);
        }

        public void AddSubFolder(FolderItem subFolder)
        {
            SubFolderItems.Add(subFolder);
        }

        public void ChangeType(IndexType type)
        {
            Type = type;
        }

        public void ChangeSourceDate(Date sourceDate)
        {
            SourceDate = sourceDate;
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
