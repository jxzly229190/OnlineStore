namespace V5.Automate.UI
{
    partial class OutputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.btnBroswer = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnGenerateDb = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutputDirectory.Location = new System.Drawing.Point(108, 6);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(313, 26);
            this.txtOutputDirectory.TabIndex = 0;
            // 
            // btnBroswer
            // 
            this.btnBroswer.Location = new System.Drawing.Point(427, 5);
            this.btnBroswer.Name = "btnBroswer";
            this.btnBroswer.Size = new System.Drawing.Size(103, 29);
            this.btnBroswer.TabIndex = 4;
            this.btnBroswer.Text = "Broswer...";
            this.btnBroswer.UseVisualStyleBackColor = false;
            this.btnBroswer.Click += new System.EventHandler(this.btnBroswer_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(427, 38);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(103, 29);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnGenerateDb
            // 
            this.btnGenerateDb.Location = new System.Drawing.Point(294, 38);
            this.btnGenerateDb.Name = "btnGenerateDb";
            this.btnGenerateDb.Size = new System.Drawing.Size(127, 29);
            this.btnGenerateDb.TabIndex = 6;
            this.btnGenerateDb.Text = "Generate Db";
            this.btnGenerateDb.UseVisualStyleBackColor = false;
            this.btnGenerateDb.Click += new System.EventHandler(this.btnGenerateDb_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNamespace.Location = new System.Drawing.Point(108, 40);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(180, 26);
            this.txtNamespace.TabIndex = 7;
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Location = new System.Drawing.Point(4, 42);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(101, 20);
            this.lblNamespace.TabIndex = 8;
            this.lblNamespace.Text = "Namespace：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Directory：";
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 72);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNamespace);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.btnGenerateDb);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnBroswer);
            this.Controls.Add(this.txtOutputDirectory);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Output";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button btnBroswer;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnGenerateDb;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Label label1;
    }
}