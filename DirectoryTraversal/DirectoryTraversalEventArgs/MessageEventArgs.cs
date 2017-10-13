using System;

namespace DirectoryTraversal.DirectoryTraversalEventArgs
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            Message = message ?? throw new ArgumentNullException($"{nameof(message)} is required");
        }
        public string Message { get; }

    }
}
