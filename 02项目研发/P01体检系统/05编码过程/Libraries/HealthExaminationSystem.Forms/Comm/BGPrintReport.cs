using Abp.Application.Services.Dto;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
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
using System.Windows;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{

    public class BGPrintReport
    {
        private GridppReport ReportMain;
        public CusNameInput cusNameInput;
        CustomerRegDto lstCustomerDtos;
        /// <summary>
        /// 检查项目表
        /// </summary>
        List<TjlCustomerRegItemReoprtDto> lstCustomerRegItemReoprtDto;
        /// <summary>
        /// 检查项目组合
        /// </summary>
        List<CustomerItemGroupPrintViewDto> lstCustomerItemGroupPrintViewDto;
        IInspectionTotalAppService inspectionTotalAppService;
        IPrintPreviewAppService PrintPreviewAppService = new PrintPreviewAppService();
        TjlCustomerSummarizeDto tjlCustomerSummarizeDto;
        private IDoctorStationAppService CustomerItemPic;//图片
        List<CustomerDepSummaryViewDto> lstCustomerDepSummaries;
        PictureController _pictureController;
        List<TjlCustomerSummarizeBMViewDto> tjlCustomerSummarizeBMViewDtos;
        List<TjlCustomerSummarizeBMViewDto> CustomerHistorySummarizeBMDtos;
        List<ItemInfoSimpleDto> itemInfoSimpleDtosall;
        private InputOccCusSumDto inputOccCusSumDto = new InputOccCusSumDto();
        private readonly ICommonAppService _commonAppService = new CommonAppService();

        private readonly IPrintPreviewAppService _printPreviewAppService = new PrintPreviewAppService();
        /// <summary>
        /// 个人头像
        /// </summary>
        PictureDto url;
        public BGPrintReport()
        {
            ReportMain = new GridppReport();
        }
        /// <summary>
        /// 打印表格体检
        /// </summary>
        /// <param name="isPreview">是否预览，是则预览，否则打印</param>
        public void Print(bool isPreview, string strBarPrintName, string path)
        {
            //try
            //{

                //获取基本信息
                ICustomerAppService customerAppService = new CustomerAppService();
                inspectionTotalAppService = new InspectionTotalAppService();
                CustomerItemPic = new DoctorStationAppService();
                _pictureController = new PictureController();
                cusNameInput.Theme = "1";
                lstCustomerDtos = customerAppService.GetCustomerRegDto(cusNameInput);

                if (lstCustomerDtos == null)
                {
                    return;
                }
                QueryClass queryClass = new QueryClass();
                queryClass.CustomerRegId = cusNameInput.Id;
                //获取总检建议列表
                tjlCustomerSummarizeBMViewDtos = inspectionTotalAppService.GetlstSummarizeBM(queryClass);
                //检查结果
                lstCustomerRegItemReoprtDto = CustomerItemPic.GetTjlCustomerRegItemReoprtDtos(queryClass).Where(o => o.ProcessState != (int)ProjectIState.Not).OrderBy(n => n.DepartmentBM?.OrderNum).ThenBy(n => n.ItemGroupBM?.OrderNum).ThenBy(o => o.ItemOrder).ToList();
                //获取总检信息
                TjlCustomerQuery tjlCustomerQuery = new TjlCustomerQuery();
                tjlCustomerQuery.CustomerRegID = cusNameInput.Id;
                tjlCustomerSummarizeDto = inspectionTotalAppService.GetSummarize(tjlCustomerQuery);
                //获取所有组合小结
                lstCustomerItemGroupPrintViewDto = CustomerItemPic.GetCustomerItemGroupPrintViewDtos(queryClass).Where(n => n.CheckState != (int)ProjectIState.GiveUp).ToList();
                //获取科室小结
                lstCustomerDepSummaries = CustomerItemPic.GetCustomerDepSummaries(queryClass);
                //个人头像
                if (lstCustomerDtos.Customer.CusPhotoBmId.HasValue && lstCustomerDtos.Customer.CusPhotoBmId != Guid.Empty)
                {
                    url = _pictureController.GetUrl(lstCustomerDtos.Customer.CusPhotoBmId.Value);

                }
                DataTable dataTable = GetDateTable();
                DataRow dataRow = dataTable.NewRow();
                dataRow["体检号"] = lstCustomerDtos.CustomerBM;
                dataRow["姓名"] = lstCustomerDtos.Customer.Name;
                dataRow["性别"] = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex).Replace("性", "");
                dataRow["年龄"] = lstCustomerDtos.Customer.Age;
                dataRow["电话"] = lstCustomerDtos.Customer.Mobile;
            dataRow["民族"] = lstCustomerDtos.Customer.Nation;
            dataRow["部门"] = lstCustomerDtos.Customer.Department;
            dataRow["照片"] = url?.RelativePath;

                dataRow["单位"] = lstCustomerDtos.ClientReg?.ClientInfo.ClientName;
            dataRow["出生日期"] = lstCustomerDtos.Customer.Birthday;
            dataRow["身份证号"] = lstCustomerDtos.Customer.IDCardNo;
                dataRow["套餐"] = lstCustomerDtos.ItemSuitName;
                dataRow["备注"] = lstCustomerDtos.Customer.Remarks;
                dataRow["地址"] = lstCustomerDtos.Customer.Address;
                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                dataRow["体检类别"] = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;
                dataRow["体检日期"] = lstCustomerDtos.LoginDate;
                dataRow["总检汇总"] = tjlCustomerSummarizeDto?.CharacterSummary;
                var adviceform = FormatAdvice();
                dataRow["总检建议"] = adviceform.adviceContent;
                dataRow["诊断"] = adviceform.adviceName;

                dataRow["总检医生"] = tjlCustomerSummarizeDto?.EmployeeBM.Name;
                dataRow["审核医生"] = tjlCustomerSummarizeDto?.ShEmployeeBM.Name;
                dataRow["总检时间"] = tjlCustomerSummarizeDto?.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
            //检查结果
            var reitemName = lstCustomerRegItemReoprtDto.GroupBy(o => o.ItemName).Select(
                o => new { itemname= o.Key, count = o.Count() }).ToList();
            var reitems = reitemName.Where(p => p.count > 1).Select(o=>o.itemname).ToList();
            foreach (var cusitem in lstCustomerRegItemReoprtDto)
            {
                var conitemname = "";
                if (reitems.Contains(cusitem.ItemName))
                {
                    conitemname = cusitem.ItemGroupBM.ItemGroupName + cusitem.ItemName;
                }
                else
                {
                    conitemname = cusitem.ItemName;
                }
                if (!dataTable.Columns.Contains(conitemname + "结果"))
                {

                    dataTable.Columns.Add(conitemname + "结果");
                    dataTable.Columns.Add(conitemname + "诊断");
                    dataTable.Columns.Add(conitemname + "单位");
                    dataTable.Columns.Add(conitemname + "参考值");
                    dataTable.Columns.Add(conitemname + "标识");
                    dataRow[conitemname + "结果"] = cusitem.ItemResultChar;
                    dataRow[conitemname + "诊断"] = cusitem.ItemDiagnosis;
                    dataRow[conitemname + "单位"] = cusitem.Unit;
                    dataRow[conitemname + "参考值"] = cusitem.Stand;
                    switch (cusitem.Symbol)
                    {
                        case "H":
                            dataRow[conitemname + "标识"] = "↑";
                            break;
                        case "HH":
                            dataRow[conitemname + "标识"] = "↑↑";
                            break;
                        case "L":
                            dataRow[conitemname + "标识"] = "↓";
                            break;
                        case "LL":
                            dataRow[conitemname + "标识"] = "↓↓";
                            break;
                    }

                }
            }
            //科室小结
            foreach (var cusDepart in lstCustomerDepSummaries)
            {
                if (!dataTable.Columns.Contains(cusDepart.DepartmentName + "科室小结"))
                {
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室小结");
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室医生");
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室时间");

                    dataRow[cusDepart.DepartmentName + "科室小结"] = cusDepart.DagnosisSummary;
                    dataRow[cusDepart.DepartmentName + "科室医生"] = cusDepart.ExamineEmployeeBM?.Name;
                    dataRow[cusDepart.DepartmentName + "科室时间"] = cusDepart.CheckDate;
                }
            }
            //组合
            foreach (var cusGroup in lstCustomerItemGroupPrintViewDto)
            {
                if (!dataTable.Columns.Contains(cusGroup.ItemGroupName + "组合小结"))
                {
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合小结");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合检查医生");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合审核医生");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合检查时间");
                    dataRow[cusGroup.ItemGroupName + "组合小结"] = cusGroup.ItemGroupDiagnosis;
                    dataRow[cusGroup.ItemGroupName + "组合检查医生"] = cusGroup.CheckEmployeeBM?.Name;
                    dataRow[cusGroup.ItemGroupName + "组合审核医生"] = cusGroup.InspectEmployeeBM?.Name;
                    dataRow[cusGroup.ItemGroupName + "组合检查时间"] = cusGroup.FirstDateTime;
                }
            }

                dataTable.Rows.Add(dataRow);
                var GrdPath = GridppHelper.GetTemplate(strBarPrintName.Replace("\r\n", ""));
                if (!GrdPath.Contains(".grf"))
                {
                    GrdPath = GrdPath + ".grf";
                }
                ReportMain.LoadFromURL(GrdPath);
                var reportJsonStringsy = JsonConvert.SerializeObject(dataTable);
                var reportJson = "{\"Detail\":" + reportJsonStringsy + "}";
                ReportMain.LoadDataFromXML(reportJson);
          
            //打印后事件
            ReportMain.PrintEnd += () =>
            {
                _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = lstCustomerDtos.Id });
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = lstCustomerDtos.CustomerBM;
                createOpLogDto.LogName = lstCustomerDtos.Customer.Name;
                createOpLogDto.LogText = "打印体检报告";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.PrintId;
                _commonAppService.SaveOpLog(createOpLogDto);

            };

            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 80)?.Remarks;

                if (!string.IsNullOrEmpty(printName))
                {
                    ReportMain.Printer.PrinterName = printName;
                }
                try
                {
                    if (isPreview)
                        ReportMain.PrintPreview(true);
                    else
                    {
                    if (path != "")
                    {
                        ReportMain.ExportDirect(GRExportType.gretPDF, path, false, false);

                    }
                    else
                        ReportMain.Print(false);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }
        private adviceNames FormatAdvice()
        {
            adviceNames adviceNames = new adviceNames();
            string strAdvice = "";
            //建议格式
            string atrAdviceType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 160).Remarks;
            string stradviceitem = "";
            //诊断格式
            string stradviceNames = "";
            string atrAdviceNameType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 161)?.Remarks;
            string stradviceName = "";
            if (atrAdviceNameType == null)
            {
                atrAdviceNameType = "";
            }
            if (tjlCustomerSummarizeBMViewDtos.Count > 0)
            {
                foreach (var item in tjlCustomerSummarizeBMViewDtos.Where(o => o.ParentAdviceId == Guid.Empty || o.ParentAdviceId == null).OrderBy(o => o.SummarizeOrderNum))
                {  //建议内容
                    stradviceitem = atrAdviceType.Replace("【序号】",
                      item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                      item.SummarizeName.Trim()).Replace("【建议内容】", item.Advice.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                    strAdvice += stradviceitem;
                    //诊断格式
                    stradviceName = atrAdviceNameType.Replace("【序号】",
                    item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                    item.SummarizeName.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                    stradviceNames += stradviceName;

                    var Children = tjlCustomerSummarizeBMViewDtos.Where(o => o.ParentAdviceId == item.Id)?.OrderBy(o => o.SummarizeOrderNum).ToList();
                    if (Children != null && Children.Count() > 0)
                    {
                        foreach (var itemChildren in Children)
                        {
                            //建议格式
                            stradviceitem = atrAdviceNameType.Replace("【序号】",
                             "").Replace("【建议名称】",
                             itemChildren.SummarizeName.Trim()).Replace("【建议内容】", itemChildren.Advice.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                            strAdvice += stradviceitem;

                            //诊断格式
                            stradviceName = atrAdviceType.Replace("【序号】",
                            item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                            item.SummarizeName.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                            stradviceNames += stradviceName;
                        }
                    }
                }

            }
            adviceNames.adviceContent = strAdvice;
            adviceNames.adviceName = stradviceNames;
            if (adviceNames.adviceName == "")
            {
                var adls = tjlCustomerSummarizeBMViewDtos.OrderBy(r => r.SummarizeOrderNum).Select(o => (o.SummarizeOrderNum + "、" + o.SummarizeName));
                adviceNames.adviceName = string.Join("\r", adls);
            }
            return adviceNames;
        }

        private DataTable GetDateTable()//生成通用存放数据的Table
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("体检号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("性别");
            dt.Columns.Add("年龄");
            dt.Columns.Add("电话");
            dt.Columns.Add("单位");
            dt.Columns.Add("手机号");
            dt.Columns.Add("出生日期");

            dt.Columns.Add("身份证号");
            dt.Columns.Add("套餐");
            dt.Columns.Add("备注");
            dt.Columns.Add("地址");
            dt.Columns.Add("照片");
            dt.Columns.Add("体检类别");
            dt.Columns.Add("体检日期");
            dt.Columns.Add("总检汇总");
            dt.Columns.Add("总检建议");
            dt.Columns.Add("诊断");
            dt.Columns.Add("总检医生");
            dt.Columns.Add("审核医生");
            dt.Columns.Add("总检时间");
            dt.Columns.Add("民族");
            dt.Columns.Add("部门");
            return dt;
        }

    }
}
