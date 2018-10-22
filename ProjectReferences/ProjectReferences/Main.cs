using System.Windows.Forms;

namespace ProjectReferences
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            hierarchicalReferences.Nodes.Clear();
            flatReferences.Nodes.Clear();
            folderStructure.Nodes.Clear();
        }

        private void Display(string[] projects)
        {
            foreach (var project in projects)
            {
                hierarchicalReferences.Nodes.Add(Dependency.Hierarchy(project));
                flatReferences.Nodes.Add(Dependency.FlatList(project));
                Dependency.FolderStructure(folderStructure.Nodes, project);
            }
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            var projects = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            Clear();
            Display(projects);
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }
    }
}
