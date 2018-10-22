using System.IO;
using System.Windows.Forms;

namespace ProjectReferences
{
    public class Dependencies
    {
        public static TreeNode Hierarchy(string projectPath)
        {
            var node = new TreeNode
            {
                Text = Path.GetFileNameWithoutExtension(projectPath),
                ToolTipText = projectPath
            };
            
            foreach (var reference in Project.References(projectPath))
            {
                var referenceNode = Hierarchy(reference);
                node.Nodes.Add(referenceNode);
            }

            return node;
        }
    }
}
