using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmIllContent : UserBaseForm
    {
        public string IllContent = "";
        public string OldeContent = "";
        public frmIllContent()
        {
            InitializeComponent();
        }

        private void frmIllContent_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OldeContent))
            {
                textIll.Text = OldeContent;                 
            }
            if (!string.IsNullOrEmpty(IllContent))
            {
                if (string.IsNullOrEmpty(textIll.Text))
                { textIll.Text = IllContent; }
                else
                {
                    var cotentlist = textIll.Text.Split('|').ToList();
                    if (!cotentlist.Contains(IllContent))
                    {
                        textIll.Text += "|" + IllContent;
                    }
                }
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            IllContent = textIll.Text;
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
