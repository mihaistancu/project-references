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
                hierarchicalReferences.Nodes.Add(BuildTree(project));
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

        private TreeNode BuildTree(string projectPath)
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
                
                var referenceNode = BuildTree(path);
                node.Nodes.Add(referenceNode);
            }

            return node;
        }

        private List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e=>e.Attribute("Include").Value).ToList();
        }
    }
}
