using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTraversal
{
    public class DirectoryTraversalIterator
    {
        public Action<string> ActionOnFile { get; set; }
        public Func<string, bool> Checker { get; set; }

        public DirectoryTraversalIterator(Action<string> actionOnFile, Func<string, bool> checker)
        {
            ActionOnFile = actionOnFile;
            Checker = checker;
        }

        public void WalkDirectoryTree(DirectoryInfo root)
        {
            try
            {
                var files = root.GetFiles("*.*");
                var subDirs = root.GetDirectories();

                foreach (FileInfo fi in files)
                {
                    if (Checker(fi.FullName))
                    {
                        ActionOnFile(fi.FullName);
                    }
                }

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    if (Checker(dirInfo.FullName))
                    {
                        ActionOnFile(dirInfo.FullName);
                        WalkDirectoryTree(dirInfo);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
            }
        }
    }
}
