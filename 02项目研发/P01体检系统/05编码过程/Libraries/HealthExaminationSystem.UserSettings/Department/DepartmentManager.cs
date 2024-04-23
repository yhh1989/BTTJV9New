using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Department
{
    public partial class DepartmentManager : UserBaseForm
    {
        private readonly Application.ItemGroup.IItemGroupAppService groupservice = new ItemGroupAppService();
        private readonly IDepartmentAppService _departmentAppService;
        public List<int> indexls = new List<int>();
        public DepartmentManager()
        {
            InitializeComponent();

            _departmentAppService = new DepartmentAppService();
            
            repositoryItemLookUpEditSex.DataSource = SexHelper.GetSexModelsForItemInfo();
        }

        
        private void LoadData()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }

            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControl.DataSource = null;
            repositoryItemLookUpEdit1.DataSource= DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.LargeDepatType);
            try
            {
                var output = _departmentAppService.QueryDepartment(new PageInputDto<TbmDepartmentDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new TbmDepartmentDto
                    {
                        Name = textEdit1.Text.Trim(),
                        IsActive = !checkEdit1.Checked
                    }
                });
                if (output != null)
                {
                    //gridControl.DataSource = output;
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator1);
                    gridControl.DataSource = output.Result;
                }
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                    {
                        splashScreenManager.CloseWaitForm();
                    }
                }
            }
        }

        private void DepartmentSetting_Load(object sender, EventArgs e)
        {
           // gridViewDepartment.OptionsView.ShowIndicator = false;//不显示指示器
            //gridViewDepartment.OptionsBehavior.ReadOnly = false;
            //gridViewDepartment.OptionsBehavior.Editable = false;
            LoadData();
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        private void simpleButtonCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new DepartmentEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    simpleButtonQuery.PerformClick();
                }
            }
        }

        //修改页面
        private void simpleButtonEdit_Click(object sender, EventArgs e)
        {
            var id = gridViewDepartment.GetRowCellValue(gridViewDepartment.FocusedRowHandle, gridColumnDepartmentId);
            var dto = gridControl.GetFocusedRowDto<TbmDepartmentDto>();
            using (var frm = new DepartmentEditor((Guid)id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //  simpleButtonQuery.PerformClick();
                    ModelHelper.CustomMapTo(frm._Model, dto);
                    gridControl.RefreshDataSource();
                }
            }
        }

        //删除
        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            var id = gridViewDepartment.GetRowCellValue(gridViewDepartment.FocusedRowHandle, gridColumnDepartmentId);
            var name = gridViewDepartment.GetRowCellValue(gridViewDepartment.FocusedRowHandle, gridColumnDepartmentName);

            var question = XtraMessageBox.Show($"确定禁用科室“{name}”？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
            {
                return;
            }

            //try
            //{
            //    _departmentAppService.DeleteDepartment(new EntityDto<Guid> {Id = Guid.Parse(id.ToString())});
            //}
            //catch (UserFriendlyException ex)
            //{
            //    ShowMessageBox(ex);
            //}
            var dto = gridControl.GetFocusedRowDto<TbmDepartmentDto>();
            AutoLoading(() =>
            {
                _departmentAppService.DeleteDepartment(new EntityDto<Guid> { Id = Guid.Parse(id.ToString()) });
                gridControl.RemoveDtoListItem(dto);
            }, Variables.LoadingDelete);

            //simpleButtonQuery.PerformClick();
        }

        //查询
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            indexls.Clear();
            LoadData();
        }
        //上移
        private void simpleButtonUp_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //
                //  IBusinessService tService = WfEnv.GetProxyBusinessService();

                int dsRowIndex = this.gridViewDepartment.GetFocusedDataSourceRowIndex();
                if (this.gridViewDepartment.IsFirstRow == true)
                {   //当前节点是第一个节点,不用上移
                    return;
                }
                var role = gridViewDepartment.GetFocusedRow() as TbmDepartmentDto;
                if (role != null)
                {


                    //int tIndex = dsRowIndex - 1;
                    //int tRowHandle = this.gridViewDepartment.GetRowHandle(tIndex);
                    //TbmDepartmentDto t_role = this.gridViewDepartment.GetRow(tRowHandle) as TbmDepartmentDto;
                    //t_role.OrderNum = t_role.OrderNum + 1;
                   
                    //
                    //显示
                    List<TbmDepartmentDto> roleList = this.gridControl.DataSource as List<TbmDepartmentDto>;
                    int selectIndex = roleList.IndexOf(role);
                    roleList[selectIndex] = roleList[selectIndex - 1];
                    roleList[selectIndex - 1] = role;
                    //
                    this.gridControl.DataSource = roleList;
                    this.gridControl.RefreshDataSource();
                    this.gridViewDepartment.MovePrev();
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
        //下移
        private void simpleButtonDown_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int dsRowIndex = this.gridViewDepartment.GetFocusedDataSourceRowIndex();
                if (this.gridViewDepartment.IsLastRow == true)
                {   //当前节点是第一个节点,不用下移
                    return;
                }
                var role = gridViewDepartment.GetFocusedRow() as TbmDepartmentDto;
                if (role != null)
                {

                    //int tIndex = dsRowIndex + 1;
                    //int tRowHandle = this.gridViewDepartment.GetRowHandle(tIndex);
                    //TbmDepartmentDto t_role = this.gridViewDepartment.GetRow(tRowHandle) as TbmDepartmentDto;

                    //显示
                    List<TbmDepartmentDto> roleList = this.gridControl.DataSource as List<TbmDepartmentDto>;
                    int selectIndex = roleList.IndexOf(role);
                    roleList[selectIndex] = roleList[selectIndex + 1];
                    roleList[selectIndex + 1] = role;
                    //
                    this.gridControl.DataSource = roleList;
                    this.gridControl.RefreshDataSource();
                    this.gridViewDepartment.MoveNext();
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
                TbmDepartmentDto Department = this.gridViewDepartment.GetRow(row) as TbmDepartmentDto;
                if (Department != null)
                {
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Id = Department.Id;
                    chargeBM.Name = Department.OrderNum.ToString();
                    _departmentAppService.UpdateOrder(chargeBM);
                }
            }
            indexls.Clear();
            simpleButtonQuery.PerformClick();
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

        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            LoadData();
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEdit1.EditValue != null)
            {
                LoadData();
            }
        }

        private void gridViewDepartment_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var id = gridViewDepartment.GetRowCellValue(gridViewDepartment.FocusedRowHandle, gridColumnDepartmentId);
                using (var frm = new DepartmentEditor((Guid)id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        simpleButtonQuery.PerformClick();
                    }
                }
            }
        }

        private void butPX_Click(object sender, EventArgs e)
        {
            int num = 30 * (CurrentPage - 1);
            for (var i = 0; i < gridViewDepartment.RowCount; i++)
            {
                var values = gridViewDepartment.GetRowCellValue(i, gridColumnDepartmentOrderNum);
                if (values == null)
                {
                    values = "0";
                }
                if (values.ToString().Trim() != (num + i).ToString())
                {
                    gridViewDepartment.SetRowCellValue(i, gridColumnDepartmentOrderNum, (num + i).ToString());
                }
            }
        }

        private void gridViewDepartment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!indexls.Contains(e.RowHandle))
            {
                indexls.Add(e.RowHandle);
            }
        }

        private void btml_Click(object sender, EventArgs e)
        {
            var id = gridViewDepartment.GetRowCellValue(gridViewDepartment.FocusedRowHandle, gridColumnDepartmentId);
            if (id == null)
            {
                MessageBox.Show("请选择建立抹零项的科室！");
                return;
            }
            EntityDto<Guid> input = new EntityDto<Guid>();
            input.Id =Guid.Parse(id.ToString());
            groupservice.adMLXM(input);
            MessageBox.Show("添加成功！");


        }
    }
}