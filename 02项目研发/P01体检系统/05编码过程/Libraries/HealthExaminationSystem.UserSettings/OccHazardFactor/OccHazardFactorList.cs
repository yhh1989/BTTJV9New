using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccHazardFactor
{
    public partial class OccHazardFactorList : UserBaseForm
        
   {    //public CreateOrUpdateHazardFactorDto _Model { get; private set; }
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;
        public List<int> indexls = new List<int>();
        public OccHazardFactorList()
        {
            InitializeComponent();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
        }

        private void OccHazardFactorList_Load(object sender, EventArgs e)
        {
            InitializeData();
        }
        private void InitializeData()
        {

            OutOccHazardFactorDto show = new OutOccHazardFactorDto();
            show.IsActive = 3;
            var data = _OccHazardFactorAppService.ShowOccHazardFactor(show);
            comboBoxEdit2.Properties.DataSource = data;
            gridControl1.DataSource = data;
            //一级分类
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.HazardHactors.ToString();
            var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            comboBoxEdit1.Properties.DataSource = dl;

            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 1, Name = "启用" });
            list.Add(new EnumModel { Id = 0, Name = "停用" });
            editActive.Properties.DataSource = list;
            gridView1.Columns[gridColumn5.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[gridColumn5.FieldName].DisplayFormat.Format = new CustomFormatter(FormatEnable);

        }
        private string FormatEnable(object arg)
        {
            try
            {
                if ((int)arg == 1)
                {
                    return "启用";
                }
                else
                {
                    return "停用";
                }
            }
            catch
            {
                return "停用";
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Add();
            using (var frm = new OccHazardFactorEdit())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.isadd == true)
                    {
                        simpleButton2.PerformClick();
                    }
                    else
                    {
                        InitializeData();
                    }
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn6);
            var old = gridView1.GetFocusedRow() as OutOccHazardFactorDto;
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
                using (var frm = new OccHazardFactorEdit((Guid)id,old))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //simpleButton5.PerformClick();                       


                        ModelHelper.CustomMapTo(frm._Model, old);
                        gridControl1.RefreshDataSource();

                    }
                }
        }
        
       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OutOccHazardFactorDto show = new OutOccHazardFactorDto();
            if (comboBoxEdit1.EditValue != null)
            {
                show.ParentId = (Guid)comboBoxEdit1.EditValue;
            }
            if (!string.IsNullOrWhiteSpace(comboBoxEdit2.Text.Trim()))
                show.Text = comboBoxEdit2.Text.Trim();
            if (editActive.EditValue != null)
                show.IsActive = (int)editActive.EditValue;
            show.IsActive = 3;
            try
            {
                indexls.Clear();
                var result = _OccHazardFactorAppService.ShowOccHazardFactor(show);
                gridControl1.DataSource = null;
                gridControl1.DataSource = result;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn6);
            if (id == null)
            {
                ShowMessageBoxError("尚未选定任何项目！");
                return;
            }
            var name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn3);

            if (XtraMessageBox.Show($"确定要删除项目 {name} 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                try
                {

                    // simpleButtonQuery.PerformClick();
                    var dto = gridControl1.GetFocusedRowDto<OutOccHazardFactorDto>();
                    AutoLoading(() =>
                    {
                        _OccHazardFactorAppService.Del(new EntityDto<Guid>
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dto = gridView1.GetRow(e.FocusedRowHandle) as CreateOrUpdateHazardFactorDto;
        }

        private void editActive_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
               

            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {

            List<OutOccHazardFactorDto> occdiseaselist = gridControl1.DataSource as List<OutOccHazardFactorDto>;
            var bmoccdisease = occdiseaselist.GroupBy(p => p.OrderNum).Where(p => p.Count() > 1).ToList();
            var IdList = bmoccdisease.Select(p => p.Key).ToList();
            var noelist = occdiseaselist.Where(p => IdList.Contains(p.OrderNum)).ToList();

            gridControl1.DataSource = noelist;

            

        }
    }
}
