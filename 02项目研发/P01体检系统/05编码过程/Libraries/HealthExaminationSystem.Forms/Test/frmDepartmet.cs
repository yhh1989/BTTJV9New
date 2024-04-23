using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem
{
    public partial class frmDepartmet : Form
    {
        public frmDepartmet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var d = new ApiProxy.Users.UserAppService();
           // var user = d.GetUser(new ApiProxy.EntityDto<long> { Id = 1 });
            
        }
    }
}
