using System;
using System.IO;

namespace DirectoryTraversal.DirectoryTraversalEventArgs
{
    public class FileEventArgs : EventArgs
    {
        public FileEventArgs(FileInfo fileInfo)
        {
            FileInfoArgs = fileInfo ?? throw new ArgumentNullException($"{nameof(fileInfo)} is required");
        }

        public FileInfo FileInfoArgs { get; }

        public bool StopTraversal { get; set; }
        public bool ExcludeFile { get; set; }
    }
}
