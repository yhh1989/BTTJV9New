
using Sw.Hospital.HealthExaminationSystem.ApiProxy.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.TargetDisease
{
    public partial class OccupationalDiseaseIncludeItemGroupAdministration : UserBaseForm
    {
        private readonly IItemGroupAppService _itemGroupAppService;
        private readonly ITargetDiseaseAppService _itemSuitAppService;
        public OccupationalDiseaseIncludeItemGroupAdministration()
        {
            InitializeComponent();
            _itemGroupAppService = new ItemGroupAppService();
            _itemSuitAppService = new TargetDiseaseAppService();
        }
        /// <summary>
        /// 目标疾病信息
        /// </summary>
        public SearchOccupationalDiseaseIncludeItemGroupDto OccupationalDiseaseIncludeItemGroup { get; set; }
        /// <summary>
        /// 已选项目信息
        /// </summary>
        public List<SimpleItemGroupDto> InItemGroupSys { get; set; }
        /// <summary>
        /// 未选项目信息
        /// </summary>
        public List<SimpleItemGroupDto> NotItemGroupSys { get; set; }
        /// <summary>
        /// 是否为必选项目
        /// </summary>
        public bool IsMandatory { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOccupationalDiseaseIncludeItemGroup_Load(object sender, EventArgs e)
        {
            NotItemGroupSys = _itemGroupAppService.QuerySimples(new SearchItemGroupDto());
            if (InItemGroupSys.Count() > 0)
            {
                foreach (var item in InItemGroupSys)
                {
                    NotItemGroupSys.Remove(item);
                }
            }
            gridControlNotItemGroup.DataSource = NotItemGroupSys;
            gridControlSelectedItemGroup.DataSource = InItemGroupSys;
        }
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNotItemGroup_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if(e.Clicks == 2)
            {
                if (gridViewNotItemGroup.GetFocusedRowCellValue(gridColumnNotItemGroupId.FieldName) == null)
                {
                    return;
                }
                //获取选中行
                var CustomerId = (Guid)gridViewNotItemGroup.GetFocusedRowCellValue(gridColumnNotItemGroupId.FieldName);
                //得到对应选择的数据
                var ItemGroupFirst = NotItemGroupSys.Where(o => o.Id == CustomerId).FirstOrDefault();
                //判断是否为空
                if (ItemGroupFirst != null)
                {
                    //从未选列表中删除该数据并绑定数据
                    gridControlNotItemGroup.DataSource = NotItemGroupSys.Remove(ItemGroupFirst);
                    //将此数据添加到未选项目中
                    InItemGroupSys.Add(ItemGroupFirst);
                    //绑定已选项目列表
                    gridControlSelectedItemGroup.DataSource = InItemGroupSys;
                    gridViewNotItemGroup.RefreshData();
                    gridViewSelectedItemGroup.RefreshData();
                }
            }
        }
        /// <summary>
        /// 删除已选项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSelectedItemGroup_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridViewSelectedItemGroup.GetFocusedRowCellValue(gridColumnNotItemGroupId.FieldName) == null)
                {
                    return;
                }
                //获取选中行
                var CustomerId = (Guid)gridViewSelectedItemGroup.GetFocusedRowCellValue(gridColumnSelectid.FieldName);
                //得到对应选择的数据
                var ItemGroupFirst = InItemGroupSys.Where(o => o.Id == CustomerId).FirstOrDefault();
                //判断是否为空
                if (ItemGroupFirst != null)
                {
                    //从已选列表中删除该数据并绑定数据
                    gridControlSelectedItemGroup.DataSource = InItemGroupSys.Remove(ItemGroupFirst);
                    //将此数据添加到未选项目中
                    NotItemGroupSys.Add(ItemGroupFirst);
                    //绑定未选项目列表
                    gridControlNotItemGroup.DataSource = NotItemGroupSys;
                    gridViewNotItemGroup.RefreshData();
                    gridViewSelectedItemGroup.RefreshData();
                }
            }
        }
        /// <summary>
        /// 保存项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (IsMandatory)
            {
                OccupationalDiseaseIncludeItemGroup.MustHaveItemGroups= InItemGroupSys;
            }
            else
            {
                OccupationalDiseaseIncludeItemGroup.MayHaveItemGroups = InItemGroupSys;
            }
            _itemSuitAppService.UpdateItemGroup(OccupationalDiseaseIncludeItemGroup);
            DialogResult = DialogResult.OK;
        }
    }
}
