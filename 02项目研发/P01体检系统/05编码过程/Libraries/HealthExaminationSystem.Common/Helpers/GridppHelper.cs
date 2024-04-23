using System;
using System.Net;
using System.Windows.Forms;

using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 报表模板帮助
    /// </summary>
    public class GridppHelper
    {
        
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <returns></returns>
        public static string GetTemplate(string name)
        {
            var url = new UriBuilder(Variables.GetUrl()) { Path = $"{Variables.GrfDirectory}/{name}" };
            return url.ToString();
        }
        /// <summary>
        /// 下载服务器文件至客户端
        /// </summary>
        /// <param name="URL">被下载的文件地址，绝对路径</param>
        /// <param name="Dir">文件名</param>
        public static void Download(string URL, string Dir)
        {
            WebClient client = new WebClient();
            try
            {
                WebRequest myre = WebRequest.Create(URL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "下载提示");
            }
            try
            {
                //提示用户选择文件在保存位置
                SaveFileDialog sfd = new SaveFileDialog();
                //设置文件类型 
                sfd.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
                //设置文件名
                sfd.FileName = Dir;
                //设置默认文件类型显示顺序 
                sfd.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录 
                sfd.RestoreDirectory = true;

                //点了保存按钮进入 
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = sfd.FileName.ToString(); //获得对话框选定在文件路径 
                    client.DownloadFile(URL, localFilePath);//下载文件到本地

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "下载提示");
            }
        }
        public static bool VitrualFileExist(string url)
        {
            try
            {
                //创建根据网络地址的请求对象
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.CreateDefault(new Uri(url));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 1000;
                //返回响应状态是否是成功比较的布尔值
                return (((System.Net.HttpWebResponse)httpWebRequest.GetResponse()).StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
            /// <summary>
            /// 打印预览
            /// </summary>
            /// <param name="templateName">模版名称，内部获取url</param>
            /// <param name="formTitle">预览窗体标题</param>
            /// <param name="dataObject">数据对象</param>
            public static void GridReportPrintPreview(string templateName, string formTitle, object dataObject)
        {
            var Report = new gregn6Lib.GridppReport();
            var templateUrl = GridppHelper.GetTemplate(templateName);
            Report.LoadFromURL(templateUrl);
            if(!string.IsNullOrEmpty(formTitle))
                Report.Title = formTitle;
            var json = JsonConvert.SerializeObject(dataObject);
            Report.LoadDataFromXML(json);
            Report.PrintPreview(true);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="dataObject">数据对象</param>
        public static void GridReportPrintPreview(string templateName, object dataObject)
        {
            GridReportPrintPreview(templateName, null, dataObject);
        }

    }
}