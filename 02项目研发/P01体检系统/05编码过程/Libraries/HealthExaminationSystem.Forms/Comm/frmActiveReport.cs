using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class frmActiveReport : Form
    {
        public frmActiveReport()
        {
            InitializeComponent();
        }

        private void frmActiveReport_Load(object sender, EventArgs e)
        {
            
        }
        public void printReport(string cusregBM, string URL, string path, bool print)
        {

            try
            {
                // String URL = "http://192.168.222.1:8080/4.rdlx";
                String Info;
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(URL);
                wbRequest.Proxy = null;
                wbRequest.Method = "GET";
                wbRequest.ContentType = "application/xml; charset=UTF-8";
                MemoryStream ms_pub = new MemoryStream();

                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        byte[] byte_pub = System.Text.Encoding.UTF8.GetBytes(sReader.ReadToEnd());
                        ms_pub.Write(byte_pub, 0, byte_pub.Length);
                        ms_pub.Seek(0, SeekOrigin.Begin);
                    }
                }
                using (TextReader StreamTxtRead = new StreamReader(ms_pub))
                {
                    PageReport rpt1 = new PageReport(StreamTxtRead);
                    rpt1.Report.ReportParameters[0].DefaultValue.Values.Add(cusregBM);
                    viewer1.LoadDocument(rpt1.Document);
                    if (print == true)
                    {
                        GrapeCity.ActiveReports.Document.PageDocument pageDocument = new GrapeCity.ActiveReports.Document.PageDocument(rpt1);
                        pageDocument.Print(true, true, false);
                    }
                    //导出word
                    if (File.Exists(path))
                    {
                        // Provide the page report you want to render.
                        GrapeCity.ActiveReports.PageReport report = new GrapeCity.ActiveReports.PageReport();
                        GrapeCity.ActiveReports.Document.PageDocument reportDocument = new GrapeCity.ActiveReports.Document.PageDocument(report);

                        // Create an output directory.
                        System.IO.DirectoryInfo outputDirectory = new System.IO.DirectoryInfo(path);
                        outputDirectory.Create();

                        // Provide settings for your rendering output.
                        GrapeCity.ActiveReports.Export.Word.Page.Settings wordSetting = new GrapeCity.ActiveReports.Export.Word.Page.Settings();

                        // Set the FileFormat property to .OOXML.
                        wordSetting.FileFormat = GrapeCity.ActiveReports.Export.Word.Page.FileFormat.OOXML;

                        // Set the rendering extension and render the report.
                        GrapeCity.ActiveReports.Export.Word.Page.WordRenderingExtension wordRenderingExtension = new GrapeCity.ActiveReports.Export.Word.Page.WordRenderingExtension();
                        GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider outputProvider = new GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider(outputDirectory, System.IO.Path.GetFileNameWithoutExtension(outputDirectory.Name));

                        // Overwrite output file if it already exists.
                        outputProvider.OverwriteOutputFile = true;

                        reportDocument.Render(wordRenderingExtension, outputProvider, wordSetting);
                    }


                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //  return 0;

        }
    }
}
