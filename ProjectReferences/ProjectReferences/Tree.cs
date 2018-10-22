using System.IO;
using System.Windows.Forms;

namespace ProjectReferences
{
    public class Tree
    {
        public static TreeNode Build(string projectPath)
        {
            var node = NewNode(projectPath);
            
            foreach (var reference in Project.References(projectPath))
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
    }
}
