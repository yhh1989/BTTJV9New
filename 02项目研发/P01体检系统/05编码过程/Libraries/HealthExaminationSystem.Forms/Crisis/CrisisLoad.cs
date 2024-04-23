using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public delegate void SendEventHandlers(CrisisCustomerDto CrisisCustomerDto); 
    public partial class CrisisLoad : UserBaseForm
    {
        private CrisisCustomerDto CrisisCustomerDto;
        private handle handle;
        public CrisisLoad(handle handle)
        {
            InitializeComponent();
            this.handle = handle;

            this.handle.SendEvent+=new SendEventHandler(main_SendEvent);
        }
        public event SendEventHandlers SendEvents;
        public string sss = "";

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void CrisisLoad_Load(object sender, EventArgs e)
        {

        }

        void main_SendEvent(CrisisCustomerDto CrisisCustomerDto) 
        {
            label3.Text = CrisisCustomerDto.CustomerBM;
            label7.Text = CrisisCustomerDto.Age.ToString();
            label6.Text = CrisisCustomerDto.Sexs;
            label8.Text = CrisisCustomerDto.ItemName;
            label10.Text = CrisisCustomerDto.ItemResultChar;
            this.CrisisCustomerDto = CrisisCustomerDto;
        }
        public CrisisLoad(CrisisCustomerDto CrisisCustomerDto)
        {
            InitializeComponent();
            label3.Text = CrisisCustomerDto.CustomerBM;
            label7.Text = CrisisCustomerDto.Age.ToString();
            label6.Text = CrisisCustomerDto.Sexs;
            label8.Text = CrisisCustomerDto.ItemName;
            label10.Text = CrisisCustomerDto.ItemResultChar;
            this.CrisisCustomerDto = CrisisCustomerDto;
           
            // this.handle.SendEvent += new SendEventHandler(main_SendEvent);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CrisisManage aCrisisManage = new CrisisManage(this);
            this.SendEvents(this.CrisisCustomerDto);
            
            aCrisisManage.ShowDialog();
            this.Close();
        }
    }
}
