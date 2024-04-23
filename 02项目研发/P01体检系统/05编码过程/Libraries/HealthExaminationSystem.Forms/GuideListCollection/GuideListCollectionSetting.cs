using DevExpress.XtraEditors;
using gregn6Lib;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection;
using Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.GuideListCollection
{
    public partial class GuideListCollectionSetting : UserBaseForm
    {
        private readonly GroupReportAppService _GroupReportAppService;
        private readonly GuideListCollectionAppService _GuideListCollectionAppService;
        GridppReport Report = new GridppReport();
        public GuideListCollectionSetting()
        {
            InitializeComponent();
            _GroupReportAppService = new GroupReportAppService();
            _GuideListCollectionAppService = new GuideListCollectionAppService();
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            InitForm();

        }
        private void GuideListCollectionSetting_Load(object sender, EventArgs e)
        {
            var rep = GridppHelper.GetTemplate("导引单领取.grf");
            Report.LoadFromURL(rep);
        }
        private void Seach()
        {

                Report.Print(true);

            
        }
        private void ReportFetchRecord()
        {
            CustomerRegQuery dto = new CustomerRegQuery();
            if (textEditCompany.EditValue != null)
            {
                dto.ClientInfoId = Guid.Parse(textEditCompany.EditValue.ToString());
            }
            var charge = _GuideListCollectionAppService.PrintCompanyName(dto);
            if (charge != null)
            {
                foreach (var item in charge)
                {
                    Report.DetailGrid.Recordset.Append();
                    Report.FieldByName("姓名").AsString = item.Customer.Name;
                    if (item.Customer.Sex != 0)
                    {
                        Report.FieldByName("性别").AsString = item.Customer.Sex == 1 ? "男" : "女";
                    }
                    if (item.GuidanceNum != null)
                    {
                        Report.FieldByName("导引单号").AsString = string.Concat(item.GuidanceNum);

                    }
                    if(item.GuidanceNum == null)
                    {
                        Report.FieldByName("导引单号").AsString = "";
                    }


                    Report.DetailGrid.Recordset.Post();
                }
            }

        }

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void InitForm()
        {

            try
            {
                var clientreglist = _GroupReportAppService.QueryCompany();
                textEditCompany.Properties.DataSource = clientreglist;

            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }

        }

        private void query()
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

                PageInputDto<CustomerRegQuery> dto = new PageInputDto<CustomerRegQuery>();
                CustomerRegQuery Query = new CustomerRegQuery();
                if (textEditCompany.EditValue != null)
                {
                    Query.Id = Guid.Parse(textEditCompany.EditValue.ToString());
                    dto.Input = Query;


                }
                dto.TotalPages = TotalPages;
                dto.CurentPage = CurrentPage;

                var output = _GuideListCollectionAppService.QueryCompanyName(dto);

                if (output != null)
                {
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator1);
                    gridControl1.DataSource = output.Result;
                }

                //gridControl1.DataSource = Select;

            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
            gridViewCustomerReg.Columns[CustomerSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridViewCustomerReg.Columns[CustomerSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void textEditCompany_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEditCompany.EditValue != null)
            {
                query();
            }
        }

        private void simpleButtonPring_Click(object sender, EventArgs e)
        {
            Seach();
            
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            query();
        }

        
    }
}
