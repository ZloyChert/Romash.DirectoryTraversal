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
        public Func<string, bool> Filter { get; set; }

        public event EventHandler<FileEventArgs> RequiredFileFound;
        public event EventHandler<FileEventArgs> FileFound;
        public event EventHandler<DirectoryEventArgs> RequiredDirectoryFound;
        public event EventHandler<DirectoryEventArgs> DirectoryFound;
        public event EventHandler<MessageEventArgs> TraversalStart;
        public event EventHandler<MessageEventArgs> TraversalFinished;

        /// <summary>
        /// Ctor with parameters
        /// </summary>
        /// <param name="filter">Delegate for filtering founded files</param>
        public DirectoryTraversalIterator(Func<string, bool> filter)
        {
            Filter = filter ?? throw new ArgumentNullException($"{nameof(filter)} is required");
        }

        /// <summary>
        /// Traversal of directory tree
        /// </summary>
        /// <param name="rootDirectory">Root to start traversal</param>
        /// <returns>IEnumerable of directorys'(or files') names</returns>
        public IEnumerable<string> WalkDirectoryTree(DirectoryInfo rootDirectory)
        {
            OnRaiseTraversalStart(new MessageEventArgs($"Traversal start: {rootDirectory}"));
            if (rootDirectory == null)
            {
                throw new ArgumentNullException($"{nameof(rootDirectory)} is required");
            }
            FileInfo[] files = rootDirectory.GetFiles("*.*");
            DirectoryInfo[] subDirectories = rootDirectory.GetDirectories();

            foreach (FileInfo file in files)
            {
                FileEventArgs fileArgs = new FileEventArgs(file);
                OnRaiseFileFoundEvent(fileArgs);
                if (fileArgs.StopTraversal)
                {
                    yield break;
                }
                if (Filter(file.FullName))
                {
                    FileEventArgs nessesaryFileArgs = new FileEventArgs(file);
                    OnRaiseRequiredFileFoundEvent(nessesaryFileArgs);
                    if (nessesaryFileArgs.StopTraversal)
                    {
                        yield break;
                    }
                    if (!fileArgs.ExcludeFile && !nessesaryFileArgs.ExcludeFile)
                    {
                        yield return file.FullName;
                    }
                }

            }

            foreach (DirectoryInfo directory in subDirectories)
            {
                DirectoryEventArgs directoryArgs = new DirectoryEventArgs(directory);
                OnRaiseDirectoryFoundEvent(directoryArgs);
                if (directoryArgs.StopTraversal)
                {
                    yield break;
                }
                if (Filter(directory.FullName))
                {
                    DirectoryEventArgs nessesaryDirectoryArgs = new DirectoryEventArgs(directory);
                    OnRaiseRequiredDirectoryFoundEvent(nessesaryDirectoryArgs);
                    if (nessesaryDirectoryArgs.StopTraversal)
                    {
                        yield break;
                    }
                    if (!directoryArgs.ExcludeFile && !nessesaryDirectoryArgs.ExcludeFile)
                    {
                        yield return directory.FullName;
                        foreach (var item in WalkDirectoryTree(directory))
                        {
                            yield return item;
                        }
                    }
                }
            }
            OnRaiseTraversalFinished(new MessageEventArgs($"Traversal finished"));
        }

        protected virtual void OnRaiseFileFoundEvent(FileEventArgs fileEventArgs)
        {
            FileFound?.Invoke(this, fileEventArgs);
        }

        protected virtual void OnRaiseRequiredFileFoundEvent(FileEventArgs fileEventArgs)
        {
            RequiredFileFound?.Invoke(this, fileEventArgs);
        }

        protected virtual void OnRaiseRequiredDirectoryFoundEvent(DirectoryEventArgs directoryEventArgs)
        {
            RequiredDirectoryFound?.Invoke(this, directoryEventArgs);
        }

        protected virtual void OnRaiseDirectoryFoundEvent(DirectoryEventArgs directoryEventArgs)
        {
            DirectoryFound?.Invoke(this, directoryEventArgs);
        }

        protected virtual void OnRaiseTraversalStart(MessageEventArgs messageEventArgs)
        {
            TraversalStart?.Invoke(this, messageEventArgs);
        }

        protected virtual void OnRaiseTraversalFinished(MessageEventArgs messageEventArgs)
        {
            TraversalFinished?.Invoke(this, messageEventArgs);
        }
    }
}
