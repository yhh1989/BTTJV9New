using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    public partial class JobCategory : UserBaseForm
    {
        private readonly PostStateAppService PostStateAppService;
        public JobCategory()
        {
            PostStateAppService = new PostStateAppService();
            InitializeComponent();
        }
        private Guid selectGuid { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobCategory_Load(object sender, EventArgs e)
        {
            gridControlJobCategory.DataSource = PostStateAppService.GetAll(new JobCategoryDto());
        }
        /// <summary>
        /// 保存和修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonModify_Click(object sender, EventArgs e)
        {
            var name = textEditName.Text.Trim();
            List<JobCategoryDto> dto = gridControlJobCategory.DataSource as List<JobCategoryDto>;
            if (dto.Any(o=>o.Name == name)){
                dxErrorProvider.SetError(textEditName, "数据已存在，请勿重复维护！");
                return;
            }
            JobCategoryDto input = new JobCategoryDto();
            var state = comboBoxEditState.SelectedIndex;
            input.Name = name;
            input.IsActive = state == 0 ? true : false;
            if (selectGuid == Guid.Empty)
            {
                input.Id = Guid.NewGuid();
                PostStateAppService.Insert(input);
            }
            else
            {
                input.Id = selectGuid;
                PostStateAppService.Edit(input);
            }
            gridControlJobCategory.DataSource = PostStateAppService.GetAll(new JobCategoryDto());
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelect_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 选择后绑定到文本框上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewJobCategory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var i = gridViewJobCategory.FocusedRowHandle;
            if (i == -1)
                return;
            
            selectGuid = (Guid)gridViewJobCategory.GetFocusedRowCellValue(gridColumnid.FieldName);
            textEditName.Text = gridViewJobCategory.GetFocusedRowCellValue(gridColumnPostStateName.FieldName)?.ToString();
            var IsActive = gridViewJobCategory.GetFocusedRowCellValue(gridColumnIsActive.FieldName).ToString();
            if (IsActive == "True")
            {
                comboBoxEditState.SelectedIndex = 0;
            }
            else
            {
                comboBoxEditState.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// 新增
        /// 清空列表选择信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonInsert_Click(object sender, EventArgs e)
        {
            selectGuid = Guid.Empty;
            textEditName.Text = string.Empty;
            comboBoxEditState.SelectedIndex = 0;
        }
    }
}
