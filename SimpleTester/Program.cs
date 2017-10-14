using System;
using System.Collections.Generic;
using System.IO;
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

            DirectoryInfo rootDir = di.RootDirectory;
            DirectoryInfo d = new DirectoryInfo("D:/Programs/tabs/13/megadeth");
            DirectoryTraversalIterator dt = new DirectoryTraversalIterator(s => s.Contains(""));
            dt.FileFound += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.FileInfoArgs.FullName);
            };
            dt.RequiredFileFound += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.FileInfoArgs.FullName);
            };
            dt.RequiredDirectoryFound += (sender, eventArgs) => Console.WriteLine(eventArgs.DirectoryInfoArgs.FullName);
            dt.WalkDirectoryTree(rootDir).ToList();
            Console.Read();
        }
    }
}
