using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using File = System.IO.File;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class frmsxqm : Form
    {
        //inform message definition
        static int complete_msg = 0x7ffe;
        static int cancel_msg = 0x7ffd;

        //return value of interface function
        static int HW_eOk = 0;      //success
        static int HW_eDeviceNotFound = -1;     //no device
        static int HW_eFailedLoadModule = -2;   //failed to load module
        static int HW_eFailedInitModule = -3;   //failed to inti module 
        static int HW_eWrongImageFormat = -4;   //do not support this image format
        static int HW_eNoSignData = -5;         //no sign data
        static int HW_eInvalidInput = -6;       //invalid input parameter
        public Image imagesxqm = null;
        public string imagesxqms = "";
        public string strima = System.Environment.CurrentDirectory ;
        public frmsxqm()
        {
            if (Directory.Exists(strima + "\\Photo") == false)
            {
                Directory.CreateDirectory(strima + "\\Photo");
            }
            strima = strima + "\\Photo\\" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".png";
            InitializeComponent();
            axHWPenSign1.HWSetBkColor(0xE0F8E0);
            axHWPenSign1.HWSetCtlFrame(4, 0x000000);
            axHWPenSign1.HWSetFilePath(strima);
            axHWPenSign1.HWSetExtWndHandle(this.Handle.ToInt32());
        }

        private void button1_Click(object sender, EventArgs e)      //open device
        {
           int res = axHWPenSign1.HWInitialize();
           if(res != HW_eOk)
           {
               MessageBox.Show("没有找到驱动");
           }
        }

        private void button2_Click(object sender, EventArgs e)      //close device
        {
            axHWPenSign1.HWFinalize();
        }

        private void button3_Click(object sender, EventArgs e)      //clear sign
        {
            axHWPenSign1.HWClearPenSign();
        }

        private void button4_Click(object sender, EventArgs e)      //save image
        {
            axHWPenSign1.HWSaveFile();
            if (File.Exists(strima))
            {
                imagesxqm = Image.FromFile(strima);
                imagesxqms = strima;
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
           //imagesxqm=PictureBox
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == complete_msg)
                MessageBox.Show("连接");
            else if (m.Msg == cancel_msg)
                MessageBox.Show("清除");
            
            base.WndProc(ref m);
        }

        private void frmsxqm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strima))
                {
                    File.Delete(strima);
                }
            }
            catch (Exception)
            {

               
            }
           
            button1_Click(sender, e);
        }

        private void frmsxqm_FormClosed(object sender, FormClosedEventArgs e)
        {
            button2_Click(sender, e);
        }
    }
}
