using Abp.Application.Services.Dto;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.SumHide
{
    public partial class FrmSumHideList : UserBaseForm
    {
        // 总检
        private readonly IInspectionTotalAppService _inspectionTotalService;
        private List<TbmSumHideDto> SumHidels= new List<TbmSumHideDto>();
        private TbmSumHideDto sumhide = new TbmSumHideDto(); 
        public FrmSumHideList()
        {
            _inspectionTotalService = new InspectionTotalAppService();
            InitializeComponent();
            gridView1.Columns[conIsNormal.FieldName].DisplayFormat.Format = new CustomFormatter(gridColumnLevel);
        }
        private string gridColumnLevel(object arg)
        {
            try
            {
                if (arg.ToString() == "1")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            catch
            {
                return "";
            }
        }
        private void FrmSumHideList_Load(object sender, EventArgs e)
        {
          
            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            var result = new Application.BasicDictionary.Dto.BasicDictionaryDto();
            result.Value = -1;
            Examination.Insert(0, result);
            lookUpEditExaminationCategories.Properties.DataSource = Examination;
            repositoryItemLookUpEditPhysicalType.DataSource = Examination;

            SumHidels = _inspectionTotalService.SearchSumHide();
            gridControl1.DataSource = SumHidels;
            isButEnable(true);
            isTxtEnable(false);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conId);
            if (id != null)
            {
                sumhide = SumHidels.FirstOrDefault(o => o.Id == (Guid)id);
                txtSum.Text = sumhide.SumWord;
                if (int.TryParse(sumhide.ClientType, out int iType))
                {
                    lookUpEditExaminationCategories.EditValue = iType;
                }
                else
                {
                    lookUpEditExaminationCategories.EditValue = null;
                }
                if (sumhide.IsNormal == 1)
                {
                    chkIsnomal.Checked = true;
                }
                else
                {
                    chkIsnomal.Checked = false;
                }
               
            }
           
        }
        private void isTxtEnable(bool isShow)
        {
            txtSum.Enabled = isShow;
            lookUpEditExaminationCategories.Enabled = isShow;
            chkIsnomal.Enabled = isShow;
        }
        private void isButEnable(bool isShow)
        {
            bntAdd.Enabled = isShow;
            bntEdit.Enabled = isShow;
            bntSave.Enabled = !isShow;
            bntDel.Enabled = isShow;
        }

        private void bntAdd_Click(object sender, EventArgs e)
        {
            isButEnable(false);
            isTxtEnable(true);
            txtSum.Text = "";
            lookUpEditExaminationCategories.EditValue = "";
            chkIsnomal.Checked = false;
            sumhide = new TbmSumHideDto();
        }

        private void bntEdit_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conId);
            if (id == null)
            {
                MessageBox.Show("请选中需要修改的数据！");
                return;
            }
            isButEnable(false);
            isTxtEnable(true);
           
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSum.Text))
            {
                MessageBox.Show("诊断词不能空！");
                return;
            }
            sumhide.SumWord = txtSum.Text;
            sumhide.ClientType = lookUpEditExaminationCategories.EditValue?.ToString();
            if (sumhide.ClientType == null)
            {
                sumhide.ClientType = "";
            }
            if (chkIsnomal.Checked == true)
            {
                sumhide.IsNormal = 1;
            }
            else
            {
                sumhide.IsNormal = 2;
            }
            if (sumhide.Id != null && sumhide.Id != Guid.Empty)
            {
                _inspectionTotalService.EditSumHide(sumhide);
            }
            else
            {               
                _inspectionTotalService.AddSumHide(sumhide);
            }
            MessageBox.Show("保存成功!");
            SumHidels = _inspectionTotalService.SearchSumHide();
            gridControl1.DataSource = SumHidels;
            isButEnable(true);
            isTxtEnable(false);
        }

        private void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var txt = searchControl1.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    var Upper = txt.ToUpper();
                    var lsit = SumHidels.Where(o => (o.SumWord.Contains(txt) || o.HelpChar.Contains(Upper))).ToList();
                    gridControl1.DataSource = lsit;
                }
                else
                {
                    gridControl1.DataSource = SumHidels;
                }

            }
        }

        private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "正常诊断")
            {
                var result = SumHidels.Where(o => o.IsNormal == 1).ToList();
                gridControl1.DataSource = result;
            }
            else if (comboBoxEdit1.Text == "异常诊断")
            {
                var result = SumHidels.Where(o => o.IsNormal == 2).ToList();
                gridControl1.DataSource = result;
            }
            else
            {
                gridControl1.DataSource = SumHidels;
            }

        }

        private void bntDel_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conId);
            if (id == null)
            {
                MessageBox.Show("请选中需要修改的数据！");
                return;
            }
            EntityDto<Guid> input = new EntityDto<Guid>();
            input.Id = (Guid)id;
            _inspectionTotalService.delSumHide(input);
            SumHidels = _inspectionTotalService.SearchSumHide();
            gridControl1.DataSource = SumHidels;
            isButEnable(true);
            isTxtEnable(false);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
