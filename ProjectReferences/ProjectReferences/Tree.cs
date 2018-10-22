using System.IO;
using System.Windows.Forms;

namespace ProjectReferences
{
    public class Tree
    {
        public static void AddPath(TreeNodeCollection nodes, string path)
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
    }
}
