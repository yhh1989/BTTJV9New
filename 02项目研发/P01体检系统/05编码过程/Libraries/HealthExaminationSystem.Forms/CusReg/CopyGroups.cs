using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CopyGroups : UserBaseForm
    {
        private readonly PhysicalExaminationAppService _PhysicalAppService;
        private readonly ICustomerAppService customerSvr;
        private readonly ItemSuitAppService _itemSuitAppService;
        private QueryCustomerRegDto curSelectCustomReg;//选中的人
        public FullItemSuitDto curSelectSuit;//选中的套餐
        public List<TjlCustomerItemGroupDto> curSelctGroup;//选中的加项组合
        public List<ClientRegDto> clientRegs;//单位字典
        public int? curSex;//当前性别
        public CopyGroups()
        {
            InitializeComponent();
            _PhysicalAppService = new PhysicalExaminationAppService();
            _itemSuitAppService = new ItemSuitAppService();
            customerSvr = new CustomerAppService();
        }

        private void CopyGroups_Load(object sender, EventArgs e)
        {
            gridView1.Columns[colSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[colSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
            TeDW.Properties.DataSource = clientRegs;
        }
        /// <summary>
        /// 搜索按钮时间 查找人员
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Query();
        }
        public void Query()
        {
            dxErrorProvider.ClearErrors();
            gridControl1.DataSource = null;
            AutoLoading(() =>
            {
                CustomerRegPhysicalDto dto = new CustomerRegPhysicalDto();
                CustomerPhysicalDto customer = new CustomerPhysicalDto();
                if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                {
                    customer.SerchInput = teTJH.Text.Trim();
                }


                ////体检号
                //if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
                //    dto.CustomerBM = teTJH.Text.Trim();
                ////姓名

                //if (!string.IsNullOrWhiteSpace(teXM.Text.Trim()))
                //    customer.Name = teXM.Text.Trim();
                ////证件号
                //if (!string.IsNullOrWhiteSpace(teZJH.Text.Trim()))
                //    customer.IDCardNo = teZJH.Text.Trim();

                //单位
                if (TeDW.EditValue != null)
                {
                    if (!string.IsNullOrWhiteSpace(TeDW.EditValue.ToString()))
                    {
                        dto.TjlClientReg_Id = Guid.Parse(TeDW.EditValue.ToString());
                    }
                }
                //时间
                if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
                {
                    if (Convert.ToDateTime(teSJQ.Text) > Convert.ToDateTime(daSJZ.Text))
                    {
                        dxErrorProvider.SetError(daSJZ, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                        daSJZ.Focus();
                        return;
                    }

                    dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
                    dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
                }
                dto.Customer = customer;

                var output = _PhysicalAppService.PersonalInformationQuery(new PageInputDto<CustomerRegPhysicalDto> { TotalPages = TotalPages, CurentPage = CurrentPage, Input = dto });
                if (output != null)
                {
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    gridControl1.DataSource = output.Result;
                }
                InitialNavigator(dataNavigator1);
            });
        }
        /// <summary>
        /// 人员列表行点击事件
        /// </summary>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            var tjcode = gridView1.GetRowCellValue(e.RowHandle, colTjCode)?.ToString();
            var result= customerSvr.QueryCustomerReg(new SearchCustomerDto() { CustomerBM = tjcode });
            if (result != null)
            {
                curSelectCustomReg = result.FirstOrDefault();
                if (curSelectCustomReg != null)
                {
                    gridControl3.DataSource = curSelectCustomReg.CustomerItemGroup;
                   // gridControl3.che
                }
            }
            
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (curSelectCustomReg != null)
            {
                if (curSelectCustomReg.CustomerItemGroup != null)
                {
                    if (curSelectCustomReg.CustomerItemGroup.Count > 0)
                    {
                        bool isxbbf = false;
                        if(curSelectCustomReg.Customer.Sex!=curSex&&curSex!=(int)Sex.GenderNotSpecified)
                        {
                            isxbbf = true;
                            //ShowMessageBoxInformation("性别不符，不能复制该人员项目");
                            //return;
                        }
                        if (curSelectCustomReg.ItemSuitBMId.HasValue)
                        {
                            var suits = _itemSuitAppService.Get(new SearchItemSuitDto { Id = curSelectCustomReg.ItemSuitBMId.Value });
                            curSelectSuit = suits;
                        }
                        var groups = curSelectCustomReg.CustomerItemGroup.Where(o => !o.ItemSuitId.HasValue || o.IsAddMinus == (int)AddMinusType.Minus).ToList();
                        if (curSelectCustomReg.CustomerItemGroup.Count > 0)
                        {
                            curSelctGroup = new List<TjlCustomerItemGroupDto>();
                            foreach (var group in groups)
                            {
                                if (isxbbf )
                                {
                                    var itemgroup = DefinedCacheHelper.GetItemGroups().FirstOrDefault(
                                        p => p.Id == group.ItemGroupBM_Id);
                                    if (itemgroup != null && itemgroup.Sex != (int)Sex.GenderNotSpecified
                                        && itemgroup.Sex != (int)Sex.Unknown &&
                                        itemgroup.Sex != (int)curSex)
                                    {
                                        continue;
                                    }

                                }
                                curSelctGroup.Add(
                                    new TjlCustomerItemGroupDto
                                    {
                                        DepartmentId = group.DepartmentId,
                                        DepartmentName = group.DepartmentName,
                                        DepartmentOrder = group.DepartmentOrder,
                                        ItemGroupOrder = group.ItemGroupOrder,
                                        ItemGroupBM_Id = group.ItemGroupBM_Id,
                                        ItemGroupName = group.ItemGroupName,
                                        ItemPrice = group.ItemPrice,
                                        DiscountRate = group.DiscountRate,
                                        PriceAfterDis = group.PriceAfterDis,
                                        IsAddMinus = group.IsAddMinus,
                                    });
                            }
                        }
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            //DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 导航按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataNavigator1_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            btnSearch_Click(sender, e);
        }

        private void TeDW_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                TeDW.EditValue = null;
            }
        }

        private void teTJH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Query();
        }
    }
}
