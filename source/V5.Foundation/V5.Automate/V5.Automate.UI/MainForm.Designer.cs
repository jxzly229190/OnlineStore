namespace V5.Automate.UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cboDataBase = new System.Windows.Forms.ComboBox();
            this.lbDbTables = new System.Windows.Forms.ListBox();
            this.dgvDbColumns = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnGenerateEntity = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDbColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDataBase
            // 
            this.cboDataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboDataBase.FormattingEnabled = true;
            this.cboDataBase.Location = new System.Drawing.Point(217, 7);
            this.cboDataBase.Name = "cboDataBase";
            this.cboDataBase.Size = new System.Drawing.Size(511, 28);
            this.cboDataBase.TabIndex = 0;
            this.cboDataBase.SelectedIndexChanged += new System.EventHandler(this.cboDataBase_SelectedIndexChanged);
            // 
            // lbDbTables
            // 
            this.lbDbTables.FormattingEnabled = true;
            this.lbDbTables.HorizontalScrollbar = true;
            this.lbDbTables.ItemHeight = 20;
            this.lbDbTables.Location = new System.Drawing.Point(7, 41);
            this.lbDbTables.Name = "lbDbTables";
            this.lbDbTables.Size = new System.Drawing.Size(204, 444);
            this.lbDbTables.TabIndex = 1;
            this.lbDbTables.SelectedIndexChanged += new System.EventHandler(this.lbDbTables_SelectedIndexChanged);
            // 
            // dgvDbColumns
            // 
            this.dgvDbColumns.AllowUserToResizeRows = false;
            this.dgvDbColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDbColumns.Location = new System.Drawing.Point(217, 41);
            this.dgvDbColumns.Name = "dgvDbColumns";
            this.dgvDbColumns.ReadOnly = true;
            this.dgvDbColumns.RowHeadersVisible = false;
            this.dgvDbColumns.RowTemplate.Height = 23;
            this.dgvDbColumns.Size = new System.Drawing.Size(511, 410);
            this.dgvDbColumns.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(143, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(68, 29);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(7, 6);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(130, 29);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnGenerateEntity
            // 
            this.btnGenerateEntity.Enabled = false;
            this.btnGenerateEntity.Location = new System.Drawing.Point(591, 457);
            this.btnGenerateEntity.Name = "btnGenerateEntity";
            this.btnGenerateEntity.Size = new System.Drawing.Size(138, 29);
            this.btnGenerateEntity.TabIndex = 5;
            this.btnGenerateEntity.Text = "Generate Entity";
            this.btnGenerateEntity.UseVisualStyleBackColor = false;
            this.btnGenerateEntity.Click += new System.EventHandler(this.btnGenerateEntity_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 492);
            this.Controls.Add(this.btnGenerateEntity);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvDbColumns);
            this.Controls.Add(this.lbDbTables);
            this.Controls.Add(this.cboDataBase);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "V5 Automate";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDbColumns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDataBase;
        private System.Windows.Forms.ListBox lbDbTables;
        private System.Windows.Forms.DataGridView dgvDbColumns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnGenerateEntity;
    }
}

