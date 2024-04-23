using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using Abp.Application.Services.Dto;
using gregn6Lib;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
   
    /// <summary>
    /// 打印导引单
    /// </summary>
    public class PrintGuidanceNew
    {
      
        /// <summary>
        /// 检查子报表
        /// </summary>
        private const string SUB_REPORT_EXAMINE = "SubReportExamine";

        /// <summary>
        /// 检验子报表
        /// </summary>
        private const string SUB_REPORT_TEST = "SubReportTest";
        /// <summary>
        /// 检验子报表
        /// </summary>
        private const string SUB_REPORT_TEST1 = "SubReportTest1";

        /// <summary>
        /// 彩超子报表
        /// </summary>
        private const string SUB_REPORT_COLOR_ULTRASOUND = "SubReportColorUltrasound";

        /// <summary>
        /// 放射子报表
        /// </summary>
        private const string SUB_REPORT_RADIATE = "SubReportRadiate";

        /// <summary>
        /// 功能子报表
        /// </summary>
        private const string SUB_REPORT_FUNCTION = "SubReportFunction";

        /// <summary>
        /// 其它子报表
        /// </summary>
        private const string SUB_REPORT_OTHER = "SubReportOther";
        /// <summary>
        /// 正常子报表
        /// </summary>
        private const string SUB_REPORT_Normal = "SubReportNormal";
        /// <summary>
        /// 加项子报表
        /// </summary>
        private const string SUB_REPORT_Add = "SubReportAdd";
        //体检预约

        public static string Print(Guid customRegId, bool preview = false, bool printDialog = false,bool isAdd=false,bool isBJ=false)
        {
           
            string ret = "";
            IBarPrintAppService barPrintAppService = new BarPrintAppService();
            ICrossTableAppService crossTableAppService = new CrossTableAppService();
            PictureController _pictureController = new PictureController();
            ICustomerAppService customerAppService = new CustomerAppService();
            IDoctorStationAppService doctorStationAppService = new DoctorStationAppService();

            var cusNameInput = new CusNameInput { Id = customRegId, Theme = "1" };
            var customerReg = customerAppService.GetCustomerRegDto(cusNameInput);
            var tjcsstr = "1";
            string OldCusBm = "";
            DateTime? OldLastTime = null;
            if (!string.IsNullOrEmpty(customerReg.Customer.IDCardNo))
            {
                var cusName = new CusNameInput { Id = customRegId, Theme = customerReg.Customer.IDCardNo };
                var tjcs = customerAppService.GetCustomerRegCountDto(cusName);
                tjcsstr = tjcs.Theme;

                var oldcus = customerAppService.GetOldCustomerReg(cusName);
                OldCusBm = oldcus.CusRegBM;
                OldLastTime = oldcus.Custime;
            }
            //按体检类别绑定模板
            var gridppTemplateFileName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 1000).Remarks;
            var templist = gridppTemplateFileName.Split('|').Where(p=>p!="").ToList();
            var mbName = gridppTemplateFileName;
            if (templist.Count > 0)
            {
                mbName = templist[0];
            }
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == customerReg.PhysicalType)?.Text;
            if (!string.IsNullOrWhiteSpace(tjlb) && templist != null)
            {
                var tlist = templist.Where(o => o.Contains(tjlb)).ToList();
                if (tlist.Count > 0)
                {
                    if (!tlist[0].Contains(".grf"))
                    { tlist[0] += ".grf"; }
                    mbName = tlist[0];
                }

            }
            var gridppUrl = GridppHelper.GetTemplate(mbName);
            var cuspay = customerAppService.GetCusPayMoney(cusNameInput);       
          
            var reportJson = new ReportMinJson();
            reportJson.Master = new List<Master>();
            var master = new Master();
            #region HIS接口
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                if (HISName == "北仑"  || HISName=="江苏鑫亿" || HISName == "东软宜兴善卷骨科")
                {
                    //获取未收费或最新申请单号
                    ICustomerAppService _customerSvr = new CustomerAppService();
                    TJSQDto input = new TJSQDto();
                    input.CustomerRegId = customerReg.Id;
                    var aplay = _customerSvr.getapplication(input);
                    if (aplay.Count > 0)
                    {
                        var app = aplay.FirstOrDefault(o => o.SQSTATUS == 1);
                        if (app != null)
                        {
                            master.AppliyNum = app.ApplicationNum;
                        }
                        else
                        {
                            master.AppliyNum = aplay.OrderByDescending(o => o.CreatTime).First().ApplicationNum;
                        }
                    }
                       
                }
                if (HISName == "东软")
                {
                    //获取未收费或最新申请单号
                    ICustomerAppService _customerSvr = new CustomerAppService();
                    TJSQDto input = new TJSQDto();
                    input.CustomerRegId = customerReg.Id;
                    var aplay = _customerSvr.getapplication(input);
                    if (aplay.Count > 0)
                    {
                        string HisSfurl = "http://tj.wh5yuan.com.cn:5000/pay?recipeNo=";
                        var SFURl = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 4)?.Remarks;
                        if (!string.IsNullOrEmpty(SFURl))
                        {
                            HisSfurl = SFURl;
                        }
                        var app = aplay.FirstOrDefault(o => o.SQSTATUS == 1);
                        if (app != null)
                        {
                            master.AppliyNum = HisSfurl + app.Remark;

                        }
                        else
                        {
                            master.AppliyNum = HisSfurl + aplay.OrderByDescending(o => o.CreatTime).First().Remark;
                        }
                    }

                }
            }
            #endregion
            var queryClass = new QueryClass();
            queryClass.CustomerRegId = customRegId;
            var itemGroups = doctorStationAppService.GetATjlCustomerItemGroupPrintGuidanceDto(queryClass).Where(r => r.IsAddMinus != (int)AddMinusType.AdjustAdd && r.IsAddMinus != (int)AddMinusType.Minus).OrderBy(r => r.DepartmentBM.OrderNum).ThenBy(r => r.ItemGroupBM.OrderNum).ToList();
            //放弃不打印导引单
            var fqxs = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => 
            o.Type == BasicDictionaryType.GuidanceSet.ToString() && o.Value==77)?.Remarks;
            if (!string.IsNullOrEmpty(fqxs) && fqxs == "1")
            { 
                itemGroups = itemGroups.Where(r => r.CheckState != (int)ProjectIState.GiveUp).OrderBy(r => r.DepartmentBM.OrderNum).ThenBy(r => r.ItemGroupBM.OrderNum).ToList();
            }
            //空腹排序
            var fqkf = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o =>
            o.Type == BasicDictionaryType.GuidanceSet.ToString() && o.Value == 78)?.Remarks;
            if (!string.IsNullOrEmpty(fqkf) && fqkf == "1")
            {
                itemGroups = itemGroups.OrderBy(p=> p.ItemGroupBM.MealState).ThenBy(r => r.DepartmentBM.OrderNum).ThenBy(r => r.ItemGroupBM.OrderNum).ToList();
            }
            #region 导引单不显示的组合
            var GroupHide = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 12)?.Remarks;
            if (!string.IsNullOrEmpty(GroupHide))
            {
                var grouplist = GroupHide.Replace(",", "|").Replace("，", "|").Replace(";","|").Replace("；","|").Split('|').ToList();
                itemGroups = itemGroups.Where(p=> !grouplist.Contains( p.ItemGroupName)).ToList();
            }

            #endregion
            //追加导引单
            if (isAdd == true)
            {
                itemGroups = itemGroups.Where(p => p.GuidanceSate != (int)PrintSate.Print).ToList();
            }
            if (isBJ == true)
            {
                itemGroups = itemGroups.Where(p=>p.CheckState == (int)ProjectIState.Stay).ToList();
            }
            //如果打印，生成导引单号
            if (preview==false)
            {
                //更改customerreg表导引单打印状态、导引单号（单位累加、个人当天累加）
                //更改customerregitemgroup表导引单打印状态
                GuideUpdateCustomerRegDto regDto = new GuideUpdateCustomerRegDto();
                
                regDto.ClientRegId = customerReg.ClientRegId;
                regDto.Id = customerReg.Id;
                customerReg.GuidanceNum = customerAppService.UpdateTimes(regDto);
                //if (queryCustomerRegDto != null)
                //{
                //    queryCustomerRegDto.GuidanceSate= (int)PrintSate.Print;
                //    var grouplist = itemGroups.Select(p=>p.Id).ToList();
                //    if (queryCustomerRegDto.CustomerItemGroup != null)
                //    {
                //        foreach (var cusgroup in queryCustomerRegDto.CustomerItemGroup)
                //        {
                //            cusgroup.GuidanceSate= (int)PrintSate.Print;
                //        }
                //    }

                //}
                master.ParameterSerialNumber = customerReg.GuidanceNum ?? -1;
            }          
            master.ParameterCustomerName = customerReg.Customer.Name;
            master.ParameterCustomerSex = SexHelper.CustomSexFormatter(customerReg.Customer.Sex);
            master.ParameterIDCardNo = customerReg.Customer.IDCardNo;
            master.ParameterSectionNum = customerReg.Customer.SectionNum;
            // 应该根据体检登记日期和生日计算
            master.ParameterCustomerAge = customerReg.Customer.Age ?? -1;
            master.ParameterRegCount = tjcsstr;
            master.OldCusBm = OldCusBm;
            master.OldLastTime = OldLastTime; 
            if (customerReg.Customer.Birthday.HasValue)
            {
                master.ParameterBirthday = customerReg.Customer.Birthday.Value.ToString("yyyy-MM-dd");
            }
            master.ParameterExaminationDate = customerReg.LoginDate ?? DateTime.Now;
            if (customerReg.BookingDate.HasValue)
            {
                master.ParameterExaminationBookingDate = customerReg.BookingDate.Value.ToShortDateString();
            }
            master.ParameterCustomerExaminationNumber = customerReg.CustomerBM;
            master.ParameterCustomerArchNum = customerReg.Customer.ArchivesNum;
            master.VisitCard = customerReg.Customer.VisitCard;
            master.PacsBM = customerReg.PacsBM;
            master.ClientRegNum = customerReg.ClientRegNum;
            master.CustomerRegNum = customerReg.CustomerRegNum;
            if (cuspay != null)
            {
                master.AllMoney = cuspay.PersonalMoney.ToString("0.00");
                master.PayMoney =(cuspay.PersonalMoney- cuspay.PersonalPayMoney).ToString("0.00");
            }
            if (customerReg.ClientReg == null)
            {
                master.ParameterCompanyName = "个人体检";
                master.ParameterIntroducer = customerReg.Introducer;
                master.ParameterReceiving = "自取";
            }
            else
            {
                master.ParameterCompanyName = customerReg.ClientReg.ClientInfo.ClientName;
                master.ParameterIntroducer = customerReg.ClientReg.linkMan;
                master.ParameterReceiving = "单位领取";
            }
            #region 超声排队
            var strCSPD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 111)?.Remarks;

            if (strCSPD!=null && strCSPD == "Y")
            {
                try
                {
                    string strpdsql = "  select * from tj_requireinfo where DepartmentID='1001' and SelfBH='"
                       + customerReg.CustomerBM + "'";
                    SqlConnection tjcon = new SqlConnection(Btpd_base.PublicTool.SqlTj);
                    tjcon.Open();//连接体检数据库
                    SqlCommand tjcom = new SqlCommand(strpdsql, tjcon);
                    SqlDataReader tjread = tjcom.ExecuteReader();
                    if (tjread.HasRows)
                    {

                        Btpd.BLL.UserInfo userinfo = new Btpd.BLL.UserInfo(Btpd_base.PublicTool.SqlConnection);
                        List<Btpd.Model.UserInfo> lsuserinfo = userinfo.GetModelList(" tjcode='" + customerReg.CustomerBM + "'");
                        Btpd.Model.UserInfo EnteruserinfoGd = new Btpd.Model.UserInfo();
                        if (lsuserinfo.Count > 0)
                        {
                            EnteruserinfoGd = lsuserinfo[0];
                            
                            master.ParameterPDXH = EnteruserinfoGd.TeamOrder.ToString();


                        }
                        
                    }

                }
                catch (Exception ex)
                {
                 
                }

            }
            #endregion

            if (customerReg.PhysicalType.HasValue)
            {
                //master.ParameterExaminationType =
                //    PhysicalTypeHelper.PhysicalTypeFormatter(customerReg.PhysicalType.Value);

                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var CheckType = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var ts= CheckType.FirstOrDefault(o => o.Value == customerReg.PhysicalType)?.Text;
                if (!string.IsNullOrEmpty(ts) && !ts.Contains("投诉"))
                {
                    master.ParameterExaminationType = ts;
                }
            }
            //客户类别
            if (customerReg.CustomerType.HasValue)
            {
                 
                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.CustomerType);
                var CheckType = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var ts = CheckType.FirstOrDefault(o => o.Value == customerReg.CustomerType)?.Text;               
                 master.ParameterCustomerType = ts;
                
            }

            
            if (customerReg.PersonnelCategoryId.HasValue)
            {
                IPersonnelCategoryAppService _personnelCategoryAppService= new PersonnelCategoryAppService();
                var  personnelCategoryViewDtos = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
                if (personnelCategoryViewDtos != null && personnelCategoryViewDtos.Count > 0)
                {
                    var cusType = personnelCategoryViewDtos.FirstOrDefault(
                        p => p.Id == customerReg.PersonnelCategoryId)?.Name;
                    if (!string.IsNullOrEmpty(cusType) && !cusType.Contains("投诉"))
                    {
                        master.ParameterCategory = cusType;
                    }
                }
            }
            if (!string.IsNullOrEmpty(customerReg.Customer.Remarks) && !customerReg.Customer.Remarks.Contains("投诉"))
            {
                master.ParameterRemark = customerReg.Customer.Remarks;
            }
            if (!string.IsNullOrEmpty(customerReg.Remarks)&& !customerReg.Remarks.Contains("投诉"))
            {
                master.ParameterRegRemark = customerReg.Remarks;
            }
            
            master.ParameterSuitName = customerReg.ItemSuitName;
            if (customerReg.ClientTeamInfo != null)
            {
                master.ParameterTeamName = customerReg.ClientTeamInfo.TeamName;
            }
            master.ParameterMobile = customerReg.Customer.Mobile;
            master.ParameterAddress = customerReg.Customer.Address;
            master.ParameterDepartment = customerReg.Customer.Department;
            master.ParameterWorkNumber = customerReg.Customer.WorkNumber;
            master.ParameterMarriage = MarrySateHelper.CustomMarrySateFormatter(customerReg.Customer.MarriageStatus);
            master.ParameterNation = customerReg.Customer.Nation;
            master.ParameterTypeWork = customerReg.TypeWork;
            master.RiskS = customerReg.RiskS;
            master.PostState = customerReg.PostState;
            master.FPName = customerReg.FPNo;
         
            //检验科组合放一起
            var Itemjy = string.Join(" ",itemGroups.Where(o=>o.DepartmentName.Contains("检验")).Select(o=>o.ItemGroupName).ToList());
            master.AllPrice = itemGroups.Sum(o=>o.ItemPrice).ToString("0.00");
            master.JYGroups = Itemjy;
            var GDJY= string.Join(" ",  itemGroups.Where(o => o.DepartmentName.Contains("检验")).Select(o => ("口"+ o.ItemGroupName)).ToList());
            reportJson.Master.Add(master);
            //获取待查信息
            var crossTable =new  List<CusGiveUpDto>();
            var ls = itemGroups.Where(o=>o.CheckState== (int)ProjectIState.Stay).ToList();
            if (ls.Count > 0)
            {
                var input = new EntityDto<Guid>();
                input.Id = customRegId;
                crossTable = crossTableAppService.getGiveUps(input);
            }
            //设置头像
            reportJson.Detail = new List<DetailMin>();
            var reportdetail = new DetailMin();
            if (customerReg.Customer.CusPhotoBmId.HasValue && customerReg.Customer.CusPhotoBmId != Guid.Empty)
            {   
                var url = _pictureController.GetUrl(customerReg.Customer.CusPhotoBmId.Value);
                reportdetail.Photo = url.Thumbnail;
                master.ParameterSerialPhoto= url.Thumbnail;


            }
            if (customerReg.PhotoBmId.HasValue && customerReg.PhotoBmId != Guid.Empty)
            {
                
                var url = _pictureController.GetUrl(customerReg.PhotoBmId.Value);
                reportdetail.PhotoReg = url.Thumbnail;
                master.ParameterSerialPhotoReg = url.Thumbnail;


            }
            if (!string.IsNullOrEmpty(reportdetail.Photo) || !string.IsNullOrEmpty(reportdetail.PhotoReg))
            {
                reportJson.Detail.Add(reportdetail);
            }
            #region 抽血组合单独 
            var dydSet = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.GuidanceSet).FirstOrDefault(
               p => p.Value == 1);
            if (dydSet != null && dydSet?.Remarks == "1")
            {
                var BBTypelist = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.SpecimenType).
                    Where(p => p.Text.Contains("血")).ToList();
                if (BBTypelist != null && BBTypelist.Count>=0)
                {
                    var BBType = BBTypelist.Select(p=>p.Value).ToList();
                    var couGroupList = itemGroups.Where(p => p.ItemGroupBM!=null && p.ItemGroupBM.SpecimenType!=null && BBType.Contains( p.ItemGroupBM.SpecimenType.Value)
                      && p.IsAddMinus!= (int)AddMinusType.Add).OrderBy(p=>p.DepartmentOrder).ThenBy(p=>p.ItemGroupOrder).ToList();
                    if (couGroupList.Count > 0)
                    {
                        var GroupIDs = couGroupList.Select(p => p.Id).ToList();
                        var itemGroupsls = itemGroups.ToList();
                        itemGroups.Clear();
                        itemGroups = itemGroupsls.Where(p => !GroupIDs.Contains(p.Id)).ToList();
                        var nowGroup = new ATjlCustomerItemGroupPrintGuidanceDto();
                        nowGroup = couGroupList.FirstOrDefault();
                        nowGroup.DepartmentOrder = 0;
                        nowGroup.ItemGroupOrder = 0;
                        var GroupNames = couGroupList.Select(p => p.ItemGroupName).ToList();
                        nowGroup.ItemGroupName = string.Join(",",GroupNames);
                        itemGroups.Add(nowGroup);
                        itemGroups = itemGroups.OrderBy(p => p.DepartmentOrder).ThenBy(p =>
                           p.ItemGroupOrder).ToList();
                    }

                }
               
            }
            #endregion
            #region 正常项目子报表    
            var itemGroupsForNoShow = itemGroups.ToList();
            
            var departmentsForNoShow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 11)?.Remarks;
            if (!string.IsNullOrEmpty(departmentsForNoShow))
            {
                var departmentsForNoShowlist = departmentsForNoShow.Split(',');
                 itemGroupsForNoShow = itemGroups.Where(r => !departmentsForNoShowlist.Contains(r.DepartmentName)).ToList();
            }

            var itemGroupsForNormal = itemGroupsForNoShow.Where(r =>r.IsAddMinus!= (int)AddMinusType.Add).ToList();
            var reportJsonForNormal = new ReportJson();
            reportJsonForNormal.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForNormal)
            {
                var detail = new Detail();
                detail.ItemGroupName = itemGroup.ItemGroupName;
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;
                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForNormal.Detail.Add(detail);
            }
            #endregion
            #region 加项子报表
            var itemGroupsForAdd = itemGroupsForNoShow.Where(r => r.IsAddMinus == (int)AddMinusType.Add).ToList();
            var reportJsonForAdd = new ReportJson();
            reportJsonForAdd.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForAdd)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForAdd.Detail.Add(detail);
            }
            #endregion
            var departmentsForExamine = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 20).Remarks;
            var departmentsForExaminelist = departmentsForExamine.Split(',');
            var itemGroupsForExamine = itemGroups.Where(r => departmentsForExaminelist.Contains(r.DepartmentName)).ToList();
            var reportJsonForExamine = new ReportJson();
            reportJsonForExamine.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForExamine)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
               // detail.ItemGroupPrice = itemGroupsForExamine.Sum(itemGroup.ItemPrice.ToString("0.00"));
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id)?.stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id)?.remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty= itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForExamine.Detail.Add(detail);
            }

            //var sss = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 30);
            var departmentsForTest = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 30).Remarks;
            var departmentsForTestlist = departmentsForTest.Split(',');
            var itemGroupsForTest = itemGroups.Where(r => departmentsForTestlist.Contains(r.DepartmentName)).ToList();
            var reportJsonForTest = new ReportJson();
            reportJsonForTest.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForTest)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForTest.Detail.Add(detail);
            }

            var departmentsForColorUltrasound = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 40).Remarks;
            var departmentsForColorUltrasoundlist = departmentsForColorUltrasound.Split(',');
            var itemGroupsForColorUltrasound = itemGroups.Where(r => departmentsForColorUltrasoundlist.Contains(r.DepartmentName)).ToList();
            var reportJsonForColorUltrasound = new ReportJson();
            reportJsonForColorUltrasound.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForColorUltrasound)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForColorUltrasound.Detail.Add(detail);
            }

            var departmentsForRadiate = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 50).Remarks;
            var departmentsForRadiatelist = departmentsForRadiate.Split(',');
            var itemGroupsForRadiate = itemGroups.Where(r => departmentsForRadiatelist.Contains(r.DepartmentName)).ToList();
            var reportJsonForRadiate = new ReportJson();
            reportJsonForRadiate.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForRadiate)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForRadiate.Detail.Add(detail);
            }

            var departmentsForFunction = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 60).Remarks;
            var departmentsForFunctionlist = departmentsForFunction.Split(',');
            var itemGroupsForFunction = itemGroups.Where(r => departmentsForFunctionlist.Contains(r.DepartmentName)).ToList();
            var reportJsonForFunction = new ReportJson();
            reportJsonForFunction.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForFunction)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForFunction.Detail.Add(detail);
            }

            var departmentsForOther = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GuidanceSet, 70).Remarks;
            var departmentsForlist = departmentsForOther.Split(',');
            var itemGroupsForOther = itemGroups.Where(r => departmentsForlist.Contains(r.DepartmentName)).ToList();
            var reportJsonForOther = new ReportJson();
            reportJsonForOther.Detail = new List<Detail>();
            foreach (var itemGroup in itemGroupsForOther)
            {
                var detail = new Detail();
                if (itemGroup.ItemGroupBM.MealState.HasValue && itemGroup.ItemGroupBM.MealState == 1)
                {
                    detail.Fasting = "空腹";
                }
                detail.ItemGroupName = itemGroup.ItemGroupName;
                detail.DeparmentName = itemGroup.DepartmentName;
                detail.Notice = itemGroup.ItemGroupBM.Notice;
                detail.ItemPrice = itemGroup.ItemPrice.ToString("0.00");
                detail.PriceAfterDis = itemGroup.PriceAfterDis.ToString("0.00");
                if (itemGroup.CheckState == (int)ProjectIState.Stay)
                {
                    detail.GiveState = "待查";
                    detail.GiveTime = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).stayDate.ToString();
                    detail.GiveRemark = crossTable.FirstOrDefault(o => o.CustomerItemGroupId == itemGroup.Id).remart.ToString();
                }
                if (customerReg.Customer.Sex == (int)Sex.Man)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.MenAddress;
                }
                else if (customerReg.Customer.Sex == (int)Sex.Woman)
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.WomenAddress;
                }
                else
                {
                    detail.DepartmentAddress = itemGroup.DepartmentBM.Address;
                }
                detail.VIPDepartmentAddress = itemGroup.DepartmentBM.Address;

                detail.Duty = itemGroup.DepartmentBM.Duty;
                detail.Remarks = itemGroup.DepartmentBM.Remarks;
                reportJsonForOther.Detail.Add(detail);
            }

            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);
            //report.FieldByName("个人照片").AsString = url.Thumbnail;


            if (report.ControlByName(SUB_REPORT_EXAMINE) != null)
            {
                var subReportExamine = report.ControlByName(SUB_REPORT_EXAMINE).AsSubReport.Report;
                var reportJsonForExamineString = JsonConvert.SerializeObject(reportJsonForExamine);
                subReportExamine.LoadDataFromXML(reportJsonForExamineString);
            }
            if (report.ControlByName(SUB_REPORT_TEST) != null)
            {
                var subReportTest = report.ControlByName(SUB_REPORT_TEST).AsSubReport.Report;
                var reportJsonForTestString = JsonConvert.SerializeObject(reportJsonForTest);
                subReportTest.LoadDataFromXML(reportJsonForTestString);
            }
            if (report.ControlByName(SUB_REPORT_TEST1) != null && GDJY != "")
            {
                var reportJson1 = new JYReportJson();
                reportJson1.Detail = new List<JYGroups>();
                var master1 = new JYGroups();
                master1.Grouplist = GDJY;
                reportJson1.Detail.Add(master1);
                var subReportTest1 = report.ControlByName(SUB_REPORT_TEST1).AsSubReport.Report;
                var reportJsonForTestString1 = JsonConvert.SerializeObject(reportJson1);
                subReportTest1.LoadDataFromXML(reportJsonForTestString1);
            }
            if (report.ControlByName(SUB_REPORT_COLOR_ULTRASOUND) != null)
            {
                var subReportColorUltrasound = report.ControlByName(SUB_REPORT_COLOR_ULTRASOUND).AsSubReport.Report;
                var reportJsonForColorUltrasoundString = JsonConvert.SerializeObject(reportJsonForColorUltrasound);
                subReportColorUltrasound.LoadDataFromXML(reportJsonForColorUltrasoundString);
            }
            if (report.ControlByName(SUB_REPORT_RADIATE) != null)
            {
                var subReportRadiate = report.ControlByName(SUB_REPORT_RADIATE).AsSubReport.Report;
                var reportJsonForRadiateString = JsonConvert.SerializeObject(reportJsonForRadiate);
                subReportRadiate.LoadDataFromXML(reportJsonForRadiateString);
            }
            if (report.ControlByName(SUB_REPORT_FUNCTION) != null)
            {
                var subReportFunction = report.ControlByName(SUB_REPORT_FUNCTION).AsSubReport.Report;
                var reportJsonForFunctionString = JsonConvert.SerializeObject(reportJsonForFunction);
                subReportFunction.LoadDataFromXML(reportJsonForFunctionString);
            }
            if (report.ControlByName(SUB_REPORT_OTHER) != null)
            {
                var subReportOther = report.ControlByName(SUB_REPORT_OTHER).AsSubReport.Report;
                var reportJsonForOtherString = JsonConvert.SerializeObject(reportJsonForOther);
                subReportOther.LoadDataFromXML(reportJsonForOtherString);
            }
            if (report.ControlByName(SUB_REPORT_Normal) != null)
            {
                var subReportNormal = report.ControlByName(SUB_REPORT_Normal).AsSubReport.Report;
                var reportJsonForNormalString = JsonConvert.SerializeObject(reportJsonForNormal);
                subReportNormal.LoadDataFromXML(reportJsonForNormalString);
            }
            if (report.ControlByName(SUB_REPORT_Add) != null)
            {
                var subReportAdd = report.ControlByName(SUB_REPORT_Add).AsSubReport.Report;
                var reportJsonForAddString = JsonConvert.SerializeObject(reportJsonForAdd);
                subReportAdd.LoadDataFromXML(reportJsonForAddString);
            }

            //打印后事件
            report.PrintEnd += () =>
            {
               
            };
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 10)?.Remarks;
            if (!string.IsNullOrEmpty(printName))
            {
                report.Printer.PrinterName = printName;
            }
        
                if (preview)
            {
                report.PrintPreview(true);
                return ret;
            }

            if (printDialog)
            {
               
                report.Print(true);

                ////更新条码打印状态
                //ChargeBM chargeBM = new ChargeBM();
                //chargeBM.Id = cusNameInput.Id;
                //chargeBM.Name = "导引单";
                //barPrintAppService.UpdateCustomerRegisterPrintState(chargeBM);
                //日志

                ICommonAppService _commonAppService1 = new CommonAppService();
                CreateOpLogDto createOpLogDto1 = new CreateOpLogDto();
                createOpLogDto1.LogBM = customerReg.CustomerBM;
                createOpLogDto1.LogName = customerReg.Customer.Name;
                createOpLogDto1.LogText = "打印导引单";
                createOpLogDto1.LogDetail = "";
                createOpLogDto1.LogType = (int)LogsTypes.PrintId;
                _commonAppService1.SaveOpLog(createOpLogDto1);

                 return ret;
            }

            report.Print(false);
            Marshal.ReleaseComObject(report);
            //日志
            ICommonAppService _commonAppService = new CommonAppService();
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            createOpLogDto.LogBM = customerReg.CustomerBM;
            createOpLogDto.LogName = customerReg.Customer.Name;
            createOpLogDto.LogText = "打印导引单";
            createOpLogDto.LogDetail = "";
            createOpLogDto.LogType = (int)LogsTypes.PrintId;
            _commonAppService.SaveOpLog(createOpLogDto);
            return ret;
        }

    }
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportMinJson
    {
        /// <summary>
        /// 参数
        /// </summary>
        public List<Master> Master { get; set; }

        /// <summary>
        /// 明细网格
        /// </summary>
        public List<DetailMin> Detail { get; set; }
    }
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportJson
    {
        /// <summary>
        /// 参数
        /// </summary>
        public List<Master> Master { get; set; }

        /// <summary>
        /// 明细网格
        /// </summary>
        public List<Detail> Detail { get; set; }
    }
    /// <summary>
    /// 报表
    /// </summary>
    public class JYReportJson
    {
    

        /// <summary>
        /// 明细网格
        /// </summary>
        public List<JYGroups> Detail { get; set; }
    }
    public class JYGroups
    {
        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string Grouplist { get; set; }
    }


    /// <summary>
    /// 报表参数
    /// </summary>
    public class Master
    {
        /// <summary>
        /// 照片
        /// </summary>
        public string ParameterSerialPhoto { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string ParameterSerialPhotoReg { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int ParameterSerialNumber { get; set; }

        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string ParameterCustomerName { get; set; }

        /// <summary>
        /// 体检人性别
        /// </summary>
        public string ParameterCustomerSex { get; set; }

        /// <summary>
        /// 体检人年龄
        /// </summary>
        public int ParameterCustomerAge { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string ParameterIDCardNo { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string ParameterSectionNum { get; set; }

        

        /// <summary>
        /// 体检日期
        /// </summary>
        public DateTime ParameterExaminationDate { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public string ParameterExaminationBookingDate { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string ParameterCustomerExaminationNumber { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        public string ParameterCustomerArchNum { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ParameterCompanyName { get; set; }

        /// <summary>
        /// 领取方式
        /// </summary>
        public string ParameterReceiving { get; set; }

        /// <summary>
        /// 排队序号
        /// </summary>
        public string ParameterPDXH { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string AppliyNum { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string VisitCard { get; set; }

        /// <summary>
        /// 开单医师
        /// </summary>
        public string ParameterDoctor { get; set; }

        /// <summary>
        /// 体检类别
        /// </summary>
        public string ParameterExaminationType { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string ParameterCategory { get; set; }

        /// <summary>
        /// 客户类别
        /// </summary>
        public string ParameterCustomerType { get; set; }

        /// <summary>
        /// 介绍人
        /// </summary>
        public string ParameterIntroducer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ParameterRemark { get; set; }
        /// <summary>
        /// 预约备注
        /// </summary>
        public string ParameterRegRemark { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string ParameterSuitName { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string ParameterTeamName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ParameterMobile { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string ParameterAddress { get; set; }

        /// <summary>
        /// 婚否
        /// </summary>
        public string ParameterMarriage { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string ParameterDepartment { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string ParameterWorkNumber { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string ParameterNation { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string ParameterTypeWork { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public string AllMoney { get; set; }
        /// <summary>
        /// 总原价
        /// </summary>
        public string AllPrice { get; set; }
        /// <summary>
        /// 应收价格                   
        /// </summary>
        public string PayMoney { get; set; }
        /// <summary>
        /// 检验组合          
        /// </summary>
        public string JYGroups { get; set; }

        /// <summary>
        /// Pacs号
        /// </summary>      
        public virtual string PacsBM { get; set; }

        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }
        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>       
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>   
        public virtual string PostState { get; set; }

        /// <summary>
        /// 开票名称
        /// </summary>
        public virtual string FPName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual string ParameterBirthday { get; set; }
        /// <summary>
        /// 预约次数
        /// </summary>
        public string ParameterRegCount { get; set; }

        /// <summary>
        /// 上次体检号
        /// </summary>
        public string OldCusBm { get; set; }
        /// <summary>
        /// 上次体检日期
        /// </summary>
        public DateTime? OldLastTime { get; set; }


    }
    /// <summary>
    /// 明细网格
    /// </summary>
    public class DetailMin
    {
        
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string PhotoReg { get; set; }
        

    }
    /// <summary>
    /// 明细网格
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// 打勾
        /// </summary>
        public string Tick { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeparmentName { get; set; }
        /// <summary>
        /// 待查状态
        /// </summary>
        public string GiveState { get; set; }
        /// <summary>
        /// 待查时间
        /// </summary>
        public string GiveTime { get; set; }
        /// <summary>
        /// 待查备注
        /// </summary>
        public string GiveRemark { get; set; }
        /// <summary>
        /// 检验类型
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// 检验类型
        /// </summary>
        public string TestType { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public string ItemGroupName { get; set; }
        /// <summary>
        /// 空腹
        /// </summary>
        public string Fasting { get; set; }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string Notice { get; set; }
        /// <summary>
        /// 科室地址（提示信息）
        /// </summary>
        public string DepartmentAddress { get; set; }
        /// <summary>
        /// 科室VIP地址（提示信息）
        /// </summary>
        public string VIPDepartmentAddress { get; set; }
        /// <summary>
        /// 科室备注（提示信息）
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 科室职责
        /// </summary>
        public string Duty { get; set; }

        /// <summary>
        /// 医生签名
        /// </summary>
        public string DoctorSign { get; set; }

        /// <summary>
        /// 延期签字
        /// </summary>
        public string PostponeSign { get; set; }

        /// <summary>
        /// 拒检签字
        /// </summary>
        public string RejectSign { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerExaminationNumber { get; set; }

        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 项目原价
        /// </summary>
        public string ItemPrice { get; set; }
        /// <summary>
        /// 项目折后价格
        /// </summary>
        public string PriceAfterDis { get; set; }
        ///// <summary>
        ///// 照片
        ///// </summary>
        //public string Photo { get; set; }
        ///// <summary>
        ///// 照片
        ///// </summary>
        //public string PhotoReg { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientInfoName { get; set; }

        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 原体检人
        /// </summary>      
        public virtual string PrimaryName { get; set; }
         
        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
      

    }
}