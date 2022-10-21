namespace Test_ADO_Net_3
{
    partial class Form1
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
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblTableColumns = new System.Windows.Forms.Label();
            this.lblTablePrimaryKey = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(137, 12);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(120, 23);
            this.btnCreateTable.TabIndex = 0;
            this.btnCreateTable.Text = "Create Table";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(12, 46);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(79, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Table Name:";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(12, 78);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(94, 13);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "Table Columns:";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.Location = new System.Drawing.Point(12, 219);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(77, 13);
            this.lbl3.TabIndex = 3;
            this.lbl3.Text = "Primary Key:";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(98, 46);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(72, 13);
            this.lblTableName.TabIndex = 4;
            this.lblTableName.Text = "lblTableName";
            // 
            // lblTableColumns
            // 
            this.lblTableColumns.AutoSize = true;
            this.lblTableColumns.Location = new System.Drawing.Point(12, 102);
            this.lblTableColumns.Name = "lblTableColumns";
            this.lblTableColumns.Size = new System.Drawing.Size(84, 13);
            this.lblTableColumns.TabIndex = 5;
            this.lblTableColumns.Text = "lblTableColumns";
            // 
            // lblTablePrimaryKey
            // 
            this.lblTablePrimaryKey.AutoSize = true;
            this.lblTablePrimaryKey.Location = new System.Drawing.Point(95, 219);
            this.lblTablePrimaryKey.Name = "lblTablePrimaryKey";
            this.lblTablePrimaryKey.Size = new System.Drawing.Size(96, 13);
            this.lblTablePrimaryKey.TabIndex = 6;
            this.lblTablePrimaryKey.Text = "lblTablePrimaryKey";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 262);
            this.Controls.Add(this.lblTablePrimaryKey);
            this.Controls.Add(this.lblTableColumns);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnCreateTable);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label lblTableColumns;
        private System.Windows.Forms.Label lblTablePrimaryKey;
    }
}

