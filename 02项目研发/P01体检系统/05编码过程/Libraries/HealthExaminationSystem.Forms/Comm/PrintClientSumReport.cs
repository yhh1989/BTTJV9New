using Abp.Application.Services.Dto;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
  public   class PrintClientSumReport
    {
        private GridppReport ReportMain;
        public CusNameInput cusNameInput;
        CustomerRegDto lstCustomerDtos;

        IGroupReportAppService PrintPreviewAppService = new GroupReportAppService();

        private IClientRegAppService CRegAppService;
        /// <summary>
        /// 个人头像
        /// </summary>
        PictureDto url;
        public PrintClientSumReport()
        {
            CRegAppService = new ClientRegAppService();
            ReportMain = new GridppReport();
        }
        /// <summary>
        /// 打印表格体检
        /// </summary>
        /// <param name="isPreview">是否预览，是则预览，否则打印</param>
        public void Print(EntityDto<Guid> input,bool isPreview, string strBarPrintName, string path)
        {
            ClientRegDto clientRegDto = CRegAppService.GetClientRegByID(input);
            sumReportJson sumReportJson = new sumReportJson();
          var outru=  PrintPreviewAppService.getClientSumResult(input);
            sumReportJson.Master = new List<cusMaster>();
            cusMaster cusMaster = new cusMaster();
            cusMaster.ClientName = clientRegDto.ClientInfo.ClientName;
            cusMaster.StarTime = clientRegDto.StartCheckDate.ToShortDateString();
            cusMaster.EndTime = clientRegDto.EndCheckDate.ToShortDateString(); ;
            sumReportJson.Master.Add(cusMaster);
            if (outru != null && outru.Count > 0)
            {
                sumReportJson.Detail = new List<SumClientDto>();
                sumReportJson.Detail.AddRange(outru);
            }
             var GrdPath = GridppHelper.GetTemplate("团报分析解答.grf");
           
            ReportMain.LoadFromURL(GrdPath);
            var reportJsonStringsy = JsonConvert.SerializeObject(sumReportJson);
        
            ReportMain.LoadDataFromXML(reportJsonStringsy);


     
            try
            {
                if (isPreview)
                    ReportMain.PrintPreview(true);
                else
                {
                    if (path != "")
                    {
                        
                        ReportMain.ExportDirect(GRExportType.gretXLS, path, false, false);

                    }
                    else
                        ReportMain.Print(false);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }

        /// <summary>
        /// 报表
        /// </summary>
        public class sumReportJson
        {
            /// <summary>
            /// 参数
            /// </summary>
            public List<cusMaster> Master { get; set; }

            /// <summary>
            /// 明细网格
            /// </summary>
            public List<SumClientDto> Detail { get; set; }
        }
        /// <summary>
        /// 报表参数
        /// </summary>
        public class cusMaster
        {
            /// <summary>
            /// 单位名称
            /// </summary>
            public string ClientName { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public string StarTime { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public string EndTime { get; set; }

        }
    }
}
