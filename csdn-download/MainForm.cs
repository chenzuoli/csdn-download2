using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using WinFormWaitDialog;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using HtmlAgilityPack;
using ReverseMarkdown;
using OpenQA.Selenium.Chrome;
using csdn_download.util;

namespace csdn_download
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void loadingLabel(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("加载中...");
        }


        private void csdn_click(object sender, EventArgs e)
        {
            CsdnForm form = new CsdnForm();
            // 不展示form窗体标题栏、最大化、最小化、隐藏按钮
            form.ControlBox = false;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.None;
            openPage(form);
        }

        public void openPage(Form form)
        {
            var winutil = new WindowUtil();
            winutil.closeOtherForms();
            
            form.Dock = DockStyle.Fill; // 设置dock为fill，使子窗口占满splitContainer1.Panel2
            form.TopLevel = false; // 设置为非顶级控件，否则无法添加
            form.Show();
            splitContainer1.Panel2.Controls.Clear(); // 清除panel2内容
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Panel2.Controls.Add(form); // 展示新form内容
            Console.WriteLine("展示Cnblog Form");
        }

        private void cnblog_click(object sender, EventArgs e)
        {
            CnblogForm form = new CnblogForm();
            // 不展示form窗体标题栏、最大化、最小化、隐藏按钮
            form.ControlBox = false; 
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.None;
            openPage(form);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CsdnForm form = new CsdnForm();
            // 不展示form窗体标题栏、最大化、最小化、隐藏按钮
            form.ControlBox = false;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.None;
            openPage(form);
        }
    }
}
