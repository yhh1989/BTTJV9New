using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraGrid.Views.Base;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using System.Collections.Generic;
using System.Data;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Abp.Application.Services.Dto;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Department
{
    public partial class Diagnosis : UserBaseForm
    {
        private readonly Guid _id;
        private string moneyTypes = "";
        private IDiagnosisAppService service = null;
        public IDiagnosisAppService _Service
        {
            get
            {
                if (service == null) service = new DiagnosisAppService();
                return service;
            }
        }

        public Diagnosis()
        {
            InitializeComponent();
            Guid id = new Guid();
            _id = id;
            LoadData();
            

        }
        //编辑调用
        public Diagnosis(Guid id) 
        {
            InitializeComponent();
            _id = id;
            LoadData();
            

        }

        private void LoadData()
        {
            var xingbieList = SexHelper.GetSexModelsForItemInfo();
            lue_xingbie.Properties.DataSource = xingbieList;
            lue_xingbie.EditValue = (int)Sex.GenderNotSpecified;

            ItemInfoGroupDto itemInfo = new ItemInfoGroupDto();
            itemInfo.ItemGroupName = textEditCombination.Text.Trim();
            var result = _Service.QueryInfoGroup(itemInfo);
            gcWait.DataSource = result;
            teStandard.Enabled = false;
            // teInstrument.Enabled = false;
            teMax.Enabled = false;
            temin.Enabled = false;
            simpleButton5.Enabled = false;
            if (_id != Guid.Empty)
            {
                try
                {
                    var dept = _Service.GetById(new EntityDto<Guid> { Id = _id });
                    teName.Text = dept.RuleName;
                    teRemarks.Text = dept.Remarks;
                    teConclusion.Text = dept.Conclusion;
                    teNum.Text = dept.OrderNum;

                    var diagnosisDatals = dept.DiagnosisDatals;
                    dept.DiagnosisDatals = null;
                    List<ItemInfoDiagnosisDto> items = new List<ItemInfoDiagnosisDto>();

                    //var i = 0;
                    foreach (var item in diagnosisDatals)
                    {
                        ItemInfoDiagnosisDto tbm = new ItemInfoDiagnosisDto();
                        tbm.Name = item.ItemInfo.Name;
                        tbm.Id = item.ItemInfo.Id;
                        tbm.moneyType = item.ItemInfo.moneyType;
                        tbm.ItemStandard = item.ItemStandard;
                        tbm.InstrumentId = item.InstrumentId;
                        tbm.maxValue = item.maxValue;
                        tbm.minValue = item.minValue;
                        tbm.ItemBM = item.ItemInfo.ItemBM;
                        tbm.Sex = item.Sex;
                        tbm.MinAge = item.MinAge;
                        tbm.MaxAge = item.MaxAge;
                        items.Add(tbm);
                    }
                    gcSelected.AddDtoListItem(items);


                }
                catch (UserFriendlyException e)
                {
                    ShowMessageBox(e);
                }
            }
        }


        #region 初始化表格
        private void GridSetting(GridView grid)
        {
            grid.OptionsView.ShowGroupPanel = false;
            grid.OptionsBehavior.AutoExpandAllGroups = true;
            grid.OptionsCustomization.AllowColumnMoving = false;
            grid.OptionsCustomization.AllowFilter = false;
            grid.OptionsCustomization.AllowGroup = false;
            grid.OptionsDetail.ShowDetailTabs = false;
        }

        private GridColumn BuildGridCol(string name, string caption, int width = 75, bool fixedWidth = false)
        {
            GridColumn col = new GridColumn();
            col.Name = name;
            col.Caption = caption;
            col.FieldName = name;
            col.Visible = true; // 是否显示，需要设置为true
            //col.VisibleIndex = 0; // 显示顺序
            col.Width = width;
            col.OptionsColumn.FixedWidth = fixedWidth;
            col.OptionsColumn.AllowEdit = false; // 禁止编辑
            return col;
        }
        #endregion
        #region 事件
        //向左添加项目
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Add();
        }
        //向右添加项目
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Del();
        }
        //保存
        private void sbOK_Click(object sender, EventArgs e)
        {

            dxErrorProvider.ClearErrors();
            try
            {
                TbmDiagnosisDto input = new TbmDiagnosisDto();

                if (_id != Guid.Empty)
                {
                    input.Id = _id;
                }
                    var RuleName = teName.Text.Trim();
                if (string.IsNullOrWhiteSpace(RuleName))
                {
                    dxErrorProvider.SetError(teName, string.Format(Variables.MandatoryTips, "名称"));
                    teName.Focus();
                    return;
                }

                var Conclusion = teConclusion.Text.Trim();
                if (string.IsNullOrWhiteSpace(Conclusion))
                {
                    dxErrorProvider.SetError(teConclusion, string.Format(Variables.MandatoryTips, "结论"));
                    teConclusion.Focus();
                    return;
                }

                input.RuleName = RuleName;//名称
                input.Remarks = teRemarks.Text.Trim();//备注
                input.Conclusion = Conclusion;//结论
                input.OrderNum = teNum.Text.Trim();//序号

                List<TbmDiagnosisDataDto> inputs = new List<TbmDiagnosisDataDto>();


                var list = gcSelected.GetDtoListDataSource<ItemInfoDiagnosisDto>();

                for (int i = 0; i < gridViewIteminfo.RowCount; i++)
                {
                    if (list[i].moneyType.ToString() != "" && list[i].moneyType.ToString() != null)
                    {
                        if (list[i].moneyType.ToString() == "1"|| list[i].moneyType.ToString() == "2")
                        {
                            moneyTypes = "2";
                        }
                        else
                        {
                            moneyTypes = "1";
                        }
                    }

                    TbmDiagnosisDataDto tbm = new TbmDiagnosisDataDto();
                    var id = list[i].Id.ToString();
                    var ItemBM = list[i].ItemBM.ToString();
                    tbm.ItemInfo = new ItemInfoDiagnosisDto { Id = Guid.Parse(id),ItemBM = ItemBM };


                    if (moneyTypes != null && moneyTypes != "")
                    {
                        tbm.ItemType = int.Parse(moneyTypes);
                    }
                    else
                    {
                        tbm.ItemType = null;
                    }
                    var ItemStandard = list[i].ItemStandard;
                    if (ItemStandard != null && ItemStandard != "")
                    {
                        tbm.ItemStandard = ItemStandard.ToString();
                    }
                    else
                    {
                        tbm.ItemStandard = "";
                    }
                    var InstrumentId = list[i].InstrumentId;
                    if (InstrumentId != null && InstrumentId != "")
                    {
                        tbm.InstrumentId = InstrumentId.ToString();
                    }
                    else
                    {
                        tbm.InstrumentId = "";
                    }
                    var minValue = list[i].minValue;
                    if (minValue != null)
                    {
                        tbm.minValue = decimal.Parse(minValue.ToString());
                    }
                    else
                    {
                        tbm.minValue = null;
                    }
                    var maxValue = list[i].maxValue;
                    if (maxValue != null)
                    {
                        tbm.maxValue = decimal.Parse(maxValue.ToString());
                    }
                    else
                    {
                        tbm.maxValue = null;
                    }
                    //性别，年龄段
                    var consex = list[i].Sex;
                    if (consex != null)
                    {
                        tbm.Sex = consex;
                    }
                    else
                    {
                        tbm.Sex = null;
                    }
                    var conminage = list[i].MinAge;
                    if (conminage != null)
                    {
                        tbm.MinAge = conminage;
                    }
                    else
                    {
                        tbm.MinAge = null;
                    }
                    var conmaxage = list[i].MaxAge;
                    if (conmaxage != null)
                    {
                        tbm.MaxAge = conmaxage;
                    }
                    else
                    {
                        tbm.MaxAge = null;
                    }
                    inputs.Add(tbm);


                }
                input.DiagnosisDatals = inputs;
                _Service.InsertDiagnosis(input);
                DialogResult = DialogResult.OK;
            }

            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }
        //确定
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var columnView = (ColumnView)gcSelected.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            var ItemStandard = teStandard.Text.Trim();
            //var InstrumentId = teInstrument.Text.Trim();
            var maxValue = teMax.Text.Trim();
            var minValue  = temin.Text.Trim();

            var zxnl = num_zxnl.Text.Trim();
            var zdnl = num_zdnl.Text.Trim();
            var xingbie = lue_xingbie.EditValue.ToString();

            if (decimal.Parse(maxValue)< decimal.Parse(minValue))
            {
                dxErrorProvider.SetError(temin, string.Format(Variables.GreaterThanTips, "下限", "上限"));
                temin.Focus();
                return;


            }

            if (ItemStandard != null && ItemStandard != "")
            {
                gridViewIteminfo.SetRowCellValue(focusedhandle, gridViewIteminfo.Columns[3], ItemStandard);
            }
            //if (InstrumentId != null && InstrumentId != "")
            //{
               // gridViewIteminfo.SetRowCellValue(focusedhandle, gridViewIteminfo.Columns[4], InstrumentId);
            //}            
            if (minValue != null && minValue != "")
            {
                gridViewIteminfo.SetRowCellValue(focusedhandle, gridViewIteminfo.Columns[6], minValue);
            }
            if (maxValue != null && maxValue != "")
            {
                gridViewIteminfo.SetRowCellValue(focusedhandle, gridViewIteminfo.Columns[5], maxValue);
            }
            //性别，年龄段
            if (zxnl != null && zxnl != "")
            {
                gridViewIteminfo.SetRowCellValue(focusedhandle, conminage, zxnl);
            }
            if (zdnl != null && zdnl != "")
            {
                gridViewIteminfo.SetRowCellValue(focusedhandle, conmaxage, zdnl);
            }
            if (xingbie != null && xingbie != "")
            {
                gridViewIteminfo.SetRowCellValue(focusedhandle, consex, xingbie);
            }

        }
        //点击表格
        private void gcSelected_Click(object sender, EventArgs e)
        {
            var columnView = (ColumnView)gcSelected.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            var moneyType = gridViewIteminfo.GetRowCellValue(focusedhandle, gridViewIteminfo.Columns[2]);
            if(moneyType ==null)
            {
                return;
            }
            if (moneyType.ToString() == "1" || moneyType.ToString() == "2")
            {
                teStandard.Enabled = false;
                //teInstrument.Enabled = false;
                teMax.Enabled = true;
                temin.Enabled = true;
                simpleButton5.Enabled = true;
            }
            else if (moneyType.ToString() == "4")
            {
                teStandard.Enabled = true;
                //teInstrument.Enabled = false;
                teMax.Enabled = true;
                temin.Enabled = true;
                simpleButton5.Enabled = true;
            }
            else
            {
                teMax.Enabled = false;
                temin.Enabled = false;
                teStandard.Enabled = true;
                //teInstrument.Enabled = true;
                simpleButton5.Enabled = true;
            }

            var ItemStandard = gridViewIteminfo.GetRowCellValue(focusedhandle, gridViewIteminfo.Columns[3]);
            if (ItemStandard != null)
            {
                teStandard.Text = ItemStandard.ToString();
            }
            else
            {
                teStandard.Text = "";
            }
            var InstrumentId = gridViewIteminfo.GetRowCellValue(focusedhandle, gridViewIteminfo.Columns[4]);
            //仪器Id
            //if (InstrumentId != null)
            //{
               // teInstrument.Text = InstrumentId.ToString();
           // }
            //else
            //{
               // teInstrument.Text = "";
            //}
            var maxValue = gridViewIteminfo.GetRowCellValue(focusedhandle, gridViewIteminfo.Columns[5]);
            if (maxValue != null)
            {
                teMax.Text = maxValue.ToString();
            }
            else
            {
                teMax.Text = "";
            }
            var minValue = gridViewIteminfo.GetRowCellValue(focusedhandle, gridViewIteminfo.Columns[6]);
            if (minValue != null)
            {
                temin.Text = minValue.ToString();
            }
            else
            {
                temin.Text = "";
            }
            //最小年龄
            var minage = gridViewIteminfo.GetRowCellValue(focusedhandle, conminage);
            if (minage != null)
            {
                num_zxnl.Text= minage.ToString();
               
            }
            else
            {
                num_zxnl.Text = "";
            }
            //最大年龄
            var maxage = gridViewIteminfo.GetRowCellValue(focusedhandle, conmaxage);
            if (maxage != null)
            {
                num_zdnl.Text = maxage.ToString();

            }
            else
            {
                num_zdnl.Text = "";
            }
            //性别
            var sex = gridViewIteminfo.GetRowCellValue(focusedhandle, consex);
            if (sex != null)
            {
                lue_xingbie.EditValue =int.Parse(sex.ToString());

            }
            else
            {
                lue_xingbie.EditValue = "";
            }
        }

        #endregion

        public void Add()
        {
            List<ItemInfoDiagnosisDto> dtos = new List<ItemInfoDiagnosisDto>();
            try
            {
                
                List<ItemInfoDiagnosisDto> list = new List<ItemInfoDiagnosisDto>();
                var gridView = gcWait.FocusedView as GridView;
                foreach (var i in gridView.GetSelectedRows())
                {
                    var dto = (ItemInfoDiagnosisDto)gridView.GetRow(i);
                    list.Add(dto);
                }
                dtos = list;
            }
            catch
            {
                var DtoInfo = gcWait.GetFocusedRowDto<ItemInfoGroupDto>();
                List<ItemInfoDiagnosisDto> list = new List<ItemInfoDiagnosisDto>();
                foreach (var i in DtoInfo.ItemInfos)
                {
                    list.Add(i);
                }
                dtos = list;

            }
            
            //var dtos = gcWait.GetSelectedRowDtos<ItemInfoDiagnosisDto>();


            if (dtos.Count() == 0) return;
            gcSelected.AddDtoListItem(dtos);
            //gcSelected.DataSource = dtos;

        }
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<ItemInfoDiagnosisDto>();
            if (dtos.Count() == 0) return;
            gcSelected.RemoveDtoListItem(dtos);
        }


        private void gridViewIteminfo_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Del();
            }
        }

        private void gridViewItemInfos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Add();
            }
        }

        private void gridViewItemGround_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Add();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Diagnosis_Load(object sender, EventArgs e)
        {

        }
    }
}