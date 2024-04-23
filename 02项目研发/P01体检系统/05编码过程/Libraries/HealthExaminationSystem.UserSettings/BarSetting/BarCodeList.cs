using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using System;
using System.Linq;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BarSetting
{
    public partial class BarCodeList : UserBaseForm
    {
        private readonly IBarSettingAppService service = new BarSettingAppService();

        public BarCodeList()
        {
            InitializeComponent();

            gvBarItems.DataController.AllowIEnumerableDetails = true;
            gvBarSetting.Columns[gvBarSettingIsRepeatItemBarcode.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvBarSetting.Columns[gvBarSettingIsRepeatItemBarcode.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);

            gvBarSetting.Columns[gvBarSettingSampletype.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvBarSetting.Columns[gvBarSettingSampletype.FieldName].DisplayFormat.Format = new CustomFormatter((val) => CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.SpecimenType, val));
            

            gvBarSetting.Columns[gvBarSettingtestType.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvBarSetting.Columns[gvBarSettingtestType.FieldName].DisplayFormat.Format = new CustomFormatter((val) => CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.TestType, val));

            gvBarSetting.Columns[gvBarSettingBarNUM.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvBarSetting.Columns[gvBarSettingBarNUM.FieldName].DisplayFormat.Format = new CustomFormatter((val) => CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.PrintMethod, val));

            gvBarSetting.Columns[gvBarSettingPrintAdress.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvBarSetting.Columns[gvBarSettingPrintAdress.FieldName].DisplayFormat.Format = new CustomFormatter((val) => CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.PrintPosition, val));
        }

        private void BarSetting_Load(object sender, EventArgs e)
        {
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
            var dto = gridControl.GetFocusedRowDto<FullBarSettingDto>();
            Edit(dto);
        }
        private void sbDel_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullBarSettingDto>();
            Del(dto);
        }

        private void gvBarSetting_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if(e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var dto = (FullBarSettingDto)gvBarSetting.GetRow(e.RowHandle);
                Edit(dto);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Reload()
        {
            AutoLoading(() => {
                var output = service.PageFulls(new PageInputDto<SearchBarSettingDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new SearchBarSettingDto
                    {
                        BarName = teBarName.Text.Trim()
                    }
                });
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator);
                gridControl.DataSource = output.Result;
                gvBarItems.BestFitColumns();
            });
        }
        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
        {
            BarCodeEditor frm = new BarCodeEditor();
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
        public void Edit(FullBarSettingDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择条码。");
                return;
            }
            var temp = ModelHelper.DeepCloneByJson(dto);
            BarCodeEditor frm = new BarCodeEditor(temp);
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
        public void Del(FullBarSettingDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择要删除的条码。");
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
            AutoLoading(() => {
                service.Del(new EntityDto<Guid> { Id = dto.Id });
                gridControl.RemoveDtoListItem(dto);
            });
        }

        private void gvBarSetting_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender,e);
            Reload();
        }

    }
}
