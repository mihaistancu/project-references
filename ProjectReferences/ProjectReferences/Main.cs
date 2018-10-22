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
                hierarchicalReferences.Nodes.Add(Dependency.Hierarchy(project));
                flatReferences.Nodes.Add(Dependency.FlatList(project));
                Dependency.FolderStructure(folderStructure.Nodes, project);
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))   
                e.Effect = DragDropEffects.All; 
        }
    }
}
