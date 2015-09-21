namespace V5.Automate.UI
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using V5.Library.Storage;

    public partial class OutputForm : Form
    {
        private Schematic schematic;

        public string OutputDirectory { get; set; }

        public string CurrentDb { get; set; }

        public string CurrentTable { get; set; }

        public OutputForm()
        {
            this.schematic = new Schematic();
            InitializeComponent();
        }

        private void btnBroswer_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            this.txtOutputDirectory.Text = folderBrowserDialog.SelectedPath;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOutputDirectory.Text))
            {
                MessageBox.Show("输出目录不能为空");
                return;
            }

            if (string.IsNullOrEmpty(this.txtNamespace.Text))
            {
                MessageBox.Show("命名空间不能为空");
                return;
            }

            var dbTable = schematic.GetDbTable(this.CurrentDb, this.CurrentTable);
            if (dbTable != null)
            {
                var fileStore = new FileStore();
                GenerateEntity(dbTable, fileStore);

                MessageBox.Show("生成成功");
            }
            else
            {
                MessageBox.Show("生成失败");
            }
        }

        private void btnGenerateDb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOutputDirectory.Text))
            {
                MessageBox.Show("输出目录不能为空");
                return;
            }

            if (string.IsNullOrEmpty(this.txtNamespace.Text))
            {
                MessageBox.Show("命名空间不能为空");
                return;
            }

            var fileStore = new FileStore();
            var dbTables = schematic.GetDbTables(this.CurrentDb);

            try
            {
                foreach (var dbTable in dbTables)
                {
                    GenerateEntity(dbTable, fileStore);
                }

                MessageBox.Show("生成成功");
            }
            catch (Exception)
            {
                MessageBox.Show("生成失败");
            }
        }

        private void GenerateEntity(DbTable dbTable, FileStore fileStore)
        {
            var template = new Template(this.txtNamespace.Text, dbTable.Name);
            
            dbTable.Summary = dbTable.Summary.EndsWith("类别表") ? dbTable.Summary.Replace("类别表", "类别类") : dbTable.Summary.Replace("表", "类");

            var entityFileText = template.GetEntityContent(dbTable);

            fileStore.CreateFile(this.txtOutputDirectory.Text, dbTable.Name + ".cs", entityFileText, Encoding.UTF8);
        }
    }
}
