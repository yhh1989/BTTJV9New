using Sw.Hospital.HealthExaminationSystem.ApiProxy.TargetDisease;
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
    public partial class HumanBodySystemAdministration : UserBaseForm
    {
        /// <summary>
        /// 目标疾病
        /// </summary>
        private readonly ITargetDiseaseAppService _itemSuitAppService;
        public HumanBodySystemAdministration()
        {
            InitializeComponent();

            _itemSuitAppService = new TargetDiseaseAppService();
        }
        /// <summary>
        /// 已选
        /// </summary>
        public List<UpdateHumanBodySystemDto> InHumanBodySystem { get; set; }
        /// <summary>
        /// 未选
        /// </summary>
        public List<UpdateHumanBodySystemDto> NotHumanBodySystem { get; set; }
        private void HumanBodySystemAdministration_Load(object sender, EventArgs e)
        {
            loads();
        }
        /// <summary>
        /// 增加检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNew_Click(object sender, EventArgs e)
        {

            UpdateHumanBodySystemDto dto = new UpdateHumanBodySystemDto();
            dto.Name = textBoxName.Text.Trim();
            _itemSuitAppService.UpdateHumanBodySystem(dto);
            loads();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            SearchOccupationalDiseaseIncludeItemGroupDto dto = new SearchOccupationalDiseaseIncludeItemGroupDto();
            dto.HumanBodySystems = InHumanBodySystem;
            _itemSuitAppService.UpdateItemGroup(dto);
            DialogResult = DialogResult.OK;

        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void loads()
        {
            gridControlIn.DataSource = InHumanBodySystem;
            NotHumanBodySystem = _itemSuitAppService.SelectHumanBodySystemAll(new UpdateHumanBodySystemDto());
            foreach (var item in InHumanBodySystem)
            {
                NotHumanBodySystem.Remove(item);
            }
            gridControlNot.DataSource = NotHumanBodySystem;
        }
        /// <summary>
        /// 未选grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNot_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //获取选中行
                var SelectId = (Guid)gridViewNot.GetFocusedRowCellValue(gridColumnNotId.FieldName);
                //得到对应选择的数据
                var SymptomFirst = NotHumanBodySystem.Where(o => o.Id == SelectId).FirstOrDefault();
                //判断是否为空
                if (SymptomFirst != null)
                {
                    //从未选列表中删除该数据并绑定数据
                    gridControlNot.DataSource = NotHumanBodySystem.Remove(SymptomFirst);
                    //将此数据添加到未选项目中
                    InHumanBodySystem.Add(SymptomFirst);
                    //绑定已选项目列表
                    gridControlIn.DataSource = InHumanBodySystem;
                }
            }
        }
        /// <summary>
        /// 已选grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewIn_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //获取选中行
                var SelectId = (Guid)gridViewIn.GetFocusedRowCellValue(gridColumnNotId.FieldName);
                //得到对应选择的数据
                var SymptomFirst = InHumanBodySystem.Where(o => o.Id == SelectId).FirstOrDefault();
                //判断是否为空
                if (SymptomFirst != null)
                {
                    //从未选列表中删除该数据并绑定数据
                    gridControlIn.DataSource = InHumanBodySystem.Remove(SymptomFirst);
                    //将此数据添加到未选项目中
                    NotHumanBodySystem.Add(SymptomFirst);
                    //绑定已选项目列表
                    gridControlNot.DataSource = NotHumanBodySystem;
                }
            }
        }
    }
}
