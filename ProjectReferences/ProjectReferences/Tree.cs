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
            var node = new TreeNode
            {
                Text = Path.GetFileNameWithoutExtension(projectPath),
                ToolTipText = projectPath
            };

            var references = References(projectPath);

            foreach (var reference in references)
            {   
                var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(node.ToolTipText), reference));
                
                var referenceNode = Build(path);
                node.Nodes.Add(referenceNode);
            }

            return node;
        }

        private static List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e=>e.Attribute("Include").Value).ToList();
        }
    }
}
