using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OConDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OConDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    public partial class DictionariesList : UserBaseForm
    {
        private IOConDictionaryAppService _conDictionaryAppService { get; set; }
       
        public DictionariesList()
        {
            InitializeComponent();
            _conDictionaryAppService = new OConDictionaryAppService();
         
        }

        private void DictionariesList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void sbReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            List<ZYBTypeDto> ConDictionary = _conDictionaryAppService.ZYBTypeGetAll();
            gridControl.DataSource = ConDictionary;
            //AutoLoading(() =>
            //{
               
            //});

        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new DictionariesSet())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                ShowMessageBoxWarning("请选择数据行后编辑");
                return;
            }
            DialogResult dr = XtraMessageBox.Show("确定删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                var curGuid = (gridView1.GetFocusedRow() as ZYBTypeDto).Id;
                try
                {
                    _conDictionaryAppService.ZYBTypeDelete(new ZYBTypeDto() { Id = curGuid });
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                LoadData();
            }
        }

        private void sbEdit_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                ShowMessageBoxWarning("请选择数据行后编辑");
                return;
            }

            using (var frm = new DictionariesSet())
            {
                frm.zybTypeDto = gridView1.GetFocusedRow() as ZYBTypeDto;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            sbEdit_Click(sender, e);
        }
    }
}
