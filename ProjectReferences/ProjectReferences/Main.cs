using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

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
                var node = treeView1.Nodes.Add(Path.GetFileNameWithoutExtension(project));
                node.ToolTipText = project;
                BuildTree(node);
            }

            foreach (TreeNode node in treeView1.Nodes)
            {
                var node2 = treeView2.Nodes.Add(node.Text);
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

        private void BuildTree(TreeNode node)
        {
            var references = References(node.ToolTipText);

            foreach (var reference in references)
            {
                var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(node.ToolTipText), reference));
                var child = node.Nodes.Add(Path.GetFileNameWithoutExtension(path));
                child.ToolTipText = path;
                BuildTree(child);
            }
        }

        private List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e=>e.Attribute("Include").Value).ToList();
        }
    }
}
