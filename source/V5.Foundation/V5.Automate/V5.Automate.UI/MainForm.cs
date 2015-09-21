namespace V5.Automate.UI
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private Schematic schematic;

        public MainForm()
        {
            this.schematic = new Schematic();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.cboDataBase.Items.Clear();
            this.cboDataBase.Items.AddRange(this.schematic.GetDbNames().ToArray());
            this.cboDataBase.SelectedIndex = 0;
        }

        private void cboDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgvDbColumns.DataSource = null;

                this.lbDbTables.DisplayMember = "Name";
                this.lbDbTables.Items.Clear();
                this.lbDbTables.Items.AddRange(this.schematic.GetDbTables(this.cboDataBase.SelectedItem.ToString()).ToArray());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误提示");
            }
        }

        private void lbDbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var dbTable = (DbTable) this.lbDbTables.SelectedItem;
                this.Text = dbTable.Summary;

                this.dgvDbColumns.DataSource = this.schematic.GetDbColumns(this.cboDataBase.SelectedItem.ToString(), ((DbTable) this.lbDbTables.SelectedItem).Name);

                this.dgvDbColumns.Columns[0].HeaderCell.Value = "编号";
                this.dgvDbColumns.Columns[0].Width = 70;
                this.dgvDbColumns.Columns[1].HeaderCell.Value = "列名称";
                this.dgvDbColumns.Columns[1].Width = 100;
                this.dgvDbColumns.Columns[2].HeaderCell.Value = "列类型";
                this.dgvDbColumns.Columns[2].Width = 100;
                this.dgvDbColumns.Columns[3].HeaderCell.Value = "数据类型";
                this.dgvDbColumns.Columns[3].Width = 130;
                this.dgvDbColumns.Columns[4].HeaderCell.Value = "概要";
                this.dgvDbColumns.Columns[4].Width = 120;

                this.btnGenerateEntity.Enabled = this.lbDbTables.SelectedItem != null;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误提示");
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var dbNames = this.schematic.GetDbNames();
                if (dbNames != null && dbNames.Count > 0)
                {
                    MessageBox.Show("Test Success");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Test Failed");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cboDataBase.Items.Clear();
            this.cboDataBase.Items.AddRange(this.schematic.GetDbNames().ToArray());
            this.cboDataBase.SelectedIndex = 0;
        }

        private void btnGenerateEntity_Click(object sender, EventArgs e)
        {
            var outputForm = new OutputForm
            {
                CurrentDb = this.cboDataBase.SelectedItem.ToString(),
                CurrentTable = ((DbTable)this.lbDbTables.SelectedItem).Name
            };

            outputForm.ShowDialog();
        }
    }
}
