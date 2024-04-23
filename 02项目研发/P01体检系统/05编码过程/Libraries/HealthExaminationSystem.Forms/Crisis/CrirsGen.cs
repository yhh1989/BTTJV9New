using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public partial class CrirsGen : Form
    {
        private CrisisCustomerDto CrisisCustomerDto;
        private handle handle;
        private readonly Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis.CrisisAppService _crisisappservice;
        public CrirsGen(handle handle)
        {
            _crisisappservice = new Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis.CrisisAppService();
            InitializeComponent();
            this.handle = handle;
            this.handle.SendEventCrirsGen += new SendEventCrirsGen(main_SendEventCrirsGen); 
        }

        private void CrirsGen_Load(object sender, EventArgs e)
        {

        }

        void main_SendEventCrirsGen(CrisisCustomerDto CrisisCustomerDto) 
        {
            label3.Text = CrisisCustomerDto.CustomerBM;
            label12.Text = CrisisCustomerDto.Name;
            label7.Text = CrisisCustomerDto.Age.ToString();
            label6.Text = CrisisCustomerDto.Sexs;
            label8.Text = CrisisCustomerDto.ItemName;
            label14.Text = CrisisCustomerDto.Stand;
            
            label10.Text = DefinedCacheHelper.GetComboUsers().Where(o => o.Id == CrisisCustomerDto.CallBacKUserId).FirstOrDefault()?.Name;
            label19.Text = DefinedCacheHelper.GetComboUsers().Where(o => o.Id == CrisisCustomerDto.SHUserId).FirstOrDefault()?.Name;
            richTextBox1.Text = CrisisCustomerDto.CallBacKContent;
            richTextBox2.Text = CrisisCustomerDto.SHContent;
            this.CrisisCustomerDto = CrisisCustomerDto;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.CrisisCustomerDto.CallBacKContent = richTextBox1.Text;
            this.CrisisCustomerDto.SHContent = richTextBox2.Text;
            var result = _crisisappservice.UpdateTjlCrisVisit(this.CrisisCustomerDto);
            if (1 != result)
            {
                MessageBox.Show("处理失败");
            }
            else
            {
                this.Close();
            }
        }
    }
}
