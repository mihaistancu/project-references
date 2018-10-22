using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProjectReferences
{
    public class Dependency
    {
        public static TreeNode Hierarchy(string projectPath)
        {
            var node = Node(projectPath);
            
            foreach (var reference in Project.DirectReferences(projectPath))
            {
                var referenceNode = Hierarchy(reference);
                node.Nodes.Add(referenceNode);
            }

            return node;
        }
        
        public static TreeNode FlatList(string projectPath)
        {
            var node = Node(projectPath);
            var references = Project.AllReferences(projectPath).Select(Node).ToArray();
            node.Nodes.AddRange(references);
            return node;
        }

        public static void FolderStructure(TreeNodeCollection nodes, string projectPath)
        {
            var references = Project.AllReferences(projectPath);

            foreach (var reference in references)
            {
                AddPath(nodes, reference);
            }
        }

        private static void AddPath(TreeNodeCollection nodes, string path)
        {
            var folders = path.Split(Path.DirectorySeparatorChar);
            
            foreach (var folder in folders)
            {
                nodes = Add(nodes, folder).Nodes;
            }
        }

        private static TreeNode Add(TreeNodeCollection nodes, string folder)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == folder) return node;
            }

            return nodes.Add(folder);
        }

        private static TreeNode Node(string projectPath)
        {
            return new TreeNode
            {
                Text = Path.GetFileNameWithoutExtension(projectPath),
                ToolTipText = projectPath
            };
        }
    }
}
