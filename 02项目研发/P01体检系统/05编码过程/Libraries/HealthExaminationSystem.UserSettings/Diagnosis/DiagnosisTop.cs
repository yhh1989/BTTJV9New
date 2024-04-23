using System;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Department
{
    public partial class DiagnosisTop : UserBaseForm
    {
        private IDiagnosisAppService service = null;
        public IDiagnosisAppService _Service
        {
            get
            {
                if (service == null) service = new DiagnosisAppService();
                return service;
            }
        }

        public DiagnosisTop()
        {
            InitializeComponent();
            InitGridView();
        }
        #region 初始化表格
        private void InitGridView()
        {
            LoadData();
        }

        private void GridSetting(GridView grid)
        {
            grid.OptionsView.ShowGroupPanel = false;
            grid.OptionsBehavior.AutoExpandAllGroups = true;
            grid.OptionsCustomization.AllowColumnMoving = false;
            grid.OptionsCustomization.AllowFilter = false;
            grid.OptionsCustomization.AllowGroup = false;
            grid.OptionsDetail.ShowDetailTabs = false;
        }

        private GridColumn BuildGridCol(string name, string caption, int width = 75, bool fixedWidth = false)
        {
            GridColumn col = new GridColumn();
            col.Name = name;
            col.Caption = caption;
            col.FieldName = name;
            col.Visible = true; // 是否显示，需要设置为true
            //col.VisibleIndex = 0; // 显示顺序
            col.Width = width;
            col.OptionsColumn.FixedWidth = fixedWidth;
            col.OptionsColumn.AllowEdit = false; // 禁止编辑
            return col;
        }
        #endregion
        //添加
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new Diagnosis())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    simpleButton4.PerformClick();
            }
            //Diagnosis DiaAdd = new Diagnosis();
            //DiaAdd.ShowDialog();
            //simpleButton4.PerformClick();

        }
        //查询
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        //修改
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            var id = gridViewDiagnosi.GetRowCellValue(gridViewDiagnosi.FocusedRowHandle, Ids);
            Diagnosis DiaAdd = new Diagnosis((Guid)id);
            DiaAdd.ShowDialog();
            simpleButton4.PerformClick();
        }

        private void LoadData()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gridControl1.DataSource = null;

            try
            {


                var output = _Service.QueryDiagnosis(new PageInputDto<TbmDiagnosisDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new TbmDiagnosisDto
                    {
                        RuleName = teName.Text.Trim()
                    }
                });
                //gridControl1.DataSource = output;
                if (output != null)
                {
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator1);
                    gridControl1.DataSource = output.Result;
                }


            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
        //删除
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var id = gridViewDiagnosi.GetRowCellValue(gridViewDiagnosi.FocusedRowHandle, Ids);
            var name = gridViewDiagnosi.GetRowCellValue(gridViewDiagnosi.FocusedRowHandle, RuleNames);

            var question = XtraMessageBox.Show($"确定删除复合判断 {name}？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;

            try
            {
                _Service.DeleteDiagnosis(new EntityDto<Guid> { Id = Guid.Parse(id.ToString()) });
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

            simpleButton4.PerformClick();
        }

        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            LoadData();
        }

        private void teName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && teName.EditValue != null)
            {
                LoadData();
            }
        }

        private void gridViewDiagnosi_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var id = gridViewDiagnosi.GetRowCellValue(gridViewDiagnosi.FocusedRowHandle, Ids);
                Diagnosis DiaAdd = new Diagnosis((Guid)id);
                DiaAdd.ShowDialog();
                simpleButton4.PerformClick();
            }
        }
    }
}