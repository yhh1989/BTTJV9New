using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using System;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemSuit
{
    public partial class ItemSuitList : UserBaseForm
    {
        private readonly IItemSuitAppService service = new ItemSuitAppService();

        public ItemSuitList()
        {
            InitializeComponent();

            lueItemSuitType.SetClearButton();

            gvItemSuit.Columns[gvItemSuitSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[gvItemSuitSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            gvItemSuit.Columns[gvItemSuitMaritalStatus.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[gvItemSuitMaritalStatus.FieldName].DisplayFormat.Format = new CustomFormatter(MarrySateHelper.CustomMarrySateFormatter);

            gvItemSuit.Columns[gvItemSuitAvailable.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[gvItemSuitAvailable.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);

            gvItemSuit.Columns[gvItemSuitConceiveStatus.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[gvItemSuitConceiveStatus.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);

            gvItemSuit.Columns[gvItemSuitExaminationType.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[gvItemSuitExaminationType.FieldName].DisplayFormat.Format = new CustomFormatter((obj) => CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.ExaminationType,obj));

            gvItemSuit.Columns[gvItemSuitItemSuitType.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvItemSuit.Columns[gvItemSuitItemSuitType.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.ItemSuitTypeFormatter);
        }
        
        #region 事件
        private void ItemSuit_Load(object sender, EventArgs e)
        {
            LoadControlData();
            Reload();
        }
        private void sbReload_Click(object sender, EventArgs e)
        {
            Reload();
        }
        private void sbAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void sbEdit_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullItemSuitDto>();
            Edit(dto);
        }
        private void sbDel_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullItemSuitDto>();
            Del(dto);
        }
        private void gvItemSuit_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var dto = (FullItemSuitDto)gvItemSuit.GetRow(e.RowHandle);
                Edit(dto);
            }
        }
        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }
        #endregion

        #region 处理
        private void LoadControlData()
        {
            lueItemSuitType.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ItemSuitType));
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Reload()
        {
            AutoLoading(() =>
            {
                var output = service.PageFulls(new PageInputDto<SearchItemSuitDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new SearchItemSuitDto
                    {
                        QueryText = teItemSuitName.Text.Trim(),
                        ItemSuitType = lueItemSuitType.EditValue == null ? null : (int?)((int)lueItemSuitType.EditValue),
                        Available = ceAvailable.Checked ? (int?)1 : null,
                    }
                });
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator);
                gridControl.DataSource = output.Result;
                gvItemSuit.BestFitColumns();
            });
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
        {
            ItemSuitEditor frm = new ItemSuitEditor();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridControl.AddDtoListItem(frm._Model);
                gridControl.RefreshDataSource();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        public void Edit(FullItemSuitDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择项目套餐。");
                return;
            }
            var temp = ModelHelper.DeepCloneByJson(dto);
            ItemSuitEditor frm = new ItemSuitEditor(temp);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ModelHelper.CustomMapTo(frm._Model, dto);
                gridControl.RefreshDataSource();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        public void Del(FullItemSuitDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择要删除的项目套餐。");
                return;
            }
            var question = XtraMessageBox.Show("是否删除？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
            {
                return;
            }
            
            AutoLoading(() =>
            {
                service.Del(new EntityDto<Guid> { Id = dto.Id });
                gridControl.RemoveDtoListItem(dto);
            }, Variables.LoadingDelete);
        }
        #endregion

    }
}
