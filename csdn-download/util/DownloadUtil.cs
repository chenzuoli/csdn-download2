using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;

namespace csdn_download.util
{
    public class DownloadUtil
    {
        public static void downloadFile(string title, string downloadFolder, string fileUrl)
        {
            string remoteUri = System.IO.Path.GetDirectoryName(fileUrl);
            string fileName = System.IO.Path.GetFileName(fileUrl);
            string myStringWebResource = fileUrl;
            WebClient webClient = new WebClient();

            // 判断folder是否存在，不存在则创建
            string imgFolder = Path.Combine(downloadFolder, title);
            if (!System.IO.Directory.Exists(imgFolder))
            {
                System.IO.Directory.CreateDirectory(imgFolder);
            }

            // 下载
            string absFileName = Path.Combine(imgFolder, fileName);
            webClient.DownloadFile(myStringWebResource, absFileName);
            
            if (System.IO.File.Exists(absFileName))
            {
                Console.WriteLine(absFileName + " image download success.");
            }

            removeWatermark(absFileName);
        }

        private static void removeWatermark(string filePath)
        {
            try
            {
                Mat src = new Mat(filePath, ImreadModes.Color);
                var dst = new Mat(filePath);
                var mask = new Mat(src.Size(), MatType.CV_8UC1, Scalar.All(0));

                // 第一二个参数是需要去水印的区域开始位置（左上角）
                // 第三四个参数是水印区域的长度和宽度
                mask.Rectangle(new Rect(src.Width-250, src.Height-50, 250, 50), Scalar.All(255), -1);

                Cv2.Inpaint(src, mask, dst, 2, InpaintMethod.Telea);

                // debug时展示图片是否是我们想要的
                //  if (Debugger.IsAttached)
                //      Window.ShowImages(src, mask, dst);
                Cv2.ImWrite(filePath, dst);
                Console.WriteLine("inpainted new image.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static string getImgUrl(string line)
        {
            string pattern = @"https?:\/\/(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*,]|(?:%[0-9a-fA-F][0-9a-fA-F]))+(\.png|\.jpg|\.jpeg|\.gif|\.bmp)";

            Match match = Regex.Match(line, pattern);

            if (match.Success)
            {
                Console.WriteLine("图片URL: " + match.Value);
                return match.Value;
            }
            else
            {
                Console.WriteLine("没有找到图片URL。");
                return null;
            }
        }

        public static string parseTitle(string title)
        {
            title = title.Trim();
            title = title.Replace(" ", "_").Replace("\\", "_").Replace("/", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_");
            title = title.Replace("'", "_").Replace("\"", "_").Replace(">", "_").Replace("<", "_").Replace("|", "_").Replace("\0", "_");
            title = title.Replace("\r", "_").Replace("\n", "_").Replace("[", "_").Replace("]", "_");
            return title;
        }

    }
}
