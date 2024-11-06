namespace csdn_download
{
    partial class CnblogForm
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
            this.cnblog_cookie_input = new System.Windows.Forms.TextBox();
            this.cnblog_cookie = new System.Windows.Forms.Label();
            this.import_button = new System.Windows.Forms.Button();
            this.selectFilesGridView1 = new System.Windows.Forms.DataGridView();
            this.cnblogImportBtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.selectFilesGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cnblog_cookie_input
            // 
            this.cnblog_cookie_input.Location = new System.Drawing.Point(140, 21);
            this.cnblog_cookie_input.Name = "cnblog_cookie_input";
            this.cnblog_cookie_input.Size = new System.Drawing.Size(431, 23);
            this.cnblog_cookie_input.TabIndex = 0;
            // 
            // cnblog_cookie
            // 
            this.cnblog_cookie.AutoSize = true;
            this.cnblog_cookie.Location = new System.Drawing.Point(23, 24);
            this.cnblog_cookie.Name = "cnblog_cookie";
            this.cnblog_cookie.Size = new System.Drawing.Size(111, 15);
            this.cnblog_cookie.TabIndex = 1;
            this.cnblog_cookie.Text = "cnblog cookie";
            // 
            // import_button
            // 
            this.import_button.Location = new System.Drawing.Point(600, 21);
            this.import_button.Name = "import_button";
            this.import_button.Size = new System.Drawing.Size(75, 23);
            this.import_button.TabIndex = 2;
            this.import_button.Text = "选择文件";
            this.import_button.UseVisualStyleBackColor = true;
            this.import_button.Click += new System.EventHandler(this.select_button_Click);
            // 
            // selectFilesGridView1
            // 
            this.selectFilesGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectFilesGridView1.Location = new System.Drawing.Point(23, 59);
            this.selectFilesGridView1.Name = "selectFilesGridView1";
            this.selectFilesGridView1.RowHeadersWidth = 49;
            this.selectFilesGridView1.RowTemplate.Height = 25;
            this.selectFilesGridView1.Size = new System.Drawing.Size(999, 483);
            this.selectFilesGridView1.TabIndex = 22;
            this.selectFilesGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectFilesGridView1_CellContentClick);
            // 
            // cnblogImportBtn
            // 
            this.cnblogImportBtn.Location = new System.Drawing.Point(704, 20);
            this.cnblogImportBtn.Name = "cnblogImportBtn";
            this.cnblogImportBtn.Size = new System.Drawing.Size(75, 23);
            this.cnblogImportBtn.TabIndex = 23;
            this.cnblogImportBtn.Text = "导入博客园";
            this.cnblogImportBtn.UseVisualStyleBackColor = true;
            this.cnblogImportBtn.Click += new System.EventHandler(this.import_btn_click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(221, 562);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "取消选择";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 562);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "全选";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(121, 562);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "反选";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.reverseSelect_Click);
            // 
            // CnblogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 612);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cnblogImportBtn);
            this.Controls.Add(this.selectFilesGridView1);
            this.Controls.Add(this.import_button);
            this.Controls.Add(this.cnblog_cookie);
            this.Controls.Add(this.cnblog_cookie_input);
            this.Name = "CnblogForm";
            this.Text = "CnblogForm";
            this.Load += new System.EventHandler(this.CnblogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.selectFilesGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cnblog_cookie_input;
        private System.Windows.Forms.Label cnblog_cookie;
        private System.Windows.Forms.Button import_button;
        private System.Windows.Forms.DataGridView selectFilesGridView1;
        private System.Windows.Forms.Button cnblogImportBtn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}