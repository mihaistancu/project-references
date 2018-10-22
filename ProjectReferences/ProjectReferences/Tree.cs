using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjectReferences
{
    public class Tree
    {
        public static TreeNode Build(string projectPath)
        {
            var node = NewNode(projectPath);
            
            foreach (var reference in References(projectPath))
            {
                var referenceNode = Build(reference);
                node.Nodes.Add(referenceNode);
            }

            return node;
        }

        private static TreeNode NewNode(string projectPath)
        {
            return new TreeNode
            {
                Text = Path.GetFileNameWithoutExtension(projectPath),
                ToolTipText = projectPath
            };
        }

        private static string FullPath(string parentPath, string relativePath)
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(parentPath), relativePath));
        }

        private static List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e => FullPath(projectPath, e.Attribute("Include").Value)).ToList();
        }
    }
}
