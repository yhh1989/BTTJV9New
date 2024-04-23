using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class InspectionReturn : UserBaseForm
    {
        public InspectionReturn(TjlCustomerRegForInspectionTotalDto tjlCustomerRegDto, List<CustomerRegInspectItemDto> CustomerRegItemList)
        {
            _tjlCustomerRegDto = tjlCustomerRegDto;
            _CustomerRegItemList = CustomerRegItemList;
            InitializeComponent();
            _inspectionTotalService = new InspectionTotalAppService();
        }
        //总检
        private readonly IInspectionTotalAppService _inspectionTotalService;

        //体检信息对象//列表页传进来//用于初始化绑定
        private TjlCustomerRegForInspectionTotalDto _tjlCustomerRegDto;

        //体检人项目集合
        private List<CustomerRegInspectItemDto> _CustomerRegItemList;

        //退回项目集合
        //List<TjlCustomerRegItemDto> ReturnRegItemList = new List<TjlCustomerRegItemDto>();

        //退回项目集合2
        List<TjlCustomerSummBackDto> ReturnSummBack = new List<TjlCustomerSummBackDto>();

        private void InspectionReturn_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = _CustomerRegItemList;
            LoadData();
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    var currentItem = gridControl1.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    var _zhuanhuan = ZhuanHuan(currentItem);
                    foreach (var item in ReturnSummBack)
                    {
                        if (item.ItemFlag == _zhuanhuan.ItemFlag && item.DepartmentId == _zhuanhuan.DepartmentId)
                        {
                            MessageBox.Show("已存在项目，不可重复选择。");
                            return;
                        }
                    }

                    ReturnSummBack.Add(_zhuanhuan);
                    this.gridControl2.DataSource = ReturnSummBack;
                    this.gridControl2.RefreshDataSource();
                    var list = gridControl2.GetDtoListDataSource<TjlCustomerSummBackDto>();
                    this.gridView2.FocusedRowHandle = list.IndexOf(_zhuanhuan);
                }
            }
        }

        public TjlCustomerSummBackDto ZhuanHuan(CustomerRegInspectItemDto t)
        {
            TjlCustomerSummBackDto _tjlCustomerSummBackDto = new TjlCustomerSummBackDto();
            _tjlCustomerSummBackDto.DepartmentId = t.DepartmentId;
            //_tjlCustomerSummBackDto.Department = t.DepartmentBM;
            _tjlCustomerSummBackDto.DepartmentName = t.DepartmentName;
            _tjlCustomerSummBackDto.ItemGroupName = t.ItemGroupName;
            _tjlCustomerSummBackDto.ItemFlag = t.ItemName;
            _tjlCustomerSummBackDto.CustomerRegBmId = _tjlCustomerRegDto.Id;
            return _tjlCustomerSummBackDto;
        }

        private void LoadData()
        {
            try
            {
                ReturnSummBack = _inspectionTotalService.GetCustomerSummBack(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id });
                this.gridControl2.DataSource = ReturnSummBack;
                this.gridControl2.RefreshDataSource();
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }

        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    var list = gridControl2.GetDtoListDataSource<TjlCustomerSummBackDto>();
                    var currentItem = gridControl2.GetFocusedRowDto<TjlCustomerSummBackDto>();
                    if (list != null)
                    {
                        ReturnSummBack.Remove(currentItem);
                        this.gridControl2.DataSource = ReturnSummBack;
                        this.gridControl2.RefreshDataSource();
                        this.gridView2.FocusedRowHandle = list.IndexOf(currentItem) - 1;
                    }
                }
            }
        }

        private void simpleButtonReturn_Click(object sender, EventArgs e)
        {
            try
            {
                _inspectionTotalService.CreateCustomerSummBack(ReturnSummBack);
                MessageBox.Show("已退回");
            }
            catch (UserFriendlyException i)
            {
                ShowMessageBox(i);
            }
            this.Close();
            this.Dispose();
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
