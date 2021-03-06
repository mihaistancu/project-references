﻿using System.IO;
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
            foreach (var reference in Project.AllReferences(projectPath))
            {
                Tree.AddMultiple(nodes, reference.Split(Path.DirectorySeparatorChar));
            }
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
