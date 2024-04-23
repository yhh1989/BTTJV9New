using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.PersonnelCategory
{
    public partial class PersonnelCategoryList : UserBaseForm
    {
        private IPersonnelCategoryAppService _personnelCategoryAppService;

        PersonnelCategoryViewDto perCate = new PersonnelCategoryViewDto();

        List<PersonnelCategoryViewDto> listPerCate = new List<PersonnelCategoryViewDto>();
        public PersonnelCategoryList()
        {
            InitializeComponent();
            _personnelCategoryAppService = new PersonnelCategoryAppService();
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            listPerCate = _personnelCategoryAppService.QueryCategoryList(new PersonnelCategoryViewDto());
            gridControl.DataSource = listPerCate;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                dxErrorProvider.SetError(txtName, string.Format(Variables.MandatoryTips, "类别名称"));
                txtName.Focus();
                return;
            }
            if (perCate.Id == null || perCate.Id == Guid.Empty)
            {
                perCate.Name = txtName.Text;
                if (chkIsFree.Checked)
                    perCate.IsFree = true;
                else
                    perCate.IsFree = false;
                if (chkIsActive.Checked)
                    perCate.IsActive = true;
                else
                    perCate.IsActive = false;

                var insertPerCate = _personnelCategoryAppService.SaveCategory(perCate);
                if (insertPerCate != null)
                {
                    ShowMessageSucceed("保存成功！");
                    listPerCate.Add(insertPerCate);
                    gridControl.RefreshDataSource();
                }
            }
            else
            {
                perCate.Name = txtName.Text;
                if (chkIsFree.Checked)
                    perCate.IsFree = true;
                else
                    perCate.IsFree = false;
                if (chkIsActive.Checked)
                    perCate.IsActive = true;
                else
                    perCate.IsActive = false;

                var updatePerCate = _personnelCategoryAppService.EditeCategory(perCate);
                if (updatePerCate)
                {
                    ShowMessageSucceed("修改成功！");
                    listPerCate.Remove(perCate);
                    listPerCate.Add(perCate);
                    gridControl.RefreshDataSource();
                }
            }
            perCate = new PersonnelCategoryViewDto();
            txtName.Text = string.Empty;
            chkIsFree.Checked = false;
            chkIsActive.Checked = false;
        }

        //点击行编辑
        private void gdvCategory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var rowList = gridViewPersonnelCategoryViewDto.GetSelectedRows();
                var row = gridViewPersonnelCategoryViewDto.GetRow(rowList[0]) as PersonnelCategoryViewDto;

                perCate = row;
                txtName.Text = row.Name;
                if (row.IsFree)
                    chkIsFree.Checked = true;
                else
                    chkIsFree.Checked = false;
                if (row.IsActive)
                    chkIsActive.Checked = true;
                else
                    chkIsActive.Checked = false;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearchName.Text))
            {
                var cate = gridControl.DataSource as List<PersonnelCategoryViewDto>;
                cate = cate.Where(o => o.Name.Contains(txtSearchName.Text)).ToList();
                gridControl.DataSource = cate;
                gridControl.RefreshDataSource();
            }
            else
            {
                gridControl.DataSource = listPerCate;
                gridControl.RefreshDataSource();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                dxErrorProvider.SetError(txtName, string.Format(Variables.MandatoryTips, "类别名称"));
                txtName.Focus();
                return;
            }          
            if(perCate.Id != null &&  perCate.Id != Guid.Empty)
            {
              

                var updatePerCate = _personnelCategoryAppService.DeleteCategory(perCate);
                if (updatePerCate)
                {
                    ShowMessageSucceed("删除成功！");
                    listPerCate.Remove(perCate);
                    listPerCate.Add(perCate);
                    gridControl.RefreshDataSource();
                }
            }
            perCate = new PersonnelCategoryViewDto();
            txtName.Text = string.Empty;
            chkIsFree.Checked = false;
            chkIsActive.Checked = false;
            simpleButton1.PerformClick();
        }
    }
}
