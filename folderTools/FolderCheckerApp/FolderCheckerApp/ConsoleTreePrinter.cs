using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderCheckerApp
{
    public class ConsoleTreePrinter
    {
        private char _space = ' ';
        private int _numOfSpaces = 2;
        private string _separator = " - ";

        private List<FolderItem> _problems;

        public ConsoleTreePrinter()
        {
            _problems = new List<FolderItem>();
        }

        public void Print(FolderItem folderItem)
        {
            string line = string.Concat(folderItem.Name, _separator,
                folderItem.Type, _separator,
                folderItem.SourceDate.ToString());

            Console.WriteLine(line);
            PrintAllSubFoldersRecoursevely(folderItem, 1);
            PrintProblems(_problems);
        }

        private void PrintAllSubFoldersRecoursevely(FolderItem folderItem, int level)
        {
            var problems = folderItem.SubFolderItems.Where(x => x.TemporaryNames.Count > 0).Where(x => x.TemporaryNames[0] == "").ToList();
            _problems.AddRange(problems);

            foreach (var subFolderItem in folderItem.SubFolderItems)
            {
                string indentation = new string(_space, _numOfSpaces * level);
                string typeText = subFolderItem.TemporaryNames.Count > 0 ? subFolderItem.TemporaryNames.First() : string.Empty;

                string line = string.Concat(indentation, subFolderItem.Name, _separator,
                    subFolderItem.Type, _separator,
                    subFolderItem.SourceDate.ToString(), _separator,
                    "\"" + typeText + "\"");
                Console.WriteLine(line);

                int nextLevel = level + 1;
                PrintAllSubFoldersRecoursevely(subFolderItem, nextLevel);
            }
        }

        private void PrintProblems(List<FolderItem> foldersList)
        {
            List<FolderItem> foldertrees = new List<FolderItem>();
            foreach (var subFolderItem in foldersList)
            {
                foldertrees.Add(CreateTree(subFolderItem));
            }

            for (int i = 0; i < foldertrees.Count; i++)
            {
                Console.WriteLine("problem {0}", i);
                PrintAllSubFoldersRecoursevely(foldertrees[i], 0);
                Console.WriteLine();
            }
        }

        public FolderItem CreateTree(FolderItem subFolderItem)
        {
            var deepCopy = new FolderItem(subFolderItem);
            deepCopy.CleanSubFolders();
            RecoursevelySetParent(ref deepCopy);

            var rootOfDeepCopy = ReturnRootFolder(deepCopy);
            return rootOfDeepCopy;
        }

        private FolderItem ReturnRootFolder(FolderItem folder)
        { 
            while (true)
            {
                folder = folder.Parent;

                if (folder.Parent == null)
                {
                    return folder;
                }
            }
        }

        private void RecoursevelySetParent(ref FolderItem deepCopy)
        {
            if (deepCopy.Parent != null)
            {
                var parentDeepCopy = new FolderItem(deepCopy.Parent);
                deepCopy.SetParent(parentDeepCopy);
                parentDeepCopy.CleanSubFolders();
                parentDeepCopy.AddSubFolder(deepCopy);

                RecoursevelySetParent(ref parentDeepCopy);
            }
        }
    }
}
