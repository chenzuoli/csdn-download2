using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csdn_download
{
    public partial class CnblogForm: Form
    {
        public CnblogForm()
        {
            InitializeComponent();
        }

        private void CnblogForm_Load(object sender, EventArgs e)
        {
            
        }

        private void select_button_Click(object sender, EventArgs e)
        {
            string cookie = cnblog_cookie_input.Text;
            if (string.IsNullOrEmpty(cookie))
            {
                MessageBox.Show("请输入cnblog博客园登录cookie，软件不会记录任何个人cookie，仅导入博客使用，请放心填写。");
                return;
            }

            // 批量选择文件导入
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            openFileDialog.Filter = "markdown文件（*.md）|*.md"; // 只能选择markdown文件

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = openFileDialog.FileNames;
                fillGridData(filePaths);
            }
            else
            {
                MessageBox.Show("请选择markdown博客文件导入。");
            }
        }

        private void fillGridData(string[] filePaths)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("文件", typeof(string));
            foreach (string s in filePaths) {
                dt.Rows.Add(s);
            }
            // 将DataTable数据绑定到DataGridView
            selectFilesGridView1.RowHeadersVisible = true;
            selectFilesGridView1.DataSource = dt;
            // 让datatable自动填满整个DataGridView
            selectFilesGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 在每行的第一列添加复选框
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = "";
            checkboxColumn.Width = 10;
            checkboxColumn.Name = "checkBoxColumn";
            selectFilesGridView1.Columns.Insert(0, checkboxColumn);

            // fill情况下，指定某列宽度
            selectFilesGridView1.Columns[0].FillWeight = 10;

        }

        private void selectFilesGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            // 遍历datagridview每一行，判断是否已经选中，若为选中，则选中
            for (int i = 0; i < selectFilesGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selectFilesGridView1.Rows[i].Cells[0].Value) == false)
                {
                    selectFilesGridView1.Rows[i].Cells[0].Value = true;
                }
                else
                    continue;
            }
        }

        private void cancelSelectAll_Click(object sender, EventArgs e)
        {
            // 取消全选
            for (int i = 0; i < selectFilesGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selectFilesGridView1.Rows[i].Cells[0].Value) == true)
                {
                    selectFilesGridView1.Rows[i].Cells[0].Value = false;
                }
                else continue;
            }
        }

        private void reverseSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < selectFilesGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selectFilesGridView1.Rows[i].Cells[0].Value) == false)
                {
                    selectFilesGridView1.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    selectFilesGridView1.Rows[i].Cells[0].Value = false;
                }
            }
        }

        private void import_btn_click(object sender, EventArgs e)
        {

            // 获取选择的博客链接
            var ifSeleted = false;
            var selectedUrls = new List<string>();
            for (int i = 0; i < selectFilesGridView1.Rows.Count; i++)
            {
                Console.WriteLine(selectFilesGridView1.Rows[i].ToString());
                if (Convert.ToBoolean(selectFilesGridView1.Rows[i].Cells[0].Value) == true)
                {
                    ifSeleted = true;
                    var url = selectFilesGridView1.Rows[i].Cells[2].Value;
                    if (url != null)
                    {
                        selectedUrls.Add(url.ToString());
                    }
                };
            }
            if (!ifSeleted)
            {
                MessageBox.Show("未选择任务博客文章，请选择后再导入。");
                return;
            }

            // 导入，请求博客园导入接口


        }
    }
}
