using System;
using System.IO;

namespace DirectoryTraversal.DirectoryTraversalEventArgs
{
    public class FileEventArgs : EventArgs
    {
        public FileEventArgs(FileInfo fileInfo)
        {
            FileInfoArgs = fileInfo;
        }

        public FileInfo FileInfoArgs { get; }
    }
}
