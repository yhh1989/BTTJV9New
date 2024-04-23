using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonFormat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using DevExpress.XtraEditors;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposalNew
{
    public partial class OccBasicDictionaryList : UserBaseForm        
    {
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        public static string ParentIds;
        public static string keys;
        public static string Names;
        public List<int> indexls = new List<int>();
        public OccBasicDictionaryList()
        {
            InitializeComponent();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
        }

        private void OccBasicDictionaryList_Load(object sender, EventArgs e)
        {
            var inputs = ZYBDictionaryTypels.getZYBDictionaryTypels();
            treeView1.DataSource = inputs;                   
            //ShowOccDictionary show = new ShowOccDictionary();
            //var data = _OccDisProposalNewAppService.GetAll(show);
            //gridControl1.DataSource = data;
            InitializeData();
        }
        private void InitializeData()
        {                   
            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 1, Name = "启用" });
            list.Add(new EnumModel { Id = 0, Name = "停用" });
            editActive.Properties.DataSource = list;
            gridView1.Columns[gridColumn4.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[gridColumn4.FieldName].DisplayFormat.Format = new CustomFormatter(FormatEnable);

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
            keys = Convert.ToString(treeView1.FocusedNode.GetValue("Key"));
            Names = Convert.ToString(treeView1.FocusedNode.GetValue("Value"));
            ParentIds = Convert.ToString(treeView1.FocusedNode.GetValue("ParentId"));
            var data = gridView1.GetFocusedRow() as ShowOccDictionary;            
            using (var frm = new OccBasicDictionaryEdit())
            {
               
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    simpleButton1.PerformClick();
                }
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            keys = Convert.ToString(treeView1.FocusedNode.GetValue("Key"));
            Names = Convert.ToString(treeView1.FocusedNode.GetValue("Value"));
            ParentIds = Convert.ToString(treeView1.FocusedNode.GetValue("ParentId"));
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn5);
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
                using (var frm = new OccBasicDictionaryEdit((Guid)id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        simpleButton1.PerformClick();
                    }
                }
        }     

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn5);
            if (id == null)
            {
                ShowMessageBoxError("尚未选定任何项目！");
                return;
            }
            var name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn2);

            if (XtraMessageBox.Show($"确定要删除项目 {name} 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                try
                {

                    // simpleButtonQuery.PerformClick();
                    var dto = gridControl1.GetFocusedRowDto<ShowOccDictionary>();
                    AutoLoading(() =>
                    {
                        _OccDisProposalNewAppService.Del(new EntityDto<Guid>
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

        private void treeView1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            AutoLoading(() =>
            {
                try
                {
                    var showOccDictionary = new ShowOccDictionary();
                    var Value = treeView1.FocusedNode.GetValue("Key").ToString();
                    var ParentIds = Convert.ToString(treeView1.FocusedNode.GetValue("ParentId"));
                    var Name = treeView1.FocusedNode.GetValue("Value").ToString();
                    if (ParentIds != "")
                {
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Name = ParentIds;
                    var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                    comboBoxTop.Properties.DataSource = dl;
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    gridColumn6.Visible = true;
                    }
                else
                {
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        gridColumn6.Visible = false ;
                    }
                    if (Value != null)
                        showOccDictionary.Type = Value;
                    showOccDictionary.IsActive = 3;
                try
                {
                    indexls.Clear();
                    var result = _OccDisProposalNewAppService.GetAll(showOccDictionary);
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = result;                                                           
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
            });
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var Value = treeView1.FocusedNode.GetValue("Key").ToString();
            var ParentIds = Convert.ToString(treeView1.FocusedNode.GetValue("ParentId"));

            var showOccDictionary = new ShowOccDictionary();
            if (!string.IsNullOrWhiteSpace(textEdit1.Text.Trim()))
                showOccDictionary.Text = textEdit1.Text.Trim();
            if (editActive.EditValue != null)
                showOccDictionary.IsActive = (int)editActive.EditValue;
            if (Value != null)
                showOccDictionary.Type = Value;
            showOccDictionary.IsActive = 3;
            try
            {
                indexls.Clear();
                var result = _OccDisProposalNewAppService.GetAll(showOccDictionary);
                gridControl1.DataSource = null;

                gridControl1.DataSource = result;

                
               
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        private void editActive_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                editActive.EditValue = null;
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {

            List<ShowOccDictionary> occdiseaselist = gridControl1.DataSource as List<ShowOccDictionary>;
            var bmoccdisease = occdiseaselist.Where(p => p.code != null).GroupBy(p => p.code).Where(p => p.Count() > 1).ToList();             
            var IdList = bmoccdisease.Select(p=>p.Key).ToList();
            var noelist = occdiseaselist.Where(p => IdList.Contains(p.code)).ToList();
            gridControl1.DataSource = noelist;
         
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            keys = Convert.ToString(treeView1.FocusedNode.GetValue("Key"));
            Names = Convert.ToString(treeView1.FocusedNode.GetValue("Value"));
            ParentIds = Convert.ToString(treeView1.FocusedNode.GetValue("ParentId"));
            var data = gridView1.GetFocusedRow() as ShowOccDictionary;
            using (var frm = new ExcelIntput())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    simpleButton1.PerformClick();
                }
            }
        }
    }
    }
