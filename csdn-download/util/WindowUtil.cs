using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace csdn_download.util
{
    public class CommonUtil
    {

        public static void ShowMessageBoxWithTimeout(string message, int timeout)
        {
            // 创建提示框
            // 倒计时弹出提示框
            Form messageBoxForm = new Form()
            {
                //Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "提示",
                StartPosition = FormStartPosition.CenterScreen,
                AutoSize = true
            };

            System.Windows.Forms.Label messageLabel = new System.Windows.Forms.Label()
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
    public class WindowUtil
    {
        public WindowUtil() { }

        public void closeOtherForms()
        {
            // 获取当前打开的所有窗体
            Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
            foreach (Form form in forms)
            {
                Console.WriteLine(form.Name);
                //if (form.Name != "MainForm")
                //{
                //    form.Close();
                //}
            }
        }

    }

    public class HttpHelper
    {
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreateGetHttpResponse(string url, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;    //http版本，默认是1.1,这里设置为1.0
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "GET";

            //设置代理UserAgent和超时
            //request.UserAgent = userAgent;
            //request.Timeout = timeout;
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, object> parameters, IDictionary<string, object> headers, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                //request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = headers["Content-Type"].ToString();
            request.Referer = headers["Referer"].ToString();
            //var cookieContainer = new CookieContainer();
            //cookieContainer.Add(cookies);
            //request.CookieContainer = cookieContainer;
            request.UserAgent = userAgent;

            var excepList = new List<string>() { "Cookie", "Referer", "Content-Type", "User-Agent" };
            foreach (KeyValuePair<string, object> kvp in headers)
            {
                if ( !excepList.Contains(kvp.Key.ToString()) )
                {
                    request.Headers.Add(kvp.Key.ToString(), kvp.Value.ToString());
                }
            }

            //设置代理UserAgent和超时
            request.UserAgent = userAgent;
            request.Timeout = timeout; 

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();

            }
        }

        /// <summary>
        /// 验证证书
        /// </summary>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
    }
}
