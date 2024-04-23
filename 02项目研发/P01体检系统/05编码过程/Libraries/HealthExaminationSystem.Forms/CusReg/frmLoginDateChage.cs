using DevExpress.ExpressApp;
using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class frmLoginDateChage : UserBaseForm
    {
        private List<SexModel> sexModels;
        private ICrossTableAppService crossTableAppService;
        private readonly ICommonAppService _commonAppService;
        public frmLoginDateChage()
        {
            InitializeComponent();
            crossTableAppService = new CrossTableAppService();
            _commonAppService = new CommonAppService();
            sexModels = SexHelper.GetSexModels();
        }
        private string FormatCheckState(object arg)
        {
            return CheckSateHelper.PhysicalEStateFormatter(arg);
        }
        private string FormatSex(object arg)
        {
            try
            {
                return sexModels.Find(s => s.Id == (int)arg).Display;
            }
            catch
            {
                return arg.ToString();
            }
        }
        private void frmLoginDateChage_Load(object sender, EventArgs e)
        {


            dateEditStartTime.EditValue = DateTime.Now;
            dateEditEndTime.EditValue = DateTime.Now;

            gridView1.Columns[Sex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[Sex.FieldName].DisplayFormat.Format = new CustomFormatter(FormatSex);
            gridView1.Columns[TijianState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[TijianState.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckState);
        }

        private void simpleButtonChaxun_Click(object sender, EventArgs e)
        {
            if (dateEditEndTime.EditValue != null && dateEditStartTime.EditValue != null)
            {
                if (dateEditEndTime.DateTime < dateEditStartTime.DateTime)
                {
                    ShowMessageBoxWarning("结束时间大于开始时间，请重新选择。");
                    return;
                }
            }

            LoadData(1);
        }
        /// <summary>
        /// 数据加载
        /// </summary>
        private void LoadData(int? curentPage)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            var selectIndex = gridView1.GetFocusedDataSourceRowIndex();
            gridControlCustomerInfo.DataSource = null;
            var input = new PageInputDto<QueryInfoDto> { TotalPages = TotalPages, CurentPage = CurrentPage };
            if (curentPage != null)
                input.CurentPage = curentPage.Value;
            QueryInfoDto queryInput = new QueryInfoDto();           
            if (dateEditStartTime.EditValue != null)
                queryInput.StartDate = Convert.ToDateTime(dateEditStartTime.EditValue).Date;
            if (dateEditEndTime.EditValue != null)
                queryInput.EndDate = Convert.ToDateTime(dateEditEndTime.EditValue).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            if (!string.IsNullOrWhiteSpace((string)textEditTijianhao.EditValue))
                queryInput.CustomerRegNum = textEditTijianhao.Text.Trim();
           
            input.Input = queryInput;
            try
            {
                var queryResult = crossTableAppService.QueryCustomerInfo(input);
                TotalPages = queryResult.CustomerReg.TotalPages;
                CurrentPage = queryResult.CustomerReg.CurrentPage;
                InitialNavigator(dataNavigator1);
                gridControlCustomerInfo.DataSource = queryResult.CustomerReg.Result;
                gridView1.FocusedRowHandle = selectIndex;
               
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
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

        private void dataNavigator1_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            LoadData(null);
        }

        private void textEditTijianhao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textEditTijianhao.Text.Trim()))
                {
                    textEditTijianhao.Focus();
                    return;
                }
                LoadData(null);
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                if (currentDate == null)
                    return;
                if (checkEditMorenJiaobiao.Checked == true)
                {
                    Jiaobiao();
                }
                var input = new PageInputDto<QueryInfoDto> { TotalPages = TotalPages, CurentPage = CurrentPage };
                input.Input = new QueryInfoDto();
                input.Input.StartDate = currentDate.LoginDate.Value.Date;
                input.Input.EndDate = currentDate.LoginDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                input.Input.CustomerRegNum = textEditTijianhao.Text.Trim();
                
                try
                {
                    var queryResult = crossTableAppService.QueryCustomerInfo(input);
                    TotalPages = queryResult.CustomerReg.TotalPages;
                    CurrentPage = queryResult.CustomerReg.CurrentPage;
                    InitialNavigator(dataNavigator1);
                    gridControlCustomerInfo.DataSource = queryResult.CustomerReg.Result;
                    
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }

                textEditTijianhao.Focus();
                textEditTijianhao.SelectAll();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
        public void Jiaobiao()
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (currentDate.SummSate == (int)SummSate.Audited ||
                currentDate.SummSate == (int)SummSate.HasInitialReview)
            {
                ShowMessageBoxInformation("已总检不能修改到检时间！");
                return;
            }           
            try
            {
                var result = crossTableAppService.EditCheckDateState(new CustomerRegForCrossTableDto { Id = currentDate.Id});
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;
                createOpLogDto.LogText = "到检确定";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                _commonAppService.SaveOpLog(createOpLogDto);               
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }
        public void Jiaobiaop()
        {
            var selectIndexes = gridControlCustomerInfo.GetSelectedRowDtos<CustomerRegForCrossTableViewDto>();
            if (selectIndexes == null)
            {
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                else
                {
                    selectIndexes = new List<CustomerRegForCrossTableViewDto>();
                    selectIndexes.Add(currentDate);

                }

            }
            if (selectIndexes.Count == 0)
            {
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                else
                {
                    selectIndexes.Add(currentDate);

                }


            }
            string arNolSIt = "";
            foreach (var currentDate in selectIndexes)
            {
                
                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                if (currentDate.SummSate == (int)SummSate.Audited ||
               currentDate.SummSate == (int)SummSate.HasInitialReview)
                {
 
                    arNolSIt += currentDate.CustomerBM + "已总检不能修改到检时间!";
                    // ShowMessageBoxInformation("选定记录已是\"已交表\"状态！");
                    continue;
                }
                currentDate.SendToConfirm = (int)SendToConfirm.Yes;
                try
                {
                    var result = crossTableAppService.EditCheckDateState(new CustomerRegForCrossTableDto { Id = currentDate.Id });
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = currentDate.CustomerBM;
                    createOpLogDto.LogName = currentDate.Customer.Name;
                    createOpLogDto.LogText = "到检确认" ;
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ResId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                   
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
            if (arNolSIt != "")
            {
                MessageBox.Show(arNolSIt);
            }
            LoadData(null);
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Jiaobiaop();
        }
    }
}
