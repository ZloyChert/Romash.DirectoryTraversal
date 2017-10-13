using System;
using System.IO;

namespace DirectoryTraversal.DirectoryTraversalEventArgs
{
    public class DirectoryEventArgs : EventArgs
    {
        public DirectoryEventArgs(DirectoryInfo dirInfo)
        {
            DirectoryInfoArgs = dirInfo ?? throw new ArgumentNullException($"{nameof(dirInfo)} is required");
        }

        public DirectoryInfo DirectoryInfoArgs { get; }

        public bool StopTraversal { get; set; }
        public bool ExcludeFile { get; set; }
    }
}
