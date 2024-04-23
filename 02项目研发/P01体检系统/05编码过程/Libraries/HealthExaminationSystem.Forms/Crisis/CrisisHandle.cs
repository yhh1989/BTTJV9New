using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public delegate void SendEventHandler(CrisisCustomerDto CrisisCustomerDto);
    public delegate void SendEventCrirsGen(CrisisCustomerDto CrisisCustomerDto); 
    public partial class handle : UserBaseForm
    {
        public event SendEventHandler SendEvent;
        public event SendEventCrirsGen SendEventCrirsGen; 

        private readonly ICommonAppService _commonAppService;
        private readonly Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis.CrisisAppService _crisisappservice;
       //private readonly ICrisisAppService

        public handle()
        {
            _crisisappservice = new Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis.CrisisAppService();
            //_addStatisticsAppService = new AddStatisticsAppService();

            InitializeComponent();
        } 

        private void staffListControl1_Load(object sender, EventArgs e)
        {

        }

       

        private void CrisisHandle_Load(object sender, EventArgs e)
        {
                    
            var Groups= DefinedCacheHelper.GetItemGroups();
            Group.Properties.DataSource = Groups;
            Group.Properties.ValueMember = "Id";
            Group.Properties.DisplayMember = "ItemGroupName";
            var Items= DefinedCacheHelper.GetItemInfos();
            Item.Properties.DataSource = Items;
            Item.Properties.ValueMember = "Id";
            Item.Properties.DisplayMember = "Name";
            var departments = DefinedCacheHelper.GetDepartments();
            department.Properties.DataSource = departments;
            department.Properties.ValueMember = "Id";
            department.Properties.DisplayMember = "Name";

            //初始化  时间
            //var date = _commonAppService.GetDateTimeNow().Now;
            //Startdate.DateTime = date;
            //Enddate.DateTime = date;
            //展示数据
            LoadData();
           
            //end
         
        }

        public void LoadData()
        {

            dxErrorProvider.ClearErrors();
            gridControlData.DataSource = null;

            try
            {
                AutoLoading(() =>
                {                  
                    var output = _crisisappservice.PageFulls(new CrisVisitSelectDto{ });
                    gridControlData.DataSource = output;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }




        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControlData_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {          
            var input = new CrisVisitSelectDto();
            input.CustomerBM = Serialnumber.Text;
            input.Name = TxtName.Text;
            //科室
            if (!string.IsNullOrWhiteSpace(department.EditValue?.ToString()))
            {
                input.DepartmentBM=Guid.Parse(department.EditValue?.ToString());
            };
            //体检项目
            if (!string.IsNullOrWhiteSpace(Item.EditValue?.ToString()))
            {
                input.ItemId = Guid.Parse(Item.EditValue?.ToString());
            };
            //体检组合
            if (!string.IsNullOrWhiteSpace(Group.EditValue?.ToString()))
            {
                input.GroupId = Guid.Parse(Group.EditValue?.ToString());
            };
            if (Startdate.EditValue != null)
            {
                var date = Startdate.DateTime.AddDays(-1);
                input.StartTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            }
            if (Enddate.EditValue != null)
            {
                var date = Enddate.DateTime.AddDays(1);
                input.EndTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            }

            gridControlData.DataSource = _crisisappservice.PageFulls(input);
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var ClickObject = gridView3.GetRow(gridView3.GetFocusedDataSourceRowIndex());                                
            CrirsGen crirsGen = new CrirsGen(this);
            this.SendEventCrirsGen(Newtonsoft.Json.JsonConvert.DeserializeObject<CrisisCustomerDto>(Newtonsoft.Json.JsonConvert.SerializeObject(ClickObject)));
            crirsGen.ShowDialog();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var fileName = saveFileDialog.FileName;
            ExcelHelper.ExportToExcel(fileName, gridControlData);
        }

        private void handle_Shown(object sender, EventArgs e)
        {
            //页面加载查询危急值
            var result = _crisisappservice.GetCrisisCustome().ToList();
            if (result.Count > 0)
            {
                CrisisLoad crisisLoad = new CrisisLoad(result.FirstOrDefault());               
                crisisLoad.ShowDialog();
                return;
            }

            LoadData();

        }

        private void dataNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            simpleButton1_Click(sender, e);
        }
    }


}
