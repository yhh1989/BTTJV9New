using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Company;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusReSultStatus
{
    public partial class ImportDataList : UserBaseForm
    {
        private readonly ICustomerAppService _customerAppService;
        public ImportDataList()
        {
            InitializeComponent();
            _customerAppService = new CustomerAppService();
        }

        private void ImportDataList_Load(object sender, EventArgs e)
        {
            var ClientNames = DefinedCacheHelper.GetClientRegNameComDto();
            sleDW.Properties.DataSource = ClientNames;
            // 加载体检状态数据
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();
            cuslookItemSuit.Properties.DataSource = DefinedCacheHelper.GetItemSuit().Where(o => o.Available == 1).ToList();
            //挂号科室
            var ghks = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
            customGridLookUpEdit1.Properties.DataSource = ghks;

            repositoryItemLookUpEdit1.DataSource = PhysicalEStateHelper.YYGetList();
            repositoryItemLookUpEdit2.DataSource = SummSateHelper.GetSelectList();
            repositoryItemLookUpEdit3.DataSource = DefinedCacheHelper.GetItemSuit().Where(o => o.Available == 1).ToList();
            repositoryItemLookUpEdit4.DataSource = ghks;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmOutModle frmOutModle = new frmOutModle();
            frmOutModle.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmIntReSult frmIntReSult = new FrmIntReSult();
            frmIntReSult.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchImportData show = new SearchImportData();
            if (sleDW.EditValue != null)
            {
                show.ClientRegId = (Guid?)sleDW.EditValue;
            }
            if (dateEditStartTime.EditValue != null)
            {
                show.StartDateTime = (DateTime?)dateEditStartTime.EditValue;
            }
            if (comboBoxEdit1.Text.Contains("导入"))
            {
                show.TimeType = 2;
            }
            else
            { show.TimeType = 1; }
            if (dateEditEndTime.EditValue != null)
            {
                show.EndDateTime =((DateTime?)dateEditEndTime.EditValue).Value.AddDays(1);
            }
            if (textEditName.EditValue != null)
            {
                show.CustomerBM = textEditName.EditValue.ToString();
            }
            if (textEditName.EditValue != null)
            {
                show.Name = textEditName.EditValue.ToString();
            }
            if (!string.IsNullOrWhiteSpace(cuslookItemSuit.EditValue?.ToString()))
            {
                show.SuitId = (Guid)cuslookItemSuit.EditValue;

            }
            if (!string.IsNullOrWhiteSpace(lookUpEditExaminationStatus.EditValue?.ToString()) && lookUpEditExaminationStatus.EditValue?.ToString()!="0")
            {
                
                show.CheckSate = (int)lookUpEditExaminationStatus.EditValue;

            }
            if (!string.IsNullOrWhiteSpace(lookUpEditSumStatus.EditValue?.ToString()) && lookUpEditSumStatus.EditValue?.ToString() != "0")
            {
                show.SummSate = (int)lookUpEditSumStatus.EditValue;

            }
            if (!string.IsNullOrWhiteSpace(customGridLookUpEdit1.EditValue?.ToString()))
            {
                show.DepartType = (int)customGridLookUpEdit1.EditValue;

            }

            var result = _customerAppService.GetImportDatas(show);
            gridControl1.DataSource = result;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView2.GetSelectedRows();         
            if (selectIndexes.Length != 0)
            {
                List<ImportDataDto> outcuslist = (List<ImportDataDto>)gridControl1.DataSource;
                List<ImportDataDto> cuslit = new List<ImportDataDto>();
                foreach (int r in selectIndexes)
                {
                    //根据gridview中的选中行索引获取数据源中对应的是行索引
                    int dataSourceRowNum = gridView2.GetDataSourceRowIndex(r);
                    cuslit.Add( outcuslist[dataSourceRowNum]);
                }
                frmOutModle frmOutModle = new frmOutModle(cuslit);
                frmOutModle.ShowDialog();

            }
            else
            {
                MessageBox.Show("请选择需要导出的人员列表");
            }
        }

        private void sleDW_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (sleDW.EditValue == null)
                    return;
                sleDW.EditValue = null;
                

            }
        }
    }
}
