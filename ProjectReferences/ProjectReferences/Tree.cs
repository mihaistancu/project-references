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
            
            foreach (var reference in Project.DirectReferences(projectPath))
            {
                var referenceNode = Hierarchy(reference);
                node.Nodes.Add(referenceNode);
            }

            return node;
        }
        
        public static TreeNode FlatList(string projectPath)
        {
            var node = new TreeNode
            {
                Text = Path.GetFileNameWithoutExtension(projectPath),
                ToolTipText = projectPath
            };

            var dependencies = Project.AllReferences(projectPath);
            
            foreach (var reference in dependencies)
            {
                var referenceNode = new TreeNode(Path.GetFileNameWithoutExtension(reference));
                referenceNode.ToolTipText = reference;
                node.Nodes.Add(referenceNode);
            }

            return node;
        }
    }
}
