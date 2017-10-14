using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DirectoryTraversal.Tests
{
    [TestFixture]
    public class DirectoryTraversalIteratorTest
    {
        [TestCase("D:/Programs/tabs/13/megadeth", false, false)]
        [TestCase("D:/Programs/tabs/13/megadeth", false, true)]
        [TestCase("D:/Programs/tabs/13/megadeth", true, false)]
        [TestCase("D:/Programs/tabs/13/megadeth", true, true)]
        public void WalkDirectoryTree_WithoutDirectories_Test(string root, bool requiredFileExclude, bool fileExclude)
        {
            DirectoryTraversalIterator directoryTraversal = new DirectoryTraversalIterator(s => s.Contains("d"));
            directoryTraversal.RequiredFileFound += (sender, args) => args.ExcludeFile = requiredFileExclude;
            directoryTraversal.FileFound += (sender, args) => args.ExcludeFile = fileExclude;
            directoryTraversal.DirectoryFound += (sender, args) => args.ExcludeDirectory = true;
            IEnumerable<string> result = directoryTraversal.WalkDirectoryTree(new DirectoryInfo(root));
            if (!fileExclude)
            {
                if (!requiredFileExclude)
                {
                    var unfiltred = result.Any(n => !n.Contains("d"));
                    Assert.AreEqual(unfiltred, false);
                }
                else
                {
                    var unfiltred = result.Any(n => n.Contains("d"));
                    Assert.AreEqual(unfiltred, false);
                }
            }
            else
            {
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestCase("D:/Programs/tabs/13/megadeth", false, false)]
        [TestCase("D:/Programs/tabs/13/megadeth", false, true)]
        [TestCase("D:/Programs/tabs/13/megadeth", true, false)]
        [TestCase("D:/Programs/tabs/13/megadeth", true, true)]
        public void WalkDirectoryTree_WithoutFiles_Test(string root, bool requiredDirectoryExclude, bool directoryExclude)
        {
            DirectoryTraversalIterator directoryTraversal = new DirectoryTraversalIterator(s => s.Contains("g"));
            directoryTraversal.RequiredDirectoryFound += (sender, args) => args.ExcludeDirectory = requiredDirectoryExclude;
            directoryTraversal.DirectoryFound += (sender, args) => args.ExcludeDirectory = directoryExclude;
            directoryTraversal.FileFound += (sender, args) => args.ExcludeFile = true;
            IEnumerable<string> result = directoryTraversal.WalkDirectoryTree(new DirectoryInfo(root));
            if (!directoryExclude)
            {
                if (!requiredDirectoryExclude)
                {
                    var unfiltred = result.Any(n => !n.Contains("g"));
                    Assert.AreEqual(unfiltred, false);
                }
                else
                {
                    var unfiltred = result.Any(n => n.Contains("g"));
                    Assert.AreEqual(unfiltred, false);
                }
            }
            else
            {
                Assert.AreEqual(result.Count(), 0);
            }
        }
    }
}
