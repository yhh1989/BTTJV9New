using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using System;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System.Collections.Generic;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System.Linq;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemGroup
{
    public partial class ItemGroupList : UserBaseForm
    {
        private readonly IItemGroupAppService service = new ItemGroupAppService();
        public List<int> indexls = new List<int>();
        public ItemGroupList()
        {
            InitializeComponent();

            lueDepartment.SetClearButton();
            lueSex.SetClearButton();

            gvItemGroups.Columns[gvItemGroupsISSFItemGroup.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsISSFItemGroup.FieldName].DisplayFormat.Format = new CustomFormatter(ISSFItemGroupFormatter);

            gvItemGroups.Columns[gvItemGroupsBarState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsBarState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsPrivacyState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsPrivacyState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsDrawState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsDrawState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsMealState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsMealState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsWomenState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsWomenState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsBreakfast.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsBreakfast.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsOutgoingState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsOutgoingState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsVoluntaryState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsVoluntaryState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvItemGroups.Columns[gvItemGroupsAutoVIP.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsAutoVIP.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);

            gvItemGroups.Columns[gvItemGroupsSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            gvItemGroups.Columns[gvItemGroupsSpecimenType.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemGroups.Columns[gvItemGroupsSpecimenType.FieldName].DisplayFormat.Format = new CustomFormatter((obj) => CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.SpecimenType, obj));

            //gvItemGroupsDepartmentName.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            //gvItemGroups.CustomColumnSort += GridViewGroupDepartmentSort_CustomColumnSort;
            //gvItemInfosDepartmentName.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            //gvItemInfos.CustomColumnSort += GridViewGroupDepartmentSort_CustomColumnSort;

        }

        private string ISSFItemGroupFormatter(object obj)
        {
            return (int.TryParse(obj?.ToString(), out int val) && val == 0) ? "是" : "否";
        }

        // gv.DataController.AllowIEnumerableDetails = true;

        #region 事件
        private void ItemGroup_Load(object sender, EventArgs e)
        {
            LoadControlData();

            Reload();
        }
        private void sbReload_Click(object sender, EventArgs e)
        {
            indexls.Clear();
            Reload();

        }
        private void sbReset_Click(object sender, EventArgs e)
        {
            indexls.Clear();
            teQueryText.EditValue = null;
            lueDepartment.EditValue = null;
            lueSex.EditValue = null;
        }
        private void sbAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void sbEdit_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullItemGroupDto>();
            Edit(dto);
        }
        private void sbDel_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullItemGroupDto>();
            Del(dto);
        }
        private void gvItemGroups_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var dto = (FullItemGroupDto)gvItemGroups.GetRow(e.RowHandle);
                Edit(dto);
            }
        }
        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }

        #endregion

        #region 处理
        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void LoadControlData()
        {
            AutoLoading(() =>
            {
                lueSex.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
                lueDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments().Where(
                    o => o.IsActive==false).ToList();
                var list = new List<EnumModel>();
                list.Add(new EnumModel { Id = 1, Name = "启用" });
                list.Add(new EnumModel { Id = 0, Name = "未启用" });
                editActive.Properties.DataSource = list;
            });            

                repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.InspectionType);


        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Reload()
        {
            AutoLoading(() =>
            {
                //var input = new SearchItemGroupDto
                //{
                //    QueryText = teQueryText.Text.Trim(),
                //    DepartmentId = lueDepartment.EditValue == null ? Guid.Empty : (Guid)lueDepartment.EditValue,
                //    Sex = (int?)lueSex.EditValue,
                //};
                //var output = service.QueryFulls(input); // 11s
                //gridControl.DataSource = output;
                //gvItemGroups.BestFitColumns();

                var input = new PageInputDto<SearchItemGroupDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new SearchItemGroupDto
                    {
                        QueryText = teQueryText.Text.Trim(),
                        DepartmentId = lueDepartment.EditValue == null ? Guid.Empty : (Guid)lueDepartment.EditValue,
                        Sex = (int?)lueSex.EditValue,
                       IsActive=(int?)editActive.EditValue
                    }
                    
                };
                var output = service.PageFulls(input);
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator);
                gridControl.DataSource = output.Result;
                gvItemGroups.BestFitColumns();
            });
        }
        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
        {
            var data = gvItemGroups.GetFocusedRow() as FullItemGroupDto;
            ItemGroupEditor frm = new ItemGroupEditor();
            if (data != null)
            {
                frm.DepartmentId = data.DepartmentId;
            }
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridControl.AddDtoListItem(frm._Model);
                gridControl.RefreshDataSource();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        public void Edit(FullItemGroupDto dto)
        {
            if(dto == null)
            {
                ShowMessageBoxWarning("请选择项目组合。");
                return;
            }
           
            var temp = ModelHelper.DeepCloneByJson(dto);
            ItemGroupEditor frm = new ItemGroupEditor(temp);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ModelHelper.CustomMapTo(frm._Model, dto);
                gridControl.RefreshDataSource();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        public void Del(FullItemGroupDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择要删除的项目组合。");
                return;
            }
            var question = XtraMessageBox.Show("是否删除？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
            {
                return;
            }

            AutoLoading(() =>
            {
                service.Del(new EntityDto<Guid> { Id = dto.Id });
                gridControl.RemoveDtoListItem(dto);
            }, Variables.LoadingDelete);
        }
        #endregion

        private void editActive_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Kind== DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                editActive.EditValue = null;
            }
        }

        private void butPX_Click(object sender, EventArgs e)
        {
            int num = 30 * (CurrentPage - 1);
            var grouplist = GetGridViewFilteredAndSortedData(gvItemGroups);


            for (var i = 0; i < grouplist.Count; i++)
            {
                FullItemGroupDto gridRow = (FullItemGroupDto)grouplist[i];
                if (gridRow.OrderNum == null)
                {
                    gridRow.OrderNum = 0;
                }
                if (gridRow.OrderNum != i)
                {
                    gridRow.OrderNum= num + i;
                    //记录更新的索引
                    for(int rr = 0; rr < gvItemGroups.RowCount; rr++)
                    {
                        FullItemGroupDto fullItemGroupDto= gvItemGroups.GetRow(rr) as FullItemGroupDto;
                        if (fullItemGroupDto != null && fullItemGroupDto.Id== gridRow.Id)
                        {
                            if (!indexls.Contains(rr))
                            {
                                indexls.Add(rr);
                            }
                        }
                    }
                }
                

                //FullItemGroupDto t_role = this.gvItemGroups.GetRow(i) as FullItemGroupDto;
                //var values = gvItemGroups.GetRowCellValue(index, gvItemOrderNum);

                //if (values == null)
                //{
                //    values = "0";
                //}
                //if (values.ToString().Trim() != (num + i).ToString())
                //{
                //    var groupname = gvItemGroups.GetRowCellValue(index, gvItemGroupsItemGroupName);
                //    gvItemGroups.SetRowCellValue(index, gvItemOrderNum, (num + i).ToString());

                //}
            }
            this.gridControl.RefreshDataSource();
        }

        private void simpleButtonUp_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;               
                int dsRowIndex = this.gvItemGroups.GetFocusedDataSourceRowIndex();
                if (this.gvItemGroups.IsFirstRow == true)
                {   //当前节点是第一个节点,不用上移
                    return;
                }
                var role = gvItemGroups.GetFocusedRow() as FullItemGroupDto;
                if (role != null)
                {
                    //var roleList = GetGridViewFilteredAndSortedData(gvItemGroups);

                    List<FullItemGroupDto> roleList = this.gridControl.DataSource as List<FullItemGroupDto>;
                   
                    int selectIndex = roleList.IndexOf(role);
                    if (selectIndex == 0)
                    {
                        return;
                    }
                    //判断如果下面的上面不是同一个科室则不能移动
                    if (roleList[selectIndex -1].DepartmentId != roleList[selectIndex].DepartmentId)
                    {
                        return;
                    }
                    roleList[selectIndex] = roleList[selectIndex - 1];
                    roleList[selectIndex - 1] = role;
                    this.gridControl.DataSource = roleList;

                    this.gridControl.RefreshDataSource();
                    this.gvItemGroups.MovePrev();
                    butPX.PerformClick();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void simpleButtonDown_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int dsRowIndex = this.gvItemGroups.GetFocusedDataSourceRowIndex();
                if (this.gvItemGroups.IsLastRow == true)
                {   //当前节点是第一个节点,不用下移
                    return;
                }
                var role = gvItemGroups.GetFocusedRow() as FullItemGroupDto;
                if (role != null)
                {

                    //int tIndex = dsRowIndex + 1;
                    //int tRowHandle = this.gvItemGroups.GetRowHandle(tIndex);
                    //TbmDepartmentDto t_role = this.gvItemGroups.GetRow(tRowHandle) as TbmDepartmentDto;

                    //显示
                    List<FullItemGroupDto> roleList = this.gridControl.DataSource as List<FullItemGroupDto>;
                    //var roleList = GetGridViewFilteredAndSortedData(gvItemGroups);

                    int selectIndex = roleList.IndexOf(role);
                    if (selectIndex == roleList.Count - 1)
                    {
                        return;
                    }
                    //判断如果下面的不是同一个科室则不能移动
                    if (roleList[selectIndex + 1].DepartmentId != roleList[selectIndex].DepartmentId)
                    {
                        return;
                    }
                        roleList[selectIndex] = roleList[selectIndex + 1];
                    roleList[selectIndex + 1] = role;
                    //
                    this.gridControl.DataSource = roleList;
                    this.gridControl.RefreshDataSource();
                    this.gvItemGroups.MoveNext();
                    butPX.PerformClick();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //
            }
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                foreach (int row in indexls)
                {
                    FullItemGroupDto Department = this.gvItemGroups.GetRow(row) as FullItemGroupDto;
                    if (Department != null)
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = Department.Id;
                        chargeBM.Name = Department.OrderNum.ToString();

                        service.UpdateOrder(chargeBM);
                    }
                }
                indexls.Clear();
                sbReload.PerformClick();
            }
            catch (UserFriendlyException ex)
            {

                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
                ShowMessageBox(ex);
                return;
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
        public System.Collections.IList GetGridViewFilteredAndSortedData(DevExpress.XtraGrid.Views.Grid.GridView view)
         {
             return view.DataController.GetAllFilteredAndSortedRows();
         }

        private void gvItemGroups_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!indexls.Contains(e.RowHandle))
            {
                indexls.Add(e.RowHandle);
            }
        }
    }
}
