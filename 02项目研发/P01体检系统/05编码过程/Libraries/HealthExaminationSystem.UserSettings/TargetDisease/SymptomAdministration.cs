
using Sw.Hospital.HealthExaminationSystem.ApiProxy.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
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
    public partial class SymptomAdministration : UserBaseForm
    {
        /// <summary>
        /// 目标疾病
        /// </summary>
        private readonly ITargetDiseaseAppService _itemSuitAppService;
        /// <summary>
        /// 缓存
        /// </summary>
        public SearchOccupationalDiseaseIncludeItemGroupDto searchOccupationalDiseaseIncludeItemGroupDto { get; set; }
        public List<UpdateSymptomDto> NotSymptomSys { get; set; }
        public SymptomAdministration()
        {
            InitializeComponent();
            _itemSuitAppService = new TargetDiseaseAppService();
        }
        /// <summary>
        /// 新增症状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNew_Click(object sender, EventArgs e)
        {
            UpdateSymptomDto dto = new UpdateSymptomDto();
            dto.Name = textEditSymptomName.Text;
            _itemSuitAppService.UpdateSymptom(dto);
            Loads();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSymptom_Load(object sender, EventArgs e)
        {
            Loads();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Loads()
        {
            gridControlIn.DataSource = searchOccupationalDiseaseIncludeItemGroupDto.Symptoms;
             NotSymptomSys = _itemSuitAppService.GetSymptom(new UpdateSymptomDto());
            foreach (var item in searchOccupationalDiseaseIncludeItemGroupDto.Symptoms)
            {
                NotSymptomSys.Remove(NotSymptomSys.First(o=>o.Id==item.Id));
            }
            gridControlNot.DataSource = NotSymptomSys;
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNewSymptom_Click(object sender, EventArgs e)
        {
            _itemSuitAppService.UpdateItemGroup(searchOccupationalDiseaseIncludeItemGroupDto);
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 未选项目
        /// 新增项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewOut_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridViewNot.GetFocusedRowCellValue(gridColumnNotId.FieldName) == null)
                {
                    return;
                }
                //获取选中行
                var SelectId = (Guid)gridViewNot.GetFocusedRowCellValue(gridColumnNotId.FieldName);
                //得到对应选择的数据
                var SymptomFirst = NotSymptomSys.Where(o => o.Id == SelectId).FirstOrDefault();
                //判断是否为空
                if (SymptomFirst != null)
                {
                    //从未选列表中删除该数据并绑定数据
                    gridControlNot.DataSource = NotSymptomSys.Remove(SymptomFirst);
                    //将此数据添加到未选项目中
                    searchOccupationalDiseaseIncludeItemGroupDto.Symptoms.Add(SymptomFirst);
                    //绑定已选项目列表
                    gridControlIn.DataSource = searchOccupationalDiseaseIncludeItemGroupDto.Symptoms;
                    gridViewIn.RefreshData();
                    gridViewNot.RefreshData();
                }
            }
        }
        /// <summary>
        /// 已选项目
        /// 删除项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewIn_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridViewIn.GetFocusedRowCellValue(gridColumnNotId.FieldName) == null)
                {
                    return;
                }
                //获取选中行
                var SelectId = (Guid)gridViewIn.GetFocusedRowCellValue(gridColumnNotId.FieldName);
                //得到对应选择的数据
                var SymptomFirst = searchOccupationalDiseaseIncludeItemGroupDto.Symptoms.Where(o => o.Id == SelectId).FirstOrDefault();
                //判断是否为空
                if (SymptomFirst != null)
                {
                    //从未选列表中删除该数据并绑定数据
                    gridControlIn.DataSource = searchOccupationalDiseaseIncludeItemGroupDto.Symptoms.Remove(SymptomFirst);
                    //将此数据添加到未选项目中
                    NotSymptomSys.Add(SymptomFirst);
                    //绑定已选项目列表
                    gridControlNot.DataSource = NotSymptomSys;
                    gridViewIn.RefreshData();
                    gridViewNot.RefreshData();
                }
            }
        }
    }
}
