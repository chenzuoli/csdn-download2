using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using csdn_download.util;
using Newtonsoft.Json;
using WinFormWaitDialog;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Contexts;
using System.Collections;

namespace csdn_download
{
    public partial class CnblogForm : Form
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
            foreach (string s in filePaths)
            {
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
            var selectedPaths = new List<string>();
            for (int i = 0; i < selectFilesGridView1.Rows.Count; i++)
            {
                Console.WriteLine(selectFilesGridView1.Rows[i].ToString());
                if (Convert.ToBoolean(selectFilesGridView1.Rows[i].Cells[0].Value) == true)
                {
                    ifSeleted = true;
                    var url = selectFilesGridView1.Rows[i].Cells[1].Value;
                    if (url != null)
                    {
                        selectedPaths.Add(url.ToString());
                    }
                };
            }
            if (!ifSeleted)
            {
                MessageBox.Show("未选择任务博客文章，请选择后再导入。");
                return;
            }
            // 发布
            // 加载中
            var waitDialog = new WaitDialog((IProgress<string> progress) =>
            {
                LongRunningProcess(progress);
            });
            waitDialog.Show();

            postCnblogs(selectedPaths, waitDialog);
        }

        private async void LongRunningProcess(IProgress<string> progress)
        {
            for (int i = 0; i < 10000; i++)
            {
                progress.Report($"cnblogs uploading...");
                Thread.Sleep(10000);
            }
        }

        private async void post(List<string> filePaths)
        {
            // 导入，请求博客园导入接口
            string cnblog_url = "https://i.cnblogs.com/api/posts";
            string referer = "https://i.cnblogs.com/posts/edit";
            string cookie = cnblog_cookie_input.Text;
            string token = cnblog_token.Text;
            string category = cnblog_category.Text;
            string tag = cnblog_tag.Text;
            string desc = cnblog_desc.Text;
            bool isDraft = true;
            Console.WriteLine("cookie: " + cookie);
            Console.WriteLine("token: " + token);
            if (radioButton1.Checked)
            {
                isDraft = false;
            }

            IDictionary<string, object> headers = new Dictionary<string, object>();
            headers.Add("Cookie", cookie); // 替换为你的Cookie
            headers.Add("Content-Type", "application/json");
            headers.Add("Referer", referer);
            headers.Add("Sec-Ch-Ua-Mobile", "?0");
            headers.Add("Sec-Ch-Ua-Platform", "Windows");
            headers.Add("X-Xsrf-Token", token);
            headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
            Console.WriteLine($"headers: {headers}");

            foreach (string path in filePaths)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string title = Path.GetFileName(path);
                    string content = sr.ReadToEnd();
                    var postBody = new Dictionary<string, object>();

                    IDictionary<string, object> paramBody = new Dictionary<string, object>();
                    paramBody.Add("postType", 1);
                    paramBody.Add("accessPermission", 0);
                    paramBody.Add("title", title);
                    paramBody.Add("postBody", content);
                    paramBody.Add("categories", new List<string>() { category });
                    paramBody.Add("inSiteCandidate", false);
                    paramBody.Add("inSiteHome", false);
                    paramBody.Add("isPublished", false);
                    paramBody.Add("isAllowComments", true);
                    paramBody.Add("description", desc);
                    paramBody.Add("tags", new List<string>() { tag });
                    paramBody.Add("isMarkdown", true);
                    paramBody.Add("isDraft", isDraft);
                    Console.WriteLine($"params: {paramBody}");

                    string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";

                    CookieCollection cookieCollection = new CookieCollection();
                    var response = HttpHelper.CreatePostHttpResponse(cnblog_url, paramBody, headers, 30, userAgent, cookieCollection);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("请求成功");
                        Console.WriteLine(response.ToString());
                    }

                    string json = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        postType = 1,
                        accessPermission = 0,
                        title = title,
                        postBody = content,
                        categories = new List<string>() { category },
                        inSiteCandidate = false,
                        inSiteHome = false,
                        isPublished = false,
                        isAllowComments = true,
                        description = desc,
                        tags = new List<string>() { tag },
                        isMarkdown = true,
                        isDraft = isDraft
                        // 添加你的JSON属性
                    });



                    var _handler = new HttpClientHandler()
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };
                    var baseAddress = new Uri(cnblog_url);
                    using (HttpClient client = new HttpClient(_handler) { BaseAddress = baseAddress })
                    {
                        // 添加Cookie到header
                        //client.DefaultRequestHeaders.Add("Cookie", cookie); // 替换为你的Cookie
                        //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                        //client.DefaultRequestHeaders.Add("Referer", referer);
                        //client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                        //client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "Windows");
                        //client.DefaultRequestHeaders.Add("X-Xsrf-Token", token);
                        //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
                        // 设置Content-Type为application/json
                        //using (HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        //{
                        //    try
                        //    {
                        //        HttpResponseMessage response = await client.PostAsync(cnblog_url, httpContent);
                        //        response.EnsureSuccessStatusCode(); // 抛出异常如果响应状态码不是成功的状态码
                        //        string responseBody = await response.Content.ReadAsStringAsync();
                        //        MessageBox.Show($"Response: {responseBody}");
                        //    }
                        //    catch (HttpRequestException ex)
                        //    {
                        //        MessageBox.Show($"Request error: {ex.Message}");
                        //    }
                        //}

                        //_handler.CookieContainer.Add(baseAddress, new Cookie("AuthCookie", cookie));
                        //var requestMsg = new HttpRequestMessage(HttpMethod.Post, cnblog_url);
                        //requestMsg.Headers.Add("Accept", "application/json");
                        //requestMsg.Headers.Add("Cookie", cookie); // 替换为你的Cookie
                        //requestMsg.Headers.Add("Referer", referer);
                        //requestMsg.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
                        //requestMsg.Headers.Add("Sec-Ch-Ua-Platform", "Windows");
                        //requestMsg.Headers.Add("X-Xsrf-Token", token);
                        //requestMsg.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
                        //requestMsg.Content = new StringContent (json, Encoding.UTF8, "application/json");
                        //var result = await client.SendAsync(requestMsg);
                        //result.EnsureSuccessStatusCode();
                        //if (result.IsSuccessStatusCode)
                        //{
                        //    Console.WriteLine("ok.");
                        //}


                    }
                }
            }
        }

        private async void postCnblogs(List<string> filePaths, WaitDialog waitDialog)
        {
            string cnblogs_url = "https://lezhifu.cc/admin/import_cnblogs";
            // 导入，请求博客园导入接口
            string cookie = cnblog_cookie_input.Text;
            string token = cnblog_token.Text;
            string category = cnblog_category.Text;
            string tag = cnblog_tag.Text;
            string desc = cnblog_desc.Text;
            bool isDraft = true;
            Console.WriteLine("cookie: " + cookie);
            Console.WriteLine("token: " + token);
            if (radioButton1.Checked)
            {
                isDraft = false;
            }

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(1440);

            // http请求超时时间10min
            var watch = Stopwatch.StartNew();

            foreach (string path in filePaths)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string title = Path.GetFileName(path);
                    string article_content = sr.ReadToEnd();

                    //string jsonData = JsonSerializer.Serialize(new
                    //{
                    //    postType = 1,
                    //    accessPermission = 0,
                    //    title = title,
                    //    postBody = article_content,
                    //    categories = new List<string>() { category },
                    //    inSiteCandidate = false,
                    //    inSiteHome = false,
                    //    isPublished = false,
                    //    isAllowComments = true,
                    //    description = desc,
                    //    tags = new List<string>() { tag },
                    //    isMarkdown = true,
                    //    isDraft = isDraft
                    //    // 添加你的JSON属性
                    //});
                    var jsonData = new Dictionary<string, object>
                    {
                        {"postType", 1},
                        {"accessPermission", 0},
                        {"title", title},
                        {"postBody", article_content},
                        {"categories", new List<string>() { category }},
                        {"inSiteCandidate", false},
                        {"inSiteHome", false},
                        {"isPublished", false},
                        {"isAllowComments", true},
                        {"description", desc},
                        {"tags", new List<string>() { tag }},
                        {"isMarkdown", true},
                        {"isDraft", isDraft }
                        // 添加你的JSON属性
                    };

                    try
                    {
                        using (var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1440)))
                        {
                            //var postContent = new FormUrlEncodedContent(new[]
                            //{
                            //    new KeyValuePair<string, string>("token", token),
                            //    new KeyValuePair<string, string>("cookie", cookie),
                            //    new KeyValuePair<string, string>("data", jsonData)
                            //});
                            var postContent = new Dictionary<string, object>
                            {
                                {"token", token},
                                {"cookie", cookie},
                                {"data", jsonData }
                            };
                            string jsonString = JsonConvert.SerializeObject(postContent);
                            Console.WriteLine(jsonString);
                            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await client.PostAsync(cnblogs_url, httpContent, tokenSource.Token);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                Console.WriteLine(content);
                            }
                            else
                            {
                                Console.WriteLine($"Error: {response.StatusCode}, cnblog upload url: {cnblogs_url}");
                            }
                        }
                    }
                    catch (TaskCanceledException ex)
                    {
                        MessageBox.Show($"{watch.Elapsed} s 任务超时");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"请求错误：{ex.ToString()}");
                    }
                    finally
                    {
                        Console.WriteLine($"{title} 导入完成。");
                        // 提示导出成功
                        CommonUtil.ShowMessageBoxWithTimeout($"{title}导入成功！", 3000);
                    }
                }
            }
            if (waitDialog != null)
            {
                // 关闭【加载中】进度条
                waitDialog.Close();
            }
        }
    }
}
