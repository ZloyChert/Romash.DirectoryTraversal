using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryTraversal.DirectoryTraversalEventArgs;

namespace DirectoryTraversal
{
    public class DirectoryTraversalIterator
    {
        public Func<string, bool> Checker { get; set; }

        public event EventHandler<FileEventArgs> RequiredFileFound;
        public event EventHandler<FileEventArgs> FileFound;
        public event EventHandler<DirectoryEventArgs> RequiredDirectoryFound;
        public event EventHandler<DirectoryEventArgs> DirectoryFound;


        public DirectoryTraversalIterator(Func<string, bool> checker)
        {
            Checker = checker;
        }

        public IEnumerable<string> WalkDirectoryTree(DirectoryInfo root)
        {
            var files = root.GetFiles("*.*");
            var subDirectories = root.GetDirectories();

            foreach (FileInfo file in files)
            {
                OnRaiseFileFoundEvent(new FileEventArgs(file));
                if (Checker(file.FullName))
                {
                    OnRaiseRequiredFileFoundEvent(new FileEventArgs(file));
                    yield return file.FullName;
                }
            }

            foreach (DirectoryInfo directory in subDirectories)
            {
                OnRaiseDirectoryFoundEvent(new DirectoryEventArgs(directory));
                if (Checker(directory.FullName))
                {
                    OnRaiseRequiredDirectoryFoundEvent(new DirectoryEventArgs(directory));
                    yield return directory.FullName;
                    foreach (var item in WalkDirectoryTree(directory))
                    {
                        yield return item;
                    }
                }
            }
        }

        protected virtual void OnRaiseFileFoundEvent(FileEventArgs fileEventArgs)
        {
            RequiredFileFound?.Invoke(this, fileEventArgs);
        }

        protected virtual void OnRaiseRequiredFileFoundEvent(FileEventArgs fileEventArgs)
        {
            FileFound?.Invoke(this, fileEventArgs);
        }

        protected virtual void OnRaiseRequiredDirectoryFoundEvent(DirectoryEventArgs directoryEventArgs)
        {
            RequiredDirectoryFound?.Invoke(this, directoryEventArgs);
        }

        protected virtual void OnRaiseDirectoryFoundEvent(DirectoryEventArgs directoryEventArgs)
        {
            DirectoryFound?.Invoke(this, directoryEventArgs);
        }
    }
}
