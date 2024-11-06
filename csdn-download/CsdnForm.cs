using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormWaitDialog;
using ReverseMarkdown;

namespace csdn_download
{
    public partial class CsdnForm : Form
    {
        public CsdnForm()
        {
            InitializeComponent();
        }

        private void CsdnForm_Load(object sender, EventArgs e)
        {

        }

        private void exportBtn_Click(object sender, EventArgs e)
        {

            // 获取选择的博客链接
            var ifSeleted = false;
            var selectedUrls = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Console.WriteLine(dataGridView1.Rows[i].ToString());
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                {
                    ifSeleted = true;
                    var url = dataGridView1.Rows[i].Cells[2].Value;
                    if (url != null)
                    {
                        selectedUrls.Add(url.ToString());
                    }
                };
            }
            if (!ifSeleted)
            {
                MessageBox.Show("未选择任务博客文章，请选择后再导出。");
                return;
            }

            // 选择导出文件夹
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "请选择一个文件夹保存博客文章";
            DialogResult dialogResult = folderDialog.ShowDialog();
            string selectedFolder = "";
            if (dialogResult == DialogResult.OK)
            {
                selectedFolder = folderDialog.SelectedPath;
                Console.WriteLine("选择的文件夹为：" + selectedFolder);
            }
            else
            {
                MessageBox.Show("请选择文件夹保存博客文章");
                return;
            }

            // 爬取文章并导出
            get_article_contents(selectedFolder, selectedUrls);

        }

        private async void get_articles(string selectedFolder, List<string> article_urls)
        {
            var waitDialog = new WaitDialog((IProgress<string> progress) =>
            {
                LongRunningProcess(progress);
            });
            waitDialog.Show();

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(35);

            // http请求超时时间10min
            var watch = Stopwatch.StartNew();
            try
            {
                using (var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(30)))
                {
                    for (int i = 0; i < article_urls.Count; i++)
                    {
                        string article_url = article_urls[i];
                        string get_article_content_url = "http://localhost:5000/csdn/get_article?csdn_url=" + article_url;
                        HttpResponseMessage response = await client.GetAsync(get_article_content_url, tokenSource.Token);
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(content);

                            // 导出到文件夹中
                            Dictionary<string, string> article_info = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                            write_markdown(selectedFolder, article_info);
                            Thread.Sleep(1000); // sleep 1s
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode}, csdn download url: {article_url}");
                        }
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
                if (waitDialog != null)
                {
                    // 关闭【加载中】进度条
                    waitDialog.Close();
                }
            }
        }

        private async Task get_article_contents(string selectedFolder, List<string> article_urls)
        {
            var waitDialog = new WaitDialog((IProgress<string> progress) =>
            {
                LongRunningProcess(progress);
            });
            waitDialog.Show();

            // 等待异步请求文章内容完成
            await Task.Run(() =>
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(35);

                // http请求超时时间10min
                var watch = Stopwatch.StartNew();
                try
                {
                    using (var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(30)))
                    {
                        for (int i = 0; i < article_urls.Count; i++)
                        {
                            string article_url = article_urls[i];
                            var article_info = RequestArticleContent(article_url);
                            write_markdown(selectedFolder, article_info);
                            Thread.Sleep(1000);
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
            });

            if (waitDialog != null)
            {
                // 关闭【加载中】进度条
                waitDialog.Close();
            }

            // 提示导出成功
            ShowMessageBoxWithTimeout("导出成功！", 3000);
        }

        private string parseTitle(string title)
        {
            title = title.Trim();
            title = title.Replace(" ", "_").Replace("\\", "_").Replace("/", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_");
            title = title.Replace("'", "_").Replace("\"", "_").Replace(">", "_").Replace("<", "_").Replace("|", "_").Replace("\0", "_");
            title = title.Replace("\r", "_").Replace("\n", "_").Replace("[", "_").Replace("]", "_");
            return title;
        }

        private void write_markdown(string selectedFolder, Dictionary<string, string> article_info)
        {
            // 写markdown文件
            string content = article_info["content"];
            string title = article_info["title"].Replace("-CSDN博客", "");
            title = parseTitle(title);
            File.WriteAllText(Path.Combine(selectedFolder, title + ".md"), content);
        }

        private void viewBlogBtn_Click(object sender, EventArgs e)
        {
            // 加载中
            var waitDialog = new WaitDialog((IProgress<string> progress) =>
            {
                LongRunningProcess(progress);
            });
            waitDialog.Show();
            GetCsdnData(waitDialog);
            //load_articles(waitDialog);
        }

        private async void load_articles(WaitDialog waitDialog)
        {
            string user_id = textBox1.Text;
            if (string.IsNullOrEmpty(user_id))
            {
                waitDialog.Close();
                MessageBox.Show("请输入用户ID");
                return;
            }

            string csdn_url = "http://localhost:5000/csdn/get_articles/" + user_id;
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(35);

            // http请求超时时间10min
            var watch = Stopwatch.StartNew();
            try
            {
                using (var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(30)))
                {
                    HttpResponseMessage response = await client.GetAsync(csdn_url, tokenSource.Token);
                    if (response.IsSuccessStatusCode)
                    {

                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);

                        // 填充表格内容
                        List<Dictionary<string, string>> articles = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(content);
                        fillGridView(articles);
                        Thread.Sleep(1000); // sleep 1s
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}, csdn download url: {csdn_url}");
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
                if (waitDialog != null)
                {
                    // 关闭【加载中】进度条
                    waitDialog.Close();
                }
            }
        }

        private async void LongRunningProcess(IProgress<string> progress)
        {
            for (int i = 0; i < 100; i++)
            {
                progress.Report($"csdn downloading...");
                Thread.Sleep(10000);
            }
        }

        private void fillGridView(List<Dictionary<string, string>> data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("文章标题", typeof(string));
            dt.Columns.Add("文章链接", typeof(string));
            dt.Columns.Add("发表时间", typeof(string));
            for (int i = 0; i < data.Count; i++)
            {
                Dictionary<string, string> article_info = data[i];
                dt.Rows.Add(article_info["title"], article_info["url"], article_info["post_time"]);
            }
            // 将DataTable数据绑定到DataGridView
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.DataSource = dt;
            // 让datatable自动填满整个DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 在每行的第一列添加复选框
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = "";
            checkboxColumn.Name = "checkBoxColumn";
            dataGridView1.Columns.Insert(0, checkboxColumn);

            // fill情况下，指定某列宽度
            dataGridView1.Columns[0].FillWeight = 20;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == -1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                using (Brush brush = new SolidBrush(e.CellStyle.ForeColor))
                {
                    e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.CellStyle.Font, brush, e.CellBounds.Location.X + 14, e.CellBounds.Location.Y + 8);
                }
                e.Handled = true;
            }
        }

        private void loadingLabel(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("加载中...");
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            // 遍历datagridview每一行，判断是否已经选中，若为选中，则选中
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == false)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }
                else
                    continue;
            }
        }

        private void cancelSelectAll_Click(object sender, EventArgs e)
        {
            // 取消全选
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;
                }
                else continue;
            }
        }

        private void reverseSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == false)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;
                }
            }
        }

        /// <summary>
        /// 获取所有文章链接
        /// </summary>
        /// <returns></returns>
        public async Task GetCsdnData(WaitDialog waitDialog)
        {
            string user_id = textBox1.Text;
            if (string.IsNullOrEmpty(user_id))
            {
                waitDialog.Close();
                MessageBox.Show("请输入用户ID");
                return;
            }

            var article_infos = new List<Dictionary<string, string>>();

            // 在后台线程上运行 Selenium 操作  ，让waitDialog处于加载中状态
            await Task.Run(() =>
            {
                string csdn_url = "https://blog.csdn.net/" + user_id;
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(35);

                // http请求超时时间10min
                var watch = Stopwatch.StartNew();
                try
                {
                    using (var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(30)))
                    {
                        var chrome_options = new OpenQA.Selenium.Chrome.ChromeOptions();
                        chrome_options.AddArgument("--headless"); //不展示浏览器页面
                        chrome_options.AddArgument("--disable-gpu");
                        chrome_options.AddArgument("--start-maximized");  // 最大化窗口

                        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                        service.HideCommandPromptWindow = true; // 隐藏命令行窗口
                        IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, chrome_options);
                        driver.Navigate().GoToUrl(csdn_url);
                        // 加载页面

                        var timeouts = driver.Manage().Timeouts();

                        // 等待页面body标签加载完成
                        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                        var element = wait.Until(condition =>
                        {
                            try
                            {
                                var elementToBeDisplayed = driver.FindElement(By.TagName("body"));
                                return elementToBeDisplayed.Displayed;
                            }
                            catch (StaleElementReferenceException)
                            {
                                return false;
                            }
                            catch (NoSuchElementException)
                            {
                                return false;
                            }
                        });

                        var body = driver.FindElement(By.TagName("body"));
                        long pageHeight = driver.Manage().Window.Size.Height;
                        Console.WriteLine("init page heigh: " + pageHeight.ToString());
                        while (true)
                        {
                            // 滑动到底部，加载更多文章，直到无法加载更多为止
                            //body.SendKeys(OpenQA.Selenium.Keys.Control + "End" + OpenQA.Selenium.Keys.Null);
                            Actions actions = new Actions(driver);
                            actions.KeyDown(OpenQA.Selenium.Keys.Control).SendKeys(OpenQA.Selenium.Keys.End).KeyUp(OpenQA.Selenium.Keys.Control).Perform();
                            Thread.Sleep(4000);
                            //int newHeight = driver.Manage().Window.Size.Height;
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            var newHeight = (long)js.ExecuteScript("return document.body.scrollHeight;");
                            Console.WriteLine("new height: " + newHeight.ToString());
                            Console.WriteLine("page height: " + pageHeight.ToString());
                            if (newHeight == pageHeight)
                            {
                                Console.WriteLine("页面到达了底部");
                                var title = driver.Title;
                                var contents = driver.FindElements(By.CssSelector("article.blog-list-box"));
                                // 获取所有文章链接信息
                                for (int i = 0; i < contents.Count; i++)
                                {
                                    var article_info = new Dictionary<string, string>();
                                    var link = contents[i].FindElement(By.TagName("a"));
                                    string href = link.GetAttribute("href");
                                    string post_time = link.FindElement(By.ClassName("view-time-box")).Text.Split(' ')[1];
                                    string article_title = link.FindElement(By.ClassName("blog-list-box-top")).Text;
                                    article_info["url"] = href;
                                    article_info["post_time"] = post_time;
                                    article_info["title"] = article_title;

                                    article_infos.Add(article_info);
                                }
                                break;
                            }
                            pageHeight = newHeight;
                        }
                        Console.WriteLine("文章列表：" + article_infos.ToList());

                        // 退出浏览器
                        driver.Quit();

                        Thread.Sleep(1000);
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
            });

            if (waitDialog != null)
            {
                // 关闭【加载中】进度条
                waitDialog.Close();
            }

            fillGridView(article_infos);

        }

        private Dictionary<string, string> RequestArticleContent(string article_url)
        {
            string content = "";
            string title = "";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(35);

            // http请求超时时间10min
            var watch = Stopwatch.StartNew();
            try
            {
                using (var tokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(30)))
                {
                    var chrome_options = new OpenQA.Selenium.Chrome.ChromeOptions();
                    chrome_options.AddArgument("--headless"); // 不展示浏览器页面
                    chrome_options.AddArgument("--disable-gpu");
                    chrome_options.AddArgument("--start-maximized");  // 最大化窗口
                    chrome_options.PageLoadStrategy = PageLoadStrategy.Eager;

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true; // 隐藏命令行窗口
                    IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, chrome_options);
                    driver.Navigate().GoToUrl(article_url);
                    // 加载页面

                    var timeouts = driver.Manage().Timeouts();

                    // 等待页面body标签加载完成
                    var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                    var element = wait.Until(condition =>
                    {
                        try
                        {
                            var elementToBeDisplayed = driver.FindElement(By.TagName("body"));
                            return elementToBeDisplayed.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            return false;
                        }
                        catch (NoSuchElementException)
                        {
                            return false;
                        }
                    });

                    var html = driver.PageSource;

                    // like beautifulsoup parse html
                    // https://html-agility-pack.net/parser
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    title = doc.DocumentNode.SelectSingleNode("//title").InnerText;

                    // markdown the content
                    HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode("//div[@id='content_views']");
                    // 检查节点是否存在
                    if (htmlNode != null)
                    {
                        // 选取该节点下所有的svg标签
                        var svgNodes = htmlNode.Descendants("svg").ToList();

                        // 遍历并删除每个svg节点
                        foreach (var svgNode in svgNodes)
                        {
                            svgNode.Remove();
                        }
                    }
                    var converter = new Converter();
                    content = converter.Convert(htmlNode.InnerHtml);
                    Console.WriteLine(content);
                    // 退出浏览器
                    driver.Quit();

                    Thread.Sleep(1000);
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
            var data = new Dictionary<string, string>();
            data.Add("title", title);
            data.Add("content", content);
            return data;
        }

        private void ShowMessageBoxWithTimeout(string message, int timeout)
        {
            // 创建提示框
            Form messageBoxForm = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "提示",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label messageLabel = new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Text = message,
                Left = 50,
                Top = 50
            };
            messageBoxForm.Controls.Add(messageLabel);

            // 显示提示框
            messageBoxForm.Show();

            // 设置定时器关闭提示框
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer()
            {
                Interval = timeout
            };
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                messageBoxForm.Close();
            };
            timer.Start();
        }

    }
}
