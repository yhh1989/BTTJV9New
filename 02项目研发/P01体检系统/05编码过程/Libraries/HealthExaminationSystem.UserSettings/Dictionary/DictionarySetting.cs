using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Dictionary;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Dictionary
{
    public partial class DictionarySetting : UserBaseForm
    {
        private readonly IDictionaryAppService _dictionaryAppService;
        public DictionarySetting()
        {
            InitializeComponent();
            _dictionaryAppService = new DictionaryAppService();

        }

        private void DictionarySetting_Load(object sender, EventArgs e)
        {
            LoadControlData();
            LoadData();
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEditItemDepartName.EditValue != null)
            {
                LoadData();
            }

            
        }

        private void lueDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            LoadData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void sbReset_Click(object sender, EventArgs e)
        {
            textEditItemDepartName.EditValue = null;
            lueDepartment.EditValue = null;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
           if( e.Clicks == 2)
                Open();
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Open();
        }



        #region 处理
        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void LoadControlData()
        {
            AutoLoading(() =>
            {
                lueDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            });
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

            try
            {
                var SearchItemInfoDto = new SearchItemInfoDto();
                if (lueDepartment.EditValue != null)
                    SearchItemInfoDto.DepartmentId = (Guid)lueDepartment.EditValue;
                if (!string.IsNullOrWhiteSpace(textEditItemDepartName.Text.Trim()))
                    SearchItemInfoDto.Name = textEditItemDepartName.Text.Trim();


                var output = _dictionaryAppService.QueryInfoDepart(SearchItemInfoDto);
                if (output != null)
                {
                    gridControlDictionary.DataSource = null;
                    gridControlDictionary.DataSource = output.OrderBy(r => r.OrderNum);

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


        private void Open()
        {
            var ItemInfoid = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, Id);
            var Departentid = gridViewItemInfo.GetRowCellValue(gridViewItemInfo.FocusedRowHandle, DepartmentId);
            try
            {
                using (var frm = new DictionaryEdit((Guid)ItemInfoid, (Guid)Departentid))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        sbReload.PerformClick();
                    }
                }
            }
            catch
            {
                XtraMessageBox.Show("请选择项目进行查看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
        }

        #endregion

       
    }
}