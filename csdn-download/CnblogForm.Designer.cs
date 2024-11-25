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
            this.cnblog_token = new System.Windows.Forms.TextBox();
            this.xsrf_token = new System.Windows.Forms.Label();
            this.cnblog_category = new System.Windows.Forms.TextBox();
            this.category = new System.Windows.Forms.Label();
            this.cnblog_tag = new System.Windows.Forms.TextBox();
            this.tag = new System.Windows.Forms.Label();
            this.cnblog_desc = new System.Windows.Forms.TextBox();
            this.desc = new System.Windows.Forms.Label();
            this.is_draft = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.selectFilesGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cnblog_cookie_input
            // 
            this.cnblog_cookie_input.Location = new System.Drawing.Point(806, 67);
            this.cnblog_cookie_input.Name = "cnblog_cookie_input";
            this.cnblog_cookie_input.Size = new System.Drawing.Size(298, 23);
            this.cnblog_cookie_input.TabIndex = 0;
            // 
            // cnblog_cookie
            // 
            this.cnblog_cookie.AutoSize = true;
            this.cnblog_cookie.Location = new System.Drawing.Point(729, 70);
            this.cnblog_cookie.Name = "cnblog_cookie";
            this.cnblog_cookie.Size = new System.Drawing.Size(71, 15);
            this.cnblog_cookie.TabIndex = 1;
            this.cnblog_cookie.Text = "* cookie";
            // 
            // import_button
            // 
            this.import_button.Location = new System.Drawing.Point(26, 18);
            this.import_button.Name = "import_button";
            this.import_button.Size = new System.Drawing.Size(92, 23);
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
            this.selectFilesGridView1.Size = new System.Drawing.Size(684, 483);
            this.selectFilesGridView1.TabIndex = 22;
            this.selectFilesGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectFilesGridView1_CellContentClick);
            // 
            // cnblogImportBtn
            // 
            this.cnblogImportBtn.Location = new System.Drawing.Point(806, 387);
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
            // cnblog_token
            // 
            this.cnblog_token.Location = new System.Drawing.Point(806, 108);
            this.cnblog_token.Name = "cnblog_token";
            this.cnblog_token.Size = new System.Drawing.Size(298, 23);
            this.cnblog_token.TabIndex = 28;
            // 
            // xsrf_token
            // 
            this.xsrf_token.AutoSize = true;
            this.xsrf_token.Location = new System.Drawing.Point(733, 111);
            this.xsrf_token.Name = "xsrf_token";
            this.xsrf_token.Size = new System.Drawing.Size(63, 15);
            this.xsrf_token.TabIndex = 29;
            this.xsrf_token.Text = "* token";
            // 
            // cnblog_category
            // 
            this.cnblog_category.Location = new System.Drawing.Point(806, 149);
            this.cnblog_category.Name = "cnblog_category";
            this.cnblog_category.Size = new System.Drawing.Size(298, 23);
            this.cnblog_category.TabIndex = 30;
            // 
            // category
            // 
            this.category.AutoSize = true;
            this.category.Location = new System.Drawing.Point(759, 152);
            this.category.Name = "category";
            this.category.Size = new System.Drawing.Size(37, 15);
            this.category.TabIndex = 31;
            this.category.Text = "分类";
            // 
            // cnblog_tag
            // 
            this.cnblog_tag.Location = new System.Drawing.Point(806, 189);
            this.cnblog_tag.Name = "cnblog_tag";
            this.cnblog_tag.Size = new System.Drawing.Size(298, 23);
            this.cnblog_tag.TabIndex = 32;
            // 
            // tag
            // 
            this.tag.AutoSize = true;
            this.tag.Location = new System.Drawing.Point(759, 192);
            this.tag.Name = "tag";
            this.tag.Size = new System.Drawing.Size(37, 15);
            this.tag.TabIndex = 33;
            this.tag.Text = "标签";
            // 
            // cnblog_desc
            // 
            this.cnblog_desc.Location = new System.Drawing.Point(806, 232);
            this.cnblog_desc.Name = "cnblog_desc";
            this.cnblog_desc.Size = new System.Drawing.Size(298, 23);
            this.cnblog_desc.TabIndex = 34;
            // 
            // desc
            // 
            this.desc.AutoSize = true;
            this.desc.Location = new System.Drawing.Point(759, 235);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(37, 15);
            this.desc.TabIndex = 35;
            this.desc.Text = "描述";
            // 
            // is_draft
            // 
            this.is_draft.AutoSize = true;
            this.is_draft.Location = new System.Drawing.Point(729, 274);
            this.is_draft.Name = "is_draft";
            this.is_draft.Size = new System.Drawing.Size(67, 15);
            this.is_draft.TabIndex = 37;
            this.is_draft.Text = "是否草稿";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(28, 22);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(40, 19);
            this.radioButton2.TabIndex = 38;
            this.radioButton2.Text = "是";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(28, 48);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(40, 19);
            this.radioButton1.TabIndex = 39;
            this.radioButton1.Text = "否";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(806, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 85);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // CnblogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 612);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.is_draft);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.cnblog_desc);
            this.Controls.Add(this.tag);
            this.Controls.Add(this.cnblog_tag);
            this.Controls.Add(this.category);
            this.Controls.Add(this.cnblog_category);
            this.Controls.Add(this.xsrf_token);
            this.Controls.Add(this.cnblog_token);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox cnblog_token;
        private System.Windows.Forms.Label xsrf_token;
        private System.Windows.Forms.TextBox cnblog_category;
        private System.Windows.Forms.Label category;
        private System.Windows.Forms.TextBox cnblog_tag;
        private System.Windows.Forms.Label tag;
        private System.Windows.Forms.TextBox cnblog_desc;
        private System.Windows.Forms.Label desc;
        private System.Windows.Forms.Label is_draft;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}