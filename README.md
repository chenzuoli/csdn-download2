# csdn文章导出

依赖包，无需用户安装，只是列在此处，供二次开发者查看：

1. packages/HtmlAgilityPack.1.11.70
2. packages/Microsoft.Bcl.AsyncInterfaces.8.0.0
3. packages/Newtonsoft.Json.13.0.3
4. packages/ReverseMarkdown.4.6.0
5. packages/Selenium.Support.4.25.0
6. packages/Selenium.WebDriver.4.25.0
7. packages/System.Buffers.4.5.1
8. packages/System.Memory.4.5.5
9. packages/System.Numerics.Vectors.4.5.0
10. packages/System.Runtime.CompilerServices.Unsafe.6.0.0
11. packages/System.Text.Encodings.Web.8.0.0
12. packages/System.Text.Json.8.0.4
13. packages/System.Threading.Tasks.Extensions.4.5.4
14. packages/System.ValueTuple.4.5

## 查看博客
![view](images/view.png)


## 导出步骤
1. 填写导出账号
![account](images/account.png)
2. 选择导出的文章
3. 导出


图片去水印依赖包：
1. OpenCvSharp4
2. OpenCvSharp4.runtime.<os>，如果是win则安装OpenCvSharp4.runtime.win，如果是ubuntu，则安装OpenCvSharp4.runtime.ubuntu或OpenCvSharp4.runtime.linux

图片已去CSDN水印，并保存在markdown文件同名文件夹中。

# cnblogs博客园导入
选择本地markdown文章，批量导入
![import_cnblogs](images/import_cnblogs.png)
1. 需提前浏览器登录cnblogs网站，并获取登录cookie和导入文章token；
![cookie](images/cookie.png)
![token](images/token.png)
2. 图片导入cnblogs获取图片在cnblogs网站的地址；
3. 上传markdown文件，替换文件中的图片地址为第二步获取的图片地址；
4. 填写参数
![params](images/params.png)
5. 导入成功
![success](images/success.png)

# 使用
1. 用户电脑必须安装google chrome浏览器
2. 进入csdn-download/bin/Debug，点击csdn-download.exe
3. 或者安装到计算机：进入Setup1/Debug，点击setup.exe


---

祝导出愉快。


如果有任何问题，欢迎提issue。



本软件由【<b>乐知付加密平台</b>】开源，乐知付加密平台，是一个以用户为中心的内容变现平台，无论是专业创作者还是个人爱好者，都可以通过我们平台实现变现梦想。

您无需亲自搭建知识付费服务平台，将知识资源放在网盘中，通过加密平台，进行压缩包密码管理，买家支付后展示网盘中压缩包密码，轻松资源变现。


官网地址：[www.lezhifu.cc](https://lezhifu.cc)

公众号：
<div style="text-align: center;">  
    <img src="images/image.png" alt="乐知付" style="width: 50%;">  
</div>