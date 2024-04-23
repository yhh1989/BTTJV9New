using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class DepartmentCustomer : UserBaseForm
    {
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly IDoctorStationAppService _doctorStation;
        public DepartmentCustomer()
        {
            InitializeComponent();
            _doctorStation = new DoctorStationAppService();
        }
        /// <summary>
        /// 自定义事件，和医生站实时交互
        /// </summary>
        public event EventHandler<DepartmentCustomerEventArgs> CustomerChanged;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentCustomer_Load(object sender, EventArgs e)
        {
            customGridLookUpEditClient.Properties.DataSource = _doctorStation.GetSearchClientInfoDto(new DeparmentCustomerSearch());
            customGridLookUpEdit1View.BestFitColumns();
            customGridLookUpEditDeparment.Properties.DataSource = CurrentUser.TbmDepartments.OrderBy(o => o.OrderNum).ToList();
            //体检状态
            lookUpEditCheckSate.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            lookUpEditCheckSate.ItemIndex = 0;
            //总检状态
            lookUpEditSummSate.Properties.DataSource = SummSateHelper.GetSelectList();
            lookUpEditSummSate.ItemIndex = 0;
            dateEditLoginDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateEditLoginDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 勾选结论默认勾选上我的患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditIsOwnSumm_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditIsOwnSumm.Checked)
            {
                checkEditIsOwn.Checked = true;
                checkEditNotComplete.Checked = false;
            }
        }
        /// <summary>
        /// 取消勾选我的默认把未结论取消勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditIsOwn_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkEditIsOwn.Checked)
            {
                checkEditIsOwnSumm.Checked = false;
            }
            else
            {
                checkEditNotComplete.Checked = false;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }
        public void SearchCustomer()
        {
            DeparmentCustomerSearch Search = new DeparmentCustomerSearch();
            Search.DepartmentBM = new List<Guid>();
            //体检号/姓名
            if (!string.IsNullOrWhiteSpace(textEditCustomerBMOrName.Text))
            {
                Search.CustomerBMOrName = textEditCustomerBMOrName.Text;
            }
            //登记时间
            if (checkEditLoginDate.Checked)
            {
                if (!string.IsNullOrWhiteSpace(dateEditLoginDateStart.Text))

                    Search.StartTime = DateTime.Parse(dateEditLoginDateStart.Text);
                if (!string.IsNullOrWhiteSpace(dateEditLoginDateEnd.Text))
                    Search.EndTime = DateTime.Parse(dateEditLoginDateEnd.Text).AddDays(1);
            }
            //单位
            if (customGridLookUpEditClient.EditValue != null && !string.IsNullOrWhiteSpace(customGridLookUpEditClient.EditValue.ToString()))
            {
                Search.ClientInfoId = Guid.Parse(customGridLookUpEditClient.EditValue?.ToString());
            }
            //科室
            if (!string.IsNullOrWhiteSpace(customGridLookUpEditDeparment.EditValue?.ToString()))
            {
                Search.DepartmentBM.Add(Guid.Parse(customGridLookUpEditDeparment.EditValue?.ToString()));
            }
            else
            {
                foreach (var itemDeparment in CurrentUser.TbmDepartments.ToList())
                {
                    Search.DepartmentBM.Add(itemDeparment.Id);
                }
                //alertControl.Show(this, "温馨提示", "科室必须选择!");
                //return;
            }
            //我的患者
            if (checkEditIsOwn.Checked)
            {
                Search.IsOwn = checkEditIsOwn.Checked;
                Search.OwnId = CurrentUser.Id;
            }
            //未生成小结
            Search.IsOwnSumm = checkEditIsOwnSumm.Checked;
            //体检状态
            if (lookUpEditCheckSate.EditValue.ToString() != "0")
            {
                Search.CheckSate = int.Parse(lookUpEditCheckSate.EditValue.ToString());
            }
            //总检状态
            if (lookUpEditCheckSate.EditValue.ToString() != "0")
            {
                Search.SummSate = int.Parse(lookUpEditSummSate.EditValue.ToString());
            }
            //查询当前科室存在为完成的患者
            Search.NotComplete = checkEditNotComplete.Checked;
            //体检号/姓名，单位，登记时间如果都不选会查询很多数据，所以提示一下！
            if (checkEditLoginDate.Checked == false &&
                (customGridLookUpEditClient.EditValue == null ||
                string.IsNullOrWhiteSpace(customGridLookUpEditClient.EditValue.ToString()))
                && string.IsNullOrWhiteSpace(textEditCustomerBMOrName.Text))
            {
                var question = XtraMessageBox.Show("当前检索条件查询数据量过大是否继续？", "询问",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                //判断是否继续
                if (question == DialogResult.Yes)
                {
                    AutoLoading(() =>
                    {
                        gridControlCustomer.DataSource = _doctorStation.GetDeparmentCustomerRegs(Search).OrderByDescending(o=>o.LoginDate);
                    }, Variables.LoadingForCloud);
                }
            }
            else
            {
                AutoLoading(() =>
                {
                    gridControlCustomer.DataSource = _doctorStation.GetDeparmentCustomerRegs(Search).OrderByDescending(o => o.LoginDate);
                }, Variables.LoadingForCloud);
            }
        }
        /// <summary>
        /// 选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewCustomer_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            if (e.Clicks == 2)
            {
                var customerBM = gridViewCustomer.GetFocusedRowCellValue(gridColumnCustomerBM.FieldName);

                if (customerBM != null)
                {
                    CustomerChanged?.Invoke(gridViewCustomer, new DepartmentCustomerEventArgs(customerBM.ToString()));
                }
            }
        }
        /// <summary>
        /// 查询未作项目人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditNotComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditNotComplete.Checked)
            {
                checkEditIsOwnSumm.Checked = false;
                checkEditIsOwn.Checked = false;
            }
        }
    }
}