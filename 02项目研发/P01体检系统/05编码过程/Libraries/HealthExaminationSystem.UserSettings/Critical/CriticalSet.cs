using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Critical;
using Sw.Hospital.HealthExaminationSystem.Application.Critical;
using Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
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
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Critical
{
    public partial class CriticalSet : UserBaseForm
    {
        private readonly Guid _id;
        private ICriticalAppService criticalAppService = new CriticalAppService();
        public CriticalSet()
        {
            InitializeComponent();
        }
        public CriticalSet(Guid id) : this()
        {
            _id = id;
        }
        private void CriticalSet_Load(object sender, EventArgs e)
        {

           var sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
            gridLookUpSex.Properties.DataSource = sexList;//性别

            //gridView1.Columns[conOperator.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            //gridView1.Columns[conOperator.FieldName].DisplayFormat.Format =
            //    new CustomFormatter(OperatorStateHelper.OperatorStateFormatter);
            repositoryItemLookUpEdit1.DataSource = OperatorStateHelper.GetList();
            repositoryItemLookUpEdit1.DisplayMember = "Display";
            repositoryItemITem.DataSource= DefinedCacheHelper.GetItemInfos();
            repositoryItemITem.DisplayMember = "Name";
            searchLookUpDepart.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            searchLookUpItem.Properties.DataSource = DefinedCacheHelper.GetItemInfos();
            searchLookUpCalculationType.Properties.DataSource = CalculationTypeStateHelper.GetList();
            searchLookUpCriticalTypeState.Properties.DataSource = CriticalTypeStateHelper.GetList();
            lookUpEditOperatorState.Properties.DataSource = OperatorStateHelper.GetList();
            if (_id != Guid.Empty)
            {
                SearchCriticalDto searchCriticalDto = new SearchCriticalDto();
                searchCriticalDto.Id = _id;
                var crid = criticalAppService.getSearchCriticalDto(searchCriticalDto);
                if (crid.Count > 0)
                {
                    searchLookUpItem.EditValue= crid[0].ItemInfoId;
                    searchLookUpDepart.EditValue= crid[0].DepartmentId;
                    searchLookUpCriticalTypeState.EditValue= crid[0].CriticalType;
                    searchLookUpCalculationType.EditValue= crid[0].CalculationType;
                    lookUpEditOperatorState.EditValue= crid[0].Operator;
                     ValueChar.EditValue= crid[0].ValueChar;

                     spinValueNum.EditValue = crid[0].ValueNum ;
                    if (crid[0].Sex.HasValue)
                    {
                        gridLookUpSex.EditValue = crid[0].Sex;
                    }
                    if (crid[0].Old.HasValue)
                    {
                        if (crid[0].Old == 1)
                        {
                            checkOld.Checked = true;
                        }
                        else
                        {
                            checkOld.Checked = false;
                        }
                    }
                  var carDetails=  crid[0].CriticalDetails;
                    if (carDetails != null)
                    {
                        gridCriDital.DataSource = carDetails;
                    }

                }
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(searchLookUpDepart.EditValue?.ToString()))
                {
                    MessageBox.Show("请选择科室！");
                    return;
                }
                if (string.IsNullOrEmpty(searchLookUpItem.EditValue?.ToString()))
                {
                    MessageBox.Show("请选择项目！");
                    return;
                }
                if (string.IsNullOrEmpty(searchLookUpCriticalTypeState.EditValue?.ToString()))
                {
                    MessageBox.Show("请选择分类！");
                    return;
                }
                if (string.IsNullOrEmpty(searchLookUpCalculationType.EditValue?.ToString()))
                {
                    MessageBox.Show("请选择类型！");
                    return;
                }
                if (string.IsNullOrEmpty(lookUpEditOperatorState.EditValue?.ToString()))
                {
                    MessageBox.Show("请选择运算符！");
                    return;
                }
                var input = new CriticalDto();
              
                if (_id != Guid.Empty)
                {
                    input.Id = _id;
                }
                input.ItemInfoId = (Guid)searchLookUpItem.EditValue;
                input.DepartmentId= (Guid)searchLookUpDepart.EditValue;
                input.CriticalType = (int)searchLookUpCriticalTypeState.EditValue;
                input.CalculationType = (int)searchLookUpCalculationType.EditValue;
                input.Operator = (int)lookUpEditOperatorState.EditValue;
                input.ValueChar = ValueChar.EditValue?.ToString();
                input.ValueNum = (decimal)spinValueNum.EditValue;
                var CriticalDetaillist = gridCriDital.DataSource as List<CriticalDetailDto>;
                if (!string.IsNullOrEmpty(gridLookUpSex.EditValue?.ToString()))
                {
                    input.Sex = (int)gridLookUpSex.EditValue;
                }
                else
                { input.Sex = (int)Sex.GenderNotSpecified; }
                if (checkOld.Checked == true)
                { input.Old = 1; }
                else
                { input.Old = 0; }
                if (CriticalDetaillist != null)
                {
                    input.CriticalDetails = CriticalDetaillist;
                }
                criticalAppService.SaveCritical(input);
                DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void searchLookUpDepart_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchLookUpDepart.EditValue?.ToString()))
            {
                var departId = (Guid)searchLookUpDepart.EditValue;
                var Itemlist = DefinedCacheHelper.GetItemInfos().Where(p=> p.Department!=null && p.Department.Id== departId).ToList();
                searchLookUpItem.Properties.DataSource = Itemlist;
            }
        }

        private void searchLookUpCalculationType_EditValueChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(searchLookUpCalculationType.EditValue?.ToString()))
            {
                var CalculationType = (int)searchLookUpCalculationType.EditValue;
                if (CalculationType==1)
                {
                    layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }

            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
           var CriticalDetaillist=  gridCriDital.DataSource as List<CriticalDetailDto>;
            if (CriticalDetaillist == null)
            {
                CriticalDetaillist = new List<CriticalDetailDto>();
            }
            CriticalDetailDto criticalDetail = new CriticalDetailDto();
            criticalDetail.relations = "并且";
            CriticalDetaillist.Add(criticalDetail);
            gridCriDital.DataSource = CriticalDetaillist;
            gridCriDital.RefreshDataSource();
            gridCriDital.Refresh();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            var currentItem = gridView1.GetFocusedRow() as CriticalDetailDto;
            if (currentItem == null)
                return;

            var dataresult = gridCriDital.DataSource as List<CriticalDetailDto>;
            dataresult.Remove(currentItem);
            gridCriDital.DataSource = dataresult;
            gridCriDital.RefreshDataSource();
            gridCriDital.Refresh();
        }
    }
}
