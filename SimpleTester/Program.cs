using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryTraversal;

namespace SimpleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] drives = Environment.GetLogicalDrives();

            System.IO.DriveInfo di = new System.IO.DriveInfo(drives[1]);

            System.IO.DirectoryInfo rootDir = di.RootDirectory;
            
            DirectoryTraversalIterator dt = new DirectoryTraversalIterator(s => true);
            dt.RequiredFileFound += (sender, eventArgs) => Console.WriteLine(eventArgs.FileInfoArgs.FullName);
            dt.RequiredDirectoryFound += (sender, eventArgs) => Console.WriteLine(eventArgs.DirectoryInfoArgs.FullName);
            dt.WalkDirectoryTree(rootDir).ToList();
            Console.Read();
        }
    }
}
