using System;
using System.IO;

namespace DirectoryTraversal.DirectoryTraversalEventArgs
{
    public class DirectoryEventArgs : EventArgs
    {
        public DirectoryEventArgs(DirectoryInfo dirInfo)
        {
            DirectoryInfoArgs = dirInfo;
        }

        public DirectoryInfo DirectoryInfoArgs { get; }
    }
}
