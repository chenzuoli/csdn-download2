using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csdn_download.util
{
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
}
