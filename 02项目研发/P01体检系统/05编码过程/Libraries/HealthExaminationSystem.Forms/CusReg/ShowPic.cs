using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class ShowPic : UserBaseForm
    {
        public string customerBm;
        public string cusName;
        public string cusSex;
        public string cusAge;
        public Image cusImage;
        public ShowPic()
        {
            InitializeComponent();
        }
        public ShowPic(string bm ,string name,string sex,string age,Image image)
        {
            InitializeComponent();
            customerBm = bm;
            cusName = name;
            cusSex = sex;
            cusAge = age;
            cusImage = image;
        }
        private void ShowPic_Load(object sender, EventArgs e)
        {
            labBm.Text ="体检号：" + customerBm;
            labName.Text = "姓名：" + cusName;
            labSex.Text = "性别：" + cusSex;
            labAge.Text = "年龄：" + cusAge;
            pictureCus.Image = cusImage;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
