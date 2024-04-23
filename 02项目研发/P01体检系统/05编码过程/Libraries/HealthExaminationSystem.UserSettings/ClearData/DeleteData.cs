using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ClearData;
using Sw.Hospital.HealthExaminationSystem.Application.Common;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ClearData
{
    public partial class DeleteData : UserBaseForm
    {
        private readonly ClearDataAppService _clearDataAppService;
        private readonly ICommonAppService _commonAppService;
        public DeleteData()
        {
            InitializeComponent();
            _clearDataAppService = new ClearDataAppService();
            
        }

        private void DeleteData_Load(object sender, EventArgs e)
        {
            // 初始化时间框
            //var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.EditValue = DateTime.Now;
            starttime.EditValue = DateTime.Now;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var startTime = starttime.EditValue.ToString();
            var endTime= dateEditEnd.EditValue.ToString();
            ClearDataDto clearDataDto = new ClearDataDto();
            clearDataDto.StartTime = startTime;
            clearDataDto.EndTime = endTime;
            DataBaseDto result = new DataBaseDto();
            result.Leixing = 0;
            AutoLoading(() =>
            {
                result= _clearDataAppService.delTableByTiem(clearDataDto);
            });
            if (result.Leixing > 0)
            {
                MessageBox.Show("删除成功");
            }
            //var result = _clearDataAppService.TimeDeleteData(clearDataDto);
            //if (result.Leixing > 0)
            //{
            //    MessageBox.Show("删除成功");
            //}
            //else {
            //    MessageBox.Show("请重新删除");
            //}
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //DataBaseDto result = new DataBaseDto();
            //result.Leixing=0;
            //  AutoLoading(() =>
            //{
            //     result = _clearDataAppService.AllDeleteData();
            //});
            //if (result.Leixing > 0)
            //{
            //    MessageBox.Show("删除成功");
            //}
            //else {
            //    MessageBox.Show("请重新删除");
            //}       
            //ClearDataDto clearDataDto = new ClearDataDto();
            //clearDataDto.StartTime = DateTime.Now.AddYears(-15).ToString();
            //clearDataDto.EndTime = DateTime.Now.ToString();
            frmCheck frmCheck = new frmCheck();
            frmCheck.ShowDialog();
            if (frmCheck.DialogResult == DialogResult.OK)
            {
                DataBaseDto result = new DataBaseDto();
                result.Leixing = 0;
                AutoLoading(() =>
                {
                    InputClearData inputClearData = new InputClearData();
                    inputClearData.isAbpLog = frmCheck.isAbpLog;
                    inputClearData.isInterfase = frmCheck.isInterfase;
                    inputClearData.isLog = frmCheck.isLog;
                    inputClearData.isSuit = frmCheck.isSuit;
                    inputClearData.isTjl = frmCheck.isTjl;
                    inputClearData.isUser = frmCheck.isUser;


                    result = _clearDataAppService.AllDeleteData(inputClearData);
                });
                if (result.Leixing > 0)
                {
                    MessageBox.Show("删除成功");
                }
            }
        }
    }
}
