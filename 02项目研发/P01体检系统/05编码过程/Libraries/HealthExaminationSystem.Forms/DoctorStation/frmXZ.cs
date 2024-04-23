using Microsoft.Win32;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmXZ : UserBaseForm
    {
        private string Url = "";
        public string patientNo = "";
        public frmXZ()
        {
            InitializeComponent();
        }     

        private void frmXZ_Load(object sender, EventArgs e)
        {
            var InterUrl = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.InterUrl, 1)?.Remarks;
            if (!string.IsNullOrEmpty(InterUrl))
            {
                Url = InterUrl;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string nowurl = Url + "?patientNo="+ patientNo + "&visitTypeCode=03";
            
            OpenUrl(nowurl);
            //System.Diagnostics.Process.Start(nowurl);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string nowurl = Url + "?visitSqNo="+ patientNo + "";
            OpenUrl(nowurl);
            //System.Diagnostics.Process.Start(nowurl);

        }
        /// <summary>
        /// C# Winform打开网址url
        /// </summary>
        /// <param name="url">要打开的网页网址</param>
        public void OpenUrl(string url)
        {
          
            OpenDefaultBrowserUrl(url);
            //var result1 = Process.Start("explorer.exe", url);
            //Process pro = new Process();
            //// pro.StartInfo.FileName = "iexplore.exe";

            //pro.StartInfo.Arguments = url;
            //pro.Start();
        }

        /// <summary>
         /// 打开系统默认浏览器（用户自己设置了默认浏览器）
         /// </summary>
         /// <param name="url"></param>
         public static void OpenDefaultBrowserUrl(string url)
         {
             try
             {
                 // 方法1
                 //从注册表中读取默认浏览器可执行文件路径
                 RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                 if (key != null)
                 {
                     string s = key.GetValue("").ToString();
                     //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！
                     //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
                     var lastIndex = s.IndexOf(".exe", StringComparison.Ordinal);
                     if (lastIndex == -1)
                     {
                         lastIndex = s.IndexOf(".EXE", StringComparison.Ordinal);
                     }
                     var path = s.Substring(1, lastIndex + 3);
                     var result = Process.Start(path, url);
                     if (result == null)
                     {
                         // 方法2
                         // 调用系统默认的浏览器 
                         var result1 = Process.Start("explorer.exe", url);
                         if (result1 == null)
                         {
                             // 方法3
                             Process.Start(url);
                         }
                     }
                 }
                 else
                 {
                     // 方法2
                     // 调用系统默认的浏览器 
                     var result1 = Process.Start("explorer.exe", url);
                     if (result1 == null)
                     {
                         // 方法3
                         Process.Start(url);
                     }
                 }
             }
             catch
             {
                Process pro = new Process();
                pro.StartInfo.FileName = "iexplore.exe";

                pro.StartInfo.Arguments = url;
                pro.Start();
            }
        }
  
 
    }
}
