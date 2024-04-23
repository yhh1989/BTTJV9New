using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet
{
    public partial class OccdiseaseMain : UserBaseForm
    {
        //private IOccDiseaseAppService _OccDiseaseAppService { get; set; }
        private readonly IOccDiseaseAppService  _OccDiseaseAppService;
        private readonly IOccDisProposalNewAppService _OccDisproAppService;

        public OccdiseaseMain()
        {
           
            InitializeComponent();
            _OccDiseaseAppService = new OccDiseaseAppService();
            _OccDisproAppService = new OccDisProposalNewAppService();

        }

        private void OccdiseaseMain_Load(object sender, EventArgs e)
        {
            
            LoadData();
        }
        

        private void LoadData()
        {
            
            var OccDiseaseDtos = new Occdieaserucan();
            OccDiseaseDtos.IsActive = 1;       
            var ConDictionary = _OccDiseaseAppService.GetAllOccDisease(OccDiseaseDtos);
            gridControl1.DataSource = ConDictionary;
            //一级分类
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Occupational.ToString();
            var dl = _OccDisproAppService.getOutOccDictionaryDto(chargeBM);
            comboBoxTop.Properties.DataSource= dl;
            //foreach (var item in ConDictionary)
            //{
            //    comboBoxSecondary.Text = item.Text;
            //}
            comboBoxSecondary.Properties.DataSource = ConDictionary;

            //当前状态
          
            var enableItems = InvoiceStateHelper.GetAdStateModels();
            comboBoxOpen.Properties.DataSource = enableItems;
            comboBoxOpen.EditValue = (int)InvoiceState.Enable;


        }
        //添加
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            using (var frm = new OccduseaseAdd())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }
       

        //查询
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var OccDiseaseDtos = new Occdieaserucan();
            //if (comboBoxTop.EditValue != null)
            //    OccDiseaseDtos.Type = (string)comboBoxTop.EditValue;
            if (comboBoxSecondary.EditValue != null)
                OccDiseaseDtos.Text = comboBoxSecondary.EditValue.ToString();
            if (comboBoxOpen.EditValue != null)
                OccDiseaseDtos.IsActive = Convert.ToInt32(comboBoxOpen.EditValue);
            try
            {               
                var result = _OccDiseaseAppService.GetAllOccDisease(OccDiseaseDtos);
                gridControl1.DataSource = null;
                gridControl1.DataSource = result;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        //删除
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            var id = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, gridid);
            if (id == null)
            {
                ShowMessageBoxError("尚未选定任何项目！");
                return;
            }
            var name = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, Name);

            if (XtraMessageBox.Show($"确定要删除项目 {name} 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                try
                {

                    // simpleButtonQuery.PerformClick();
                    var dto = gridControl1.GetFocusedRowDto<OutOccDiseaseDto>();
                    AutoLoading(() =>
                    {
                        _OccDiseaseAppService.OccDel(new EntityDto<Guid>
                        {
                            Id = (Guid)id
                        });
                        gridControl1.RemoveDtoListItem(dto);
                    }, Variables.LoadingDelete);
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBoxError(exception.ToString());
                }
            }
        }


        //修改
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            var id = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, gridid);
            var old = gridViewItemInfo.GetFocusedRow() as OutOccDiseaseDto;
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
                using (var frm = new OccduseaseAdd((Guid)id, old))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //simpleButton5.PerformClick();                       
                     

                        ModelHelper.CustomMapTo(frm._Model, old);
                        gridControl1.RefreshDataSource();

                    }
                }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            var fileName = saveFileDialog.FileName;
            ExcelHelper.ExportToExcel(fileName, gridControl1);
        }

        private void comboBoxSecondary_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            List<OutOccDiseaseDto> occdiseaselist= gridControl1.DataSource as List<OutOccDiseaseDto>;
            var bmoccdisease = occdiseaselist.GroupBy(p=>p.OrderNum).Where(p=>p.Count()>1).ToList();
            var IdList = bmoccdisease.Select(p => p.Key).ToList();
            var noelist = occdiseaselist.Where(p => IdList.Contains(p.OrderNum)).ToList();
            gridControl1.DataSource = noelist;

        }
    }
}
