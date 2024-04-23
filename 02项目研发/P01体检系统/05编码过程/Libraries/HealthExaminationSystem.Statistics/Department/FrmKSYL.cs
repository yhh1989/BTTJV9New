using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Department
{
    public partial class FrmKSYL : UserBaseForm
    {
        /// <summary>
        /// 获取时间
        /// </summary>
        private readonly CommonAppService _commonProxy = new CommonAppService();

        ///// <summary>
        ///// 记录当前科室Id  防止重复点击
        ///// </summary>
        //private Guid _currentDepartmentId;

        /// <summary>
        /// 科室查询类
        /// </summary>
        private readonly DepartmentAppService _departProxy = new DepartmentAppService();

        private readonly DoctorStationAppService _proxy = new DoctorStationAppService();

        //private List<ClientRegDto> clientRegs; //单位及分组字典

        private ICustomerAppService customerSvr; //体检预约

        public FrmKSYL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                btnSearch.Enabled = false; //防止重复点击加载卡死现象
                closeWait = true;
            }

            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                var query = new QueryClass();

                //科室
                var departmentIds = new List<Guid>();
                var arrIds = chkcmbDepartment.Properties.GetCheckedItems().ToString().Split(',');
                if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
                    foreach (var item in arrIds)
                        departmentIds.Add(new Guid(item));

                //BaseCheckedListBoxControl.CheckedItemCollection checkboxs = chkcmbDepartment..CheckedItems;
                //foreach (TbmDepartmentDto item in checkboxs)
                //{
                //    departmentIds.Add(item.Id);
                //    // string name = item;
                //}
                query.DepartmentBMList = departmentIds;

                //时间

                if (dtpStart.EditValue != null)
                    query.LastModificationTimeBign = Convert.ToDateTime(dtpStart.EditValue.ToString());
                if (dtpEnd.EditValue != null)
                    query.LastModificationTimeEnd = Convert.ToDateTime(dtpEnd.EditValue.ToString());

                gcItem.DataSource = _proxy.KSYLStatistics(query);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    splashScreenManager.CloseWaitForm();
                    btnSearch.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmKSGZL_Load(object sender, EventArgs e)
        {
            customerSvr = new CustomerAppService();
            Init();
            dtpEnd.DateTime = _commonProxy.GetDateTimeNow().Now;
        }

        /// <summary>
        /// 初始化窗体控件数据
        /// </summary>
        private void Init()
        {
            var departmentList = _departProxy.GetAll();
            chkcmbDepartment.Properties.DataSource = departmentList;
            chkcmbDepartment.Properties.DisplayMember = "Name";
            chkcmbDepartment.Properties.ValueMember = "Id";
        }

        /*

        /// <summary>
        /// 左侧查询dgv行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvResult_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
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
                SearchKSGZLStatisticsDto focuseRow = gvResult.GetFocusedRow() as SearchKSGZLStatisticsDto;
                
                if (focuseRow != null)
                {
                    //防止重复点击处理
                    if (focuseRow.Department_Id == _currentDepartmentId) return;

                    QueryClass query = new QueryClass();
                    if (dtpStart.EditValue != null)
                        query.LastModificationTimeBign = Convert.ToDateTime(dtpStart.EditValue.ToString());
                    if (dtpEnd.EditValue != null)
                        query.LastModificationTimeEnd = Convert.ToDateTime(dtpEnd.EditValue.ToString());

                    List<Guid> departmentIds = new List<Guid>();//科室id
                    departmentIds.Add((focuseRow.Department_Id));
                    query.DepartmentBMList = departmentIds;
                    gcItem.DataSource = _proxy.KSGZLItemStatistics(query);

                    //防止重复点击处理
                    _currentDepartmentId = focuseRow.Department_Id;
                }
            }
            catch (UserFriendlyException ex)
            {
                XtraMessageBox.Show(ex.Message, ex.Description, ex.Buttons, ex.Icon);
            }
            finally
            {
                if (closeWait)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }
        }*/
        /// <summary>
        /// 项目工作量结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportItemResult_Click(object sender, EventArgs e)
        {
            gvItem.CustomExport("项目结果");
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvItem_CellMerge(object sender, CellMergeEventArgs e)
        {
        }

        /// <summary>
        /// 科室绩效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKSJX_Click(object sender, EventArgs e)
        {
        }
    }
}