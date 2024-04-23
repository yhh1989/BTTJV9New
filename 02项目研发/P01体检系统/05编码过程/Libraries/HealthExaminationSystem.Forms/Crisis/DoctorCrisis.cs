using Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using HealthExaminationSystem.Enumerations;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public partial class DoctorCrisis : UserBaseForm
    {
        private readonly ICrisisAppService _crisisAppService;
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM;
        /// <summary>
        /// 预约id
        /// </summary>
        private Guid _customerRegId;
        public DoctorCrisis()
        {
            InitializeComponent();
            _crisisAppService = new CrisisAppService();
        }
        private void DoctorCrisis_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CustomerBM))
            {
                txtCustomBM.Text = CustomerBM;
                SearchData();
            }
        }
        /// <summary>
        /// 添加危急值项目
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var row= gvBeixuan.GetFocusedRow() as CustomerRegItemDto;
            if (row != null)
            {
                gcBeiXuan.RemoveDtoListItem(row);
                gcYixuan.AddDtoListItem(RegItemConvertCrisisInfo(row));
            }
        }
        /// <summary>
        /// 移除危急值项目
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = gvYiXuan.GetFocusedRow() as CrisisInfo;
            if(row!=null)
            {
                gcYixuan.RemoveDtoListItem(row);
                gcBeiXuan.AddDtoListItem(CrisisConvertRegItem(row));
            }
        }
        /// <summary>
        /// 保存危急值项目
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var list= gcYixuan.GetDtoListDataSource<CrisisInfo>();
            AutoLoading(() =>
            {
                _crisisAppService.SetCrisisList(new SetCrisisDto() { CustomerRegId = _customerRegId, CrisisInfos = ConvertList(list) });
                SearchData();
            });
           
            
        }
        /// <summary>
        /// 项目数据转换为危急值项目
        /// </summary>
        private CrisisInfo RegItemConvertCrisisInfo(CustomerRegItemDto data)
        {
            var result = new CrisisInfo();
            result.CheckTime = data.CheckTime;
            result.DepartmentName = data.DepartmentName;
            result.ItemName = data.ItemName;
            result.ItemResultChar = data.ItemResultChar;
            result.RegItemId = data.Id;
            result.ItemGroupName = data.ItemGroupName;
            result.CheckTime = data.CheckTime;
            return result;
        }
        /// <summary>
        /// 危急值项目转为项目数据
        /// </summary>
        private CustomerRegItemDto CrisisConvertRegItem(CrisisInfo data)
        {
            var result = new CustomerRegItemDto();
            result.CheckTime = data.CheckTime;
            result.DepartmentName = data.DepartmentName;
            result.ItemName = data.ItemName;
            result.ItemResultChar = data.ItemResultChar;
            result.Id = data.RegItemId;
            result.ItemGroupName = data.ItemGroupName;
            result.CheckTime = data.CheckTime;
            return result;
        }
        private List<TjlCrisisSetDto> ConvertList(List<CrisisInfo> list)
        {
            var result = new List<TjlCrisisSetDto>();
            foreach(var l in list)
            {
                var data = new TjlCrisisSetDto();
                data.TjlCustomerRegItemId = l.RegItemId;
                data.SetNotice = l.SetNotice;
                result.Add(data);
            }
            return result;
        }
        /// <summary>
        /// 列表颜色样式修改
        /// </summary>
        private void gvBeixuan_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.Name == conJieGuo.Name)
            {
                var data = gvBeixuan.GetRow(e.RowHandle) as CustomerRegItemDto;
                if (data != null)
                {
                    if (data.Symbol != SymbolHelper.SymbolFormatter(Symbol.Normal) && data.Symbol != null)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
                   
            }
        }
        private void gvWeijizhi_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.FieldName == gConType.FieldName)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((CallBackType)e.Value);
            }
        }

        private void txtCustomBM_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != System.Windows.Forms.Keys.Enter)
                return;
            SearchData();
        }
        /// <summary>
        /// 检索数据方法
        /// </summary>
        private void SearchData()
        {
            if (!string.IsNullOrWhiteSpace(txtCustomBM.Text))
            {
                AutoLoading(() =>
                {
                    var data = _crisisAppService.QueryCrisisInfos(new SearchCrisisInfosDto() { CustomerBM = txtCustomBM.Text });
                    txtName.EditValue = data.Name;
                    txtAge.EditValue = data.Age;
                    txtSex.Text = EnumHelper.GetEnumDesc((Sex)data.Sex);
                    txtMobile.EditValue = data.Mobile;
                    txtClientReg.EditValue = data.ClientName;
                    txtIdNo.EditValue = data.IDCardNo;
                    _customerRegId = data.CustomerRegId;
                    gcBeiXuan.DataSource = data.RegItemList;
                    gcYixuan.DataSource = data.CrisisList.GroupBy(o => o.Id).Select(o => o.First())?.ToList();
                    gcWeijizhi.DataSource = data.CrisisList;
                });
            }
        }
    }
}