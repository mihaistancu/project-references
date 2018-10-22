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

                var references = References(project);

                foreach (var reference in references)
                {
                    node.Nodes.Add(Path.GetFileNameWithoutExtension(reference));
                }
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))   
                e.Effect = DragDropEffects.All; 
        }
        
        private List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e=>e.Attribute("Include").Value).ToList();
        }
    }
}
