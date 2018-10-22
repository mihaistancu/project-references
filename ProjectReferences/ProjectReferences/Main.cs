using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectReferences
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            
            foreach (var project in files)
            {
                hierarchicalReferences.Nodes.Add(Dependencies.Hierarchy(project));
            }

            foreach (TreeNode node in hierarchicalReferences.Nodes)
            {
                var node2 = flatReferences.Nodes.Add(node.Text);
                var references = new HashSet<string>();
                MakeFlatTree(node, references);

                foreach (var reference in references)
                {
                    node2.Nodes.Add(reference);
                }
            }
        }

        private void MakeFlatTree(TreeNode node, HashSet<string> references)
        {
            foreach (TreeNode r in node.Nodes)
            {
                references.Add(r.Text);
                MakeFlatTree(r, references);
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))   
                e.Effect = DragDropEffects.All; 
        }
    }
}
