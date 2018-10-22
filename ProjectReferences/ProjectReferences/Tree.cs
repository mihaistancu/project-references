using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectReferences
{
    public class Tree
    {
        public static void AddMultiple(TreeNodeCollection nodes, IEnumerable<string> successors)
        {
            foreach (var item in successors)
            {
                nodes = AddSingle(nodes, item).Nodes;
            }
        }

        private static TreeNode AddSingle(TreeNodeCollection nodes, string item)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == item) return node;
            }

            return nodes.Add(item);
        }
    }
}
