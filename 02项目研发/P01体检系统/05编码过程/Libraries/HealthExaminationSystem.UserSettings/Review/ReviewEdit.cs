using Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Review
{
    public partial class ReviewEdit : UserBaseForm
    {
        private readonly ISummarizeAdviceAppService service = new SummarizeAdviceAppService();
        private readonly CrisisAppService crisisAppService = new CrisisAppService();
        private  ReviewSetDto ReviewSetDto = new ReviewSetDto();

        public ReviewEdit()
        {
            InitializeComponent();
        }
        public ReviewEdit(ReviewSetDto reviewSetDto)
        {
            InitializeComponent();
            ReviewSetDto = reviewSetDto;

        }

        private void ReviewEdit_Load(object sender, EventArgs e)
        {
            SearchSummarizeAdvice searchSummarizeAdvice = new SearchSummarizeAdvice();            
            var advice = DefinedCacheHelper.GetSummarizeAdvices();
            txtIllName.Properties.DataSource = advice;
    
            gcWait.DataSource = DefinedCacheHelper.GetItemGroups().Where(p=>p.IsActive== true
            && p.Department !=null).ToList();
            if (ReviewSetDto != null && ReviewSetDto.Id !=Guid.Empty)
            {
                txtIllName.EditValue = ReviewSetDto.SummarizeAdviceId;
                txtReviewTime.EditValue = ReviewSetDto.Checkday.ToString();
                txtVisitTime.EditValue= ReviewSetDto.KFday.ToString();
                textEdit1.EditValue = ReviewSetDto.Remark;
                gcSelected.DataSource = ReviewSetDto.ItemGroupBM;

                var ls= gcWait.GetDtoListDataSource<SimpleItemGroupDto>()?.RemoveAll(m => ReviewSetDto.ItemGroupBM.Any(i => i.Id == m.Id));
                gcWait.GetDtoListDataSource<SimpleItemGroupDto>()?.RemoveAll(m => ReviewSetDto.ItemGroupBM.Any(i => i.Id == m.Id));
                gcWait.RefreshDataSource();

            }

        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            Del();
        }
        /// <summary>
        /// 添加选择
        /// </summary> 
        private void sbAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        public void Add()
        {
            var dtos = gcWait.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (dtos.Count == 0) return;
            var itemSuitItemGroups = dtos;

            var list = gcSelected.GetDtoListDataSource<SimpleItemGroupDto>();
            if (list == null)
            {
                list = new List<SimpleItemGroupDto>();
                gcSelected.DataSource = list;
            }
            itemSuitItemGroups.RemoveAll(m => list.Any(s => s.Id == m.Id));
            list.AddRange(itemSuitItemGroups);
            gcSelected.RefreshDataSource();
            gcWait.GetDtoListDataSource<SimpleItemGroupDto>()?.RemoveAll(m => dtos.Any(i => i.Id == m.Id));
            gcWait.RefreshDataSource();
        }
        /// <summary>
        /// 移除选择
        /// </summary>
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (dtos.Count == 0) return;
            //gcSelected.RemoveDtoListItem(dtos);
            gcSelected.GetDtoListDataSource<SimpleItemGroupDto>()?.RemoveAll(m => dtos.Any(i => i.Id == m.Id));
            gcSelected.RefreshDataSource();

            var itemSuitItemGroups = dtos;

            var list = gcWait.GetDtoListDataSource<SimpleItemGroupDto>();
            if (list == null)
            {
                list = new List<SimpleItemGroupDto>();
                gcWait.DataSource = list;
            }
            itemSuitItemGroups.RemoveAll(m => list.Any(s => s.Id == m.Id));
            list.AddRange(itemSuitItemGroups);
            gcWait.RefreshDataSource();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
        /// <summary>
        /// 确定
        /// </summary>
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            if (string.IsNullOrEmpty(txtIllName.EditValue?.ToString()))
            {
                dxErrorProvider.SetError(txtIllName, string.Format(Variables.MandatoryTips, "疾病名称"));
                txtIllName.Focus();
                return false;
            }
            // 组合列表
             var  ItemSuitItemGroups = gcSelected.GetDtoListDataSource<SimpleItemGroupDto>();
            if (gridView1.RowCount==0)
            {
                ShowMessageBoxWarning("未选择项目组合");
                return false;
            }

            ReviewSetDto.SummarizeAdviceId = Guid.Parse(txtIllName.EditValue.ToString());
            ReviewSetDto.IllName = txtIllName.Text;
            ReviewSetDto.Checkday =int.Parse( txtReviewTime.EditValue.ToString());
            ReviewSetDto.KFday = int.Parse(txtVisitTime.EditValue.ToString());
            ReviewSetDto.ItemGroupBM = new List<SimpleItemGroupDto>();
            ReviewSetDto.ItemGroupBM = ItemSuitItemGroups;
            ReviewSetDto.Remark = textEdit1.Text.ToString();
            //ReviewSetDto.
            AutoLoading(() =>
            {
                crisisAppService.SaveReviewSet(ReviewSetDto);
            }, Variables.LoadingSaveing);
            return true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            Add();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Del();
        }
    }
}
