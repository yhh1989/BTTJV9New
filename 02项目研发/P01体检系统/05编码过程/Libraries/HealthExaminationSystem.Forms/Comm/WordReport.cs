using Microsoft.Office.Interop.Word;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
   public  class WordReport
    {
        private IDoctorStationAppService CustomerItemPic;//图片
        PictureController _pictureController;
        IInspectionTotalAppService inspectionTotalAppService;
        Microsoft.Office.Interop.Word.Document myWordDoc;
        Microsoft.Office.Interop.Word.Application wordapp;
        //模板导出
        #region 不用
        //public void ExportWord(CustomerRegDto cusInfo, string path, string mbName)
        //{
        //    inspectionTotalAppService = new InspectionTotalAppService();
        //    _pictureController = new PictureController();
        //    //获取总检信息
        //    TjlCustomerQuery tjlCustomerQuery = new TjlCustomerQuery();
        //    tjlCustomerQuery.CustomerRegID = cusInfo.Id;
        //    //复查原始报告                              
        //    var tjlCustomerSummarizeDto = inspectionTotalAppService.GetSummarize(tjlCustomerQuery);
        //    //修改为全局Application    bcq  2013-3-19
        //    wordapp = new Microsoft.Office.Interop.Word.Application();

        //    //修改为全局word文档实例  bcq  2013-3-19
        //    myWordDoc = new Microsoft.Office.Interop.Word.Document();

        //    object oMissing = System.Reflection.Missing.Value;
        //    wordapp.Visible = false;
        //    if (!string.IsNullOrEmpty(path))
        //    {
        //        path = path.Substring(0, path.LastIndexOf('\\')) + "\\";


        //        path += cusInfo.Customer.Name + ".doc";
        //    }

        //    List<string[]> tableData = new List<string[]>();
        //    try
        //    {
        //        object filepath = mbName;

        //        myWordDoc = wordapp.Documents.Open(ref filepath, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        //        //个人头像
        //        PictureDto url = new PictureDto();
        //        if (cusInfo.Customer.CusPhotoBmId.HasValue && cusInfo.Customer.CusPhotoBmId != Guid.Empty)
        //        {
        //            url = _pictureController.GetUrl(cusInfo.Customer.CusPhotoBmId.Value);

        //            ExportCusPhoto(url.RelativePath, myWordDoc);
        //        }
        //        string txtpath = "";
        //        StreamReader sr = new StreamReader(txtpath, Encoding.GetEncoding("gb2312"));
        //        string str = null;
        //        List<string> list = new List<string>();
        //        while ((str = sr.ReadLine()) != null)
        //        {
        //            list.Add(str);
        //        }



        //        Bookmark bk = null;
        //        Selection sn = null;
        //        foreach (var nd in list) //Birthday  //IDNum
        //        {
        //            //int number = Convert.ToInt32(nd.Attributes["number"].Value);
        //            int number = 1;
        //            try
        //            {
        //                #region 导出基本信息
        //                switch (nd)
        //                {
        //                    case "Name":
        //                        ExportCusInfo(number, nd, cusInfo.Customer.Name, myWordDoc, bk);
        //                        break;
        //                    case "Sex":
        //                        var strsex = "男";
        //                        if (cusInfo.Customer.Sex.HasValue && cusInfo.Customer.Sex == 2)
        //                        {
        //                            strsex = "女";
        //                        }
        //                        ExportCusInfo(number, nd, strsex, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.Sex;
        //                        break;
        //                    case "Age":
        //                        if (cusInfo.Customer.Age == 1)
        //                        {
        //                            ExportCusInfo(number, nd, "", myWordDoc, bk);
        //                        }
        //                        else
        //                        {
        //                            ExportCusInfo(number, nd, cusInfo.Customer.Age.ToString(), myWordDoc, bk);
        //                        }
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.Age.ToString();
        //                        break;
        //                    case "MZ"://部门信息
        //                        ExportCusInfo(number, nd, cusInfo.Customer.Department, myWordDoc, bk);
        //                        break;
        //                    case "GZ"://部门信息
        //                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString());
        //                        var khlb = Clientcontract.FirstOrDefault(p => p.Value == cusInfo.Customer.CustomerType);
        //                        ExportCusInfo(number, nd, khlb.Text, myWordDoc, bk);
        //                        break;
        //                    case "ZJSJ"://总检时间
        //                                //CustomerSummarizeEntry cusSum = m_BusiCheckOperation.getSummarize(cusInfo.CustomerRegID);

        //                        if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.ConclusionDate.HasValue)
        //                        {
        //                            ExportCusInfo(number, nd, tjlCustomerSummarizeDto.ConclusionDate.Value.ToShortDateString(), myWordDoc, bk);
        //                        }
        //                        break;
        //                    case "cusPhoto":
        //                        try
        //                        {
        //                            object bookMark = "【hp】";
        //                            object linkToFile = true;
        //                            if (myWordDoc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
        //                            {
        //                                string ss = "";
        //                            }
        //                            object Nothing = System.Reflection.Missing.Value;
        //                            //定义插入图片是否随word文档一起保存
        //                            object saveWithDocument = true;




        //                            myWordDoc.Bookmarks.get_Item(ref bookMark).Select();

        //                            //设置图片位置
        //                            //wordapp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
        //                            InlineShape inlineShape = wordapp.Selection.InlineShapes.AddPicture(url.RelativePath, ref linkToFile, ref saveWithDocument, ref Nothing);
        //                            //在书签的位置添加图片                               
        //                            //设置图片大小
        //                            var WordReport = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.WordReport.ToString() && o.Value == 1);
        //                            if (WordReport != null && !string.IsNullOrEmpty(WordReport.Remarks) && WordReport.Remarks.Contains("*"))
        //                            {
        //                                string[] size = WordReport.Remarks.Split('*');
        //                                float Width = float.Parse(size[0]);
        //                                float hight = float.Parse(size[1]);
        //                                inlineShape.Width = Width;
        //                                inlineShape.Height = hight;
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            string ss = ex.ToString();
        //                        }
        //                        break;
        //                    case "CheckDate":
        //                        if (cusInfo.LoginDate.HasValue)
        //                        {
        //                            ExportCusInfo(number, nd, cusInfo.LoginDate.Value.ToShortDateString(), myWordDoc, bk);
        //                        }
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.CheckDate.ToShortDateString();
        //                        break;
        //                    case "Client":
        //                        string clientname = "个人";
        //                        if (cusInfo.ClientRegId.HasValue)
        //                        {
        //                            clientname = cusInfo.ClientReg.ClientInfo.ClientName;
        //                        }
        //                        ExportCusInfo(number, nd, clientname, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = lsvCheckResult.SelectedItems[0].SubItems[6].Text;
        //                        break;
        //                    case "Address":
        //                        ExportCusInfo(number, nd, cusInfo.Customer.Address, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.Address;
        //                        break;
        //                    case "Suit":
        //                        ExportCusInfo(number, nd, cusInfo.ItemSuitName, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.ItemSuitName;
        //                        break;
        //                    case "CallNum":
        //                        ExportCusInfo(number, nd, cusInfo.Customer.Mobile, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.Mobile;
        //                        break;
        //                    case "ArchivesNum":
        //                        ExportCusInfo(number, nd, cusInfo.CustomerBM, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.ArchivesNum;
        //                        break;
        //                    case "WebCode":
        //                        try
        //                        {
        //                            //ExportCusInfo(number, nd, cusInfo.wb.Trim(), myWordDoc, bk);
        //                        }
        //                        catch { }
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.ArchivesNum;
        //                        break;
        //                    case "CardNumber":
        //                        ExportCusInfo(number, nd, cusInfo.Customer.CardNumber, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.CardNumber;
        //                        break;
        //                    #region zht 20120816 添加书签  省份证号 出生日期
        //                    case "IDNum"://省份证号
        //                        ExportCusInfo(number, nd, cusInfo.Customer.IDCardNo, myWordDoc, bk);
        //                        break;
        //                    case "Birthday"://出生日期
        //                        if (cusInfo.Customer.Birthday.HasValue)
        //                        {
        //                            ExportCusInfo(number, nd, cusInfo.Customer.Birthday.Value.ToShortDateString(), myWordDoc, bk);
        //                        }
        //                        break;

        //                    #endregion
        //                    case "HF":
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        var messa = MessageStateHelper.CriticalTypeStateFormatter(cusInfo.Customer.MarriageStatus);
        //                        ExportCusInfo(number, nd, messa, myWordDoc, bk);
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //bk.Range.Text = cusInfo.CardNumber;
        //                        break;
        //                    case "Employee":
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                        //20090820 QF:取总检医生ID
        //                        //{EmployeeEntry employee = m_BusiSystemSettings.GetEmployeeByID(cusInfo.EmployeeID);}                               
        //                        if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.EmployeeBMId.HasValue)
        //                        {
        //                            ExportCusInfo(number, nd, tjlCustomerSummarizeDto.EmployeeBM.Name, myWordDoc, bk);
        //                        }
        //                        break;
        //                    case "ShEmployeeID":
        //                        if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.ShEmployeeBMId.HasValue)
        //                        {
        //                            ExportCusInfo(number, nd, tjlCustomerSummarizeDto.ShEmployeeBM.Name, myWordDoc, bk);
        //                        }
        //                        break;
        //                    case "Summarize":
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index);

        //                        if (tjlCustomerSummarizeDto != null)
        //                        {

        //                            ExportCusInfo(number, nd, tjlCustomerSummarizeDto.CharacterSummary, myWordDoc, bk);
        //                        }
        //                        break;
        //                    case "Advice":
        //                        //bk = myWordDoc.Bookmarks.get_Item(ref index); 
        //                        if (tjlCustomerSummarizeDto != null)
        //                        {
        //                            try
        //                            {
        //                                ExportCusInfo(number, nd, tjlCustomerSummarizeDto.Advice, myWordDoc, bk);
        //                            }
        //                            catch (Exception ex) { throw ex; }
        //                        }
        //                        break;
        //                    //zhangsb，20130125，增加报告打印日期


        //                    case "DateNow":
        //                        ExportCusInfo(number, nd, string.Format("{0:yyyy-MM-dd}", System.DateTime.Now), myWordDoc, bk);
        //                        break;
        //                    //zhangsb，20130125，增加报告打印时间


        //                    case "DateTimeNow":
        //                        ExportCusInfo(number, nd, string.Format("{0:yyyy-MM-dd HH:mm}", System.DateTime.Now), myWordDoc, bk);
        //                        break;
        //                }
        //                #endregion
        //            }
        //            catch (Exception ex)
        //            {
        //                continue;
        //            }
        //        }
        //        #region 导出已检项目信息
        //        CustomerItemPic = new DoctorStationAppService();
        //        QueryClass queryClass = new QueryClass();
        //        queryClass.CustomerRegId = cusInfo.Id;
        //        var cusRegItems = CustomerItemPic.GetTjlCustomerRegAllItemReoprtDtos(queryClass).Where(o => o.ProcessState != (int)ProjectIState.Not).OrderBy(n => n.DepartmentBM?.OrderNum).ThenBy(n => n.ItemGroupBM?.OrderNum).ThenBy(o => o.ItemOrder).ToList();
        //        //组合小结
        //        var lstCustomerItemGroupPrintViewDto = CustomerItemPic.GetCustomerItemGroupPrintViewDtos(queryClass).ToList();
        //        //获取科室小结
        //        var lstCustomerDepSummaries = CustomerItemPic.GetCustomerDepSummaries(queryClass);
        //        #region 婚检科不打印   丽水 杨建辉  2013-03-16
        //        var StrNotPrintDeparment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.WordReport, 2)?.Remarks;
        //        if (!string.IsNullOrEmpty(StrNotPrintDeparment))
        //        {
        //            string[] strNotPrintDeparments = StrNotPrintDeparment.Split(',');
        //            if (strNotPrintDeparments.Length > 0)
        //            {
        //                for (int i = cusRegItems.Count - 1; i > -1; i--)
        //                {
        //                    foreach (string var in strNotPrintDeparments)
        //                    {
        //                        if (cusRegItems[i].DepartmentBM.Name == var)
        //                        {
        //                            cusRegItems.Remove(cusRegItems[i]);
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //        #endregion



        //        #region 废弃的老代码



        //        #endregion

        //        #region 科室未启用过滤



        //        #endregion

        //        #region 独立打印科室过滤
        //        //string strKS = CommonTools.GetSysSeting("GRBB0010");
        //        //DepartmentEntry depPrint = new DepartmentEntry();
        //        //string[] strKsList = strKS.Split('|');
        //        //foreach (string KsName in strKsList)
        //        //{
        //        //    depPrint = m_BusiSystemSettings.getDepartmentByName(KsName);
        //        //    if (depPrint != null && depPrint.DepartmentID != null)
        //        //    {
        //        //        depIDs.Add(depPrint.DepartmentID);
        //        //    }
        //        //}
        //        #endregion

        //        //项目过滤

        //        //foreach (CustomerRegItems cusRegItem in cusRegItems)
        //        //{
        //        //    if (depIDs.Contains(cusRegItem.DepartmentID))
        //        //    {
        //        //        continue;
        //        //    }
        //        //    tmpList.Add(cusRegItem);
        //        //}

        //        string KSType = "";
        //        bool isFQ = false;
        //        string[] depSum = new string[2];
        //        Guid lastDoctorDepID = new Guid();
        //        foreach (var cusRegItem in cusRegItems)
        //        {
        //            var cusGroup = lstCustomerItemGroupPrintViewDto.FirstOrDefault(p => p.Id == cusRegItem.CustomerItemGroupBMid);
        //            #region 导出科室医生


        //            //CustomerRegOfficeAndSummaryEntry summary = m_BusiCheckReport.GetCustomerRegOfficeAndSummaryEntry(cusInfo.CustomerRegID, cusRegItem.DepartmentID);
        //            //EmployeeEntry emp = new EmployeeEntry();
        //            //if (summary.EmployeeID != null)
        //            //{
        //            //    if (summary.DoctorID != null && summary.DoctorID != "")
        //            //    {
        //            //        emp = m_BusiSystemSettings.GetEmployeeByID(summary.DoctorID);
        //            //    }
        //            //    else
        //            //    {
        //            //        emp = m_BusiSystemSettings.GetEmployeeByID(summary.EmployeeID);
        //            //    }
        //            //}

        //            if (lastDoctorDepID != cusRegItem.DepartmentId && lastDoctorDepID != Guid.Empty)
        //            {

        //                string employeeName = "";
        //                if (cusRegItem.InspectEmployeeBMId.HasValue)
        //                {

        //                    employeeName = cusRegItem.InspectEmployeeBM.Name;

        //                }
        //                tableData.Add(depSum);
        //                depSum = new string[2];
        //                string[] depEmp = new string[2];
        //                #region 杨建辉  86 2013-04-30

        //                depEmp[0] = "检查日期：" + cusGroup.FirstDateTime.ToString();
        //                depEmp[1] = "检查医生：" + employeeName;

        //                #endregion
        //                tableData.Add(depEmp);
        //            }
        //            if (tjlCustomerSummarizeDto.CharacterSummary != null)
        //            {
        //                string content = tjlCustomerSummarizeDto.CharacterSummary;

        //                if (content.Trim() == "")
        //                    content = "未见异常";
        //                if (content == "未检未检")
        //                    content = "未检";
        //                depSum[0] = "科室小结：";
        //                depSum[1] = content;
        //            }

        //            var dep = DefinedCacheHelper.GetDepartments().FirstOrDefault(p => p.Id == cusRegItem.DepartmentId);
        //            KSType = dep.Category;
        //            string[] depName = new string[1];
        //            depName[0] = dep.Name + "|" + dep.Category;
        //            tableData.Add(depName);

        //            //if (dep.DepartmentName == "血流变")
        //            //{
        //            //    dep.DepartmentName += "";
        //            //}

        //            string[] colName = new string[3];
        //            colName[0] = "项目名称";
        //            colName[1] = "检查结果";
        //            colName[2] = "参考值";
        //            tableData.Add(colName);
        //            xPath = "/report/department[@id=\"" + cusRegItem.DepartmentID + "\"]/depEmployee";
        //            XmlNode depEmployee = doc.DocumentElement.SelectSingleNode(xPath);
        //            if (emp == null)
        //            {
        //                string content = "";
        //                ExportItemInfo(content, depEmployee, myWordDoc, bk, "");

        //            }
        //            else
        //            {
        //                if (depEmployee != null && emp.EmployeeName != null)
        //                {
        //                    string content = emp.EmployeeName;

        //                }
        //            }

        //            xPath = "/report/department[@id=\"" + cusRegItem.DepartmentID + "\"]/depSum";
        //            XmlNode ndDepSum = doc.DocumentElement.SelectSingleNode(xPath);
        //            if (ndDepSum != null && summary.CharacterSummary != null)
        //            {
        //                string content = "";
        //                switch (CommonTools.GetSysSeting("GRBB0009"))
        //                {
        //                    case "":
        //                    case "0":
        //                        if (summary.CharacterSummary.Trim() != "未见异常" && summary.CharacterSummary.Trim() != "" && summary.DagnosisSummary.Trim() != "未见异常" && summary.DagnosisSummary.Trim() != "")
        //                        {
        //                            if (summary.DagnosisSummary.Trim() != "未见异常" && summary.DagnosisSummary.Trim() != "")
        //                            {
        //                                if (summary.CharacterSummary.Trim() != "未见异常" && summary.CharacterSummary.Trim() != "")
        //                                {
        //                                    content += "\r\n\t" + summary.CharacterSummary.Trim() + "\r\n";
        //                                }
        //                                content += "" + summary.DagnosisSummary.Trim();
        //                            }
        //                            else
        //                            {
        //                                if (summary.CharacterSummary.Trim().Contains("\r\n"))
        //                                {
        //                                    content += "\r\n\t" + summary.CharacterSummary.Trim() + "\r\n";
        //                                }
        //                                else
        //                                {
        //                                    content += summary.CharacterSummary.Trim() + "\r\n";
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    case "1":
        //                        if (summary.CharacterSummary.Trim() != "未见异常")
        //                            content += summary.CharacterSummary.Trim();
        //                        break;
        //                    case "2":
        //                        if (summary.DagnosisSummary.Trim() != "未见异常")
        //                            content += summary.DagnosisSummary.Trim();
        //                        break;
        //                }
        //                if (content.Trim() == "")
        //                    content = "未见异常";
        //                if (content == "未检未检")
        //                    content = "未检";
        //                ExportItemInfo(content, ndDepSum, myWordDoc, bk, "");
        //            }

        //            lastDepId = cusRegItem.DepartmentID;
        //            #endregion
        //            if (lastGroupId != cusRegItem.ItemGroupID)
        //            {
        //                if (cusRegItem.ItemGroupID != "")
        //                {
        //                    ItemGroupEntry itemGroup = m_BusiSystemSettings.getItemGroupByID(cusRegItem.ItemGroupID);
        //                    if (itemGroup == null)
        //                        groupName = "";
        //                    else
        //                        groupName = itemGroup.ItemGroupName;
        //                }
        //                else
        //                    groupName = "";
        //            }
        //            lastGroupId = cusRegItem.ItemGroupID;
        //            #region 导出项目
        //            ItemInfoEntry itemInfo = m_BusiSystemSettings.getItemInfoByItemID(cusRegItem.ItemID);
        //            if (itemInfo != null)
        //            {
        //                isFQ = false;
        //                string content = "";
        //                switch (cusRegItem.ProcessID.Trim())
        //                {
        //                    case "0":
        //                        isFQ = true;
        //                        content = "放弃";
        //                        break;
        //                    case "1":
        //                        if (itemInfo.ItemType.Trim() == "说明型")
        //                        {
        //                            content = cusRegItem.ItemResultChar.Trim();
        //                        }
        //                        else
        //                        {
        //                            if (cusRegItem.ItemFlag == "理想体重" || cusRegItem.ItemFlag == "体重指数")
        //                            {
        //                                if (Convert.ToInt32(cusRegItem.ItemResultNum) < 0)
        //                                {
        //                                    content = "" + cusRegItem.ItemResultChar.Trim();
        //                                }
        //                                else
        //                                {
        //                                    //if (blxsks.Contains(cusRegItem.DepartmentID))
        //                                    //{
        //                                    //    content = cusRegItem.ItemResultNum.ToString("0.0").Trim() + cusRegItem.ItemResultChar.Trim();
        //                                    //}
        //                                    //else
        //                                    //{
        //                                    //    content = cusRegItem.ItemResultNum.ToString().Trim() + cusRegItem.ItemResultChar.Trim();
        //                                    //}
        //                                    bool isSTR = false;
        //                                    try
        //                                    {
        //                                        if (Convert.ToDouble(cusRegItem.ItemResultNum.ToString().Trim()) == Convert.ToDouble(cusRegItem.ItemResultChar.Trim())) isSTR = true;
        //                                    }
        //                                    catch { }
        //                                    if (blxsks.Contains(cusRegItem.DepartmentID))
        //                                    {
        //                                        if (cusRegItem.ItemResultChar.Trim() != "" && isSTR)
        //                                            content = cusRegItem.ItemResultNum.ToString("0.0#").Trim();
        //                                        else
        //                                            content = cusRegItem.ItemResultNum.ToString("0.0#").Trim() + cusRegItem.ItemResultChar.Trim();
        //                                    }
        //                                    else
        //                                    {
        //                                        if (cusRegItem.ItemResultChar.Trim() != "" && isSTR)
        //                                            content = cusRegItem.ItemResultChar.Trim();
        //                                        else
        //                                            content = cusRegItem.ItemResultNum.ToString().Trim() + cusRegItem.ItemResultChar.Trim();
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                bool isSTR = false;
        //                                try
        //                                {
        //                                    if (Convert.ToDouble(cusRegItem.ItemResultNum.ToString().Trim()) == Convert.ToDouble(cusRegItem.ItemResultChar.Trim())) isSTR = true;
        //                                }
        //                                catch { }
        //                                if (blxsks.Contains(cusRegItem.DepartmentID))
        //                                {
        //                                    if (cusRegItem.ItemResultChar.Trim() != "" && isSTR)
        //                                        content = cusRegItem.ItemResultNum.ToString("0.0#").Trim();
        //                                    else
        //                                    {
        //                                        if (cusRegItem.strSign.Trim() != "")
        //                                        {
        //                                            content = cusRegItem.strSign.Trim() + cusRegItem.ItemResultNum.ToString("0.0#").Trim() + cusRegItem.ItemResultChar.Trim();
        //                                        }
        //                                        else
        //                                        {
        //                                            content = cusRegItem.ItemResultNum.ToString("0.0#").Trim() + cusRegItem.ItemResultChar.Trim();
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (cusRegItem.ItemResultChar.Trim() != "" && isSTR)
        //                                        content = cusRegItem.ItemResultChar.Trim();
        //                                    else
        //                                        if (cusRegItem.strSign.Trim() != "")
        //                                    {
        //                                        content = cusRegItem.strSign.Trim() + cusRegItem.ItemResultNum.ToString().Trim() + cusRegItem.ItemResultChar.Trim();
        //                                    }
        //                                    else
        //                                    {
        //                                        content = cusRegItem.ItemResultNum.ToString().Trim() + cusRegItem.ItemResultChar.Trim();
        //                                    }
        //                                }
        //                            }


        //                        }
        //                        //content = itemInfo.ItemType.Trim() == "说明型" ? cusRegItem.ItemResultChar.Trim() : (cusRegItem.ItemResultNum.ToString().Trim() + cusRegItem.ItemResultChar.Trim());
        //                        break;
        //                    case "2":
        //                        content = "拒检";
        //                        break;
        //                }
        //                string[] item = new string[6];

        //                #region 如果体检结果里面的图片不为空，暂将图片拼接到项目名称中   步长强  2013-2-21
        //                if (cusRegItem.ImageFileName != null && cusRegItem.ImageFileName != "")
        //                {
        //                    groupName = groupName + "@" + cusRegItem.ImageFileName;
        //                }
        //                #endregion
        //                item[0] = groupName;
        //                item[1] = itemInfo.ItemName;
        //                item[2] = content.Trim();
        //                bool xsys = false;
        //                string xmbz = m_BusiCheckReport.GetReforenceValueForBZ(itemInfo, cusInfo, content.Trim(), ref xsys).Trim();
        //                item[3] = xmbz + " " + itemInfo.Unit.Trim();
        //                item[4] = itemInfo.Unit.Trim();
        //                item[5] = xsys.ToString();

        //                //if (cusRegItem.ItemResultChar.Trim() != "" && cusRegItem.Symbol != "")
        //                //{
        //                //    xsys = true;
        //                //}
        //                //if(tableData[tableData.Count-1].Length==5&&tableData[tableData.Count-1][0]!=groupName )
        //                //{
        //                //    item[4] = "M";
        //                //}
        //                tableData.Add(item);

        //                object index;

        //                xPath = "/report/department[@id=\"" + cusRegItem.DepartmentID + "\"]/item[@id=\"" + cusRegItem.ItemID + "\"]";
        //                XmlNode nodeItem = doc.DocumentElement.SelectSingleNode(xPath);
        //                if (nodeItem != null)
        //                {
        //                    //ExportItemInfo(content, nodeItem, myWordDoc, bk, cusRegItem.ImageFileName.Trim());
        //                    //ExportItemInfo(content, nodeItem, myWordDoc, bk, cusRegItem.ImageFileName.Trim(), cusInfo,xsys);

        //                    index = nodeItem.InnerText;
        //                    bool isPic = true;
        //                    try
        //                    {
        //                        bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                    }
        //                    catch
        //                    {
        //                        isPic = false;
        //                    }
        //                    if (isPic)
        //                    {
        //                        bool isItem = false;
        //                        if (CommonTools.GetSysSeting("GRBB0052").IndexOf(cusRegItem.ItemFlag) >= 0)
        //                        {
        //                            isItem = true;
        //                        }
        //                        ExportItemInfo(content, nodeItem, myWordDoc, bk, cusRegItem.ImageFileName.Trim(), cusInfo, xsys, isItem, KSType);
        //                    }
        //                    else
        //                    {
        //                        if (myWordDoc.Bookmarks.Exists("【AllItem】"))
        //                        {
        //                            //通用显示图像
        //                            if (cusRegItem.ImageFileName.Trim() != "")
        //                            {
        //                                string[] strPicList = cusRegItem.ImageFileName.Trim().Split(',');
        //                                for (int iPic = 0; iPic < strPicList.Length; iPic += 2)
        //                                {
        //                                    string[] picInfo = new string[5];
        //                                    picInfo[0] = "PIC";
        //                                    picInfo[1] = cusRegItem.ImageFileName.Trim();
        //                                    picInfo[2] = strPicList[iPic];
        //                                    if (iPic < strPicList.Length - 1)
        //                                    {
        //                                        picInfo[3] = strPicList[iPic + 1];
        //                                    }
        //                                    else
        //                                    {
        //                                        picInfo[3] = "";
        //                                    }
        //                                    picInfo[4] = strPicList.Length.ToString();
        //                                    tableData.Add(picInfo);
        //                                }
        //                            }

        //                        }
        //                        else
        //                        {
        //                            //标签图像
        //                            XmlNode nextNode = nodeItem.NextSibling;
        //                            EmployeeEntry emp;
        //                            if (cusRegItem.DoctorID != null && cusRegItem.DoctorID != "")
        //                            {
        //                                emp = m_BusiSystemSettings.GetEmployeeByID(cusRegItem.DoctorID);
        //                            }
        //                            else
        //                            {
        //                                emp = m_BusiSystemSettings.GetEmployeeByID(cusRegItem.EmployeeID);
        //                            }
        //                            if (emp == null)
        //                            {
        //                                content = "";
        //                            }
        //                            else
        //                            {
        //                                if (isFQ)
        //                                {
        //                                    content = "";
        //                                }
        //                                else
        //                                {
        //                                    content = emp.EmployeeName;
        //                                }
        //                            }

        //                            //ExportItemInfo(content, nextNote, myWordDoc, bk);
        //                            try
        //                            {
        //                                index = nodeItem.InnerText.Insert(1, "E");
        //                                bk = myWordDoc.Bookmarks.get_Item(ref index);
        //                                bk.Range.Text = emp.EmployeeName;
        //                            }
        //                            catch
        //                            { }
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            //最后一个科室医生


        //            #region 医院ID处理
        //            if (intDepID < cusRegItems.Count - 1)
        //            {
        //                lastDoctorDepID = cusRegItems[intDepID].DepartmentID;
        //                lastDate = cusRegItems[intDepID].CheckDate.ToShortDateString();
        //                if (cusRegItems[intDepID].DoctorID == null)
        //                {
        //                    if (cusRegItems[intDepID].EmployeeID == null)
        //                    {
        //                        lastEmployeeID = "";
        //                    }
        //                    else
        //                    {
        //                        lastEmployeeID = cusRegItems[intDepID].EmployeeID;
        //                    }
        //                }
        //                else
        //                {
        //                    lastEmployeeID = cusRegItems[intDepID].DoctorID;
        //                }
        //            }
        //            #endregion
        //            intDepID++;
        //            if (cusRegItem == cusRegItems[cusRegItems.Count - 1])
        //            {
        //                #region 最后一个科室医生签名处理


        //                CustomerRegOfficeAndSummaryEntry summary = m_BusiCheckReport.GetCustomerRegOfficeAndSummaryEntry(cusInfo.CustomerRegID, cusRegItem.DepartmentID);
        //                EmployeeEntry emp = new EmployeeEntry();
        //                if (summary.EmployeeID != null)
        //                {
        //                    if (summary.DoctorID != null && summary.DoctorID != "")
        //                    {
        //                        emp = m_BusiSystemSettings.GetEmployeeByID(summary.DoctorID);
        //                    }
        //                    else
        //                    {
        //                        emp = m_BusiSystemSettings.GetEmployeeByID(summary.EmployeeID);
        //                    }
        //                }
        //                string[] depSum1 = new string[2];
        //                //if (summary.CharacterSummary != null && summary.DagnosisSummary != null)
        //                //{
        //                //    depSum1[0] = "科室小结：\r\n\t";
        //                //    if (summary.DagnosisSummary.Trim() != "")
        //                //    {
        //                //        depSum1[0] += "体征：" + summary.CharacterSummary.Trim();
        //                //        depSum1[1] = "\r\n诊断：" + summary.DagnosisSummary.Trim();
        //                //    }
        //                //    else
        //                //    {
        //                //        depSum1[1] = summary.CharacterSummary.Trim();
        //                //    }

        //                //}
        //                //else
        //                //{
        //                //    depSum1[0] = "科室小结：\r\n";
        //                //    depSum1[1] = "";
        //                //}
        //                string content = "";
        //                switch (CommonTools.GetSysSeting("GRBB0009"))
        //                {
        //                    case "":
        //                    case "0":
        //                        if (summary.CharacterSummary.Trim() != "未见异常" && summary.CharacterSummary.Trim() != "" && summary.DagnosisSummary.Trim() != "未见异常" && summary.DagnosisSummary.Trim() != "")
        //                        {
        //                            if (summary.DagnosisSummary.Trim() != "未见异常" && summary.DagnosisSummary.Trim() != "")
        //                            {
        //                                if (summary.CharacterSummary.Trim() != "未见异常" && summary.CharacterSummary.Trim() != "")
        //                                {
        //                                    content += "\r\n\t" + summary.CharacterSummary.Trim() + "\r\n";
        //                                }
        //                                content += "" + summary.DagnosisSummary.Trim();
        //                            }
        //                            else
        //                            {
        //                                if (summary.CharacterSummary.Trim().Contains("\r\n"))
        //                                {
        //                                    content += "\r\n\t" + summary.CharacterSummary.Trim() + "\r\n";
        //                                }
        //                                else
        //                                {
        //                                    content += summary.CharacterSummary.Trim() + "\r\n";
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    case "1":
        //                        if (summary.CharacterSummary.Trim() != "未见异常")
        //                            content += summary.CharacterSummary.Trim();
        //                        break;
        //                    case "2":
        //                        if (summary.DagnosisSummary.Trim() != "未见异常")
        //                            content += summary.DagnosisSummary.Trim();
        //                        break;
        //                }
        //                depSum1[0] = "科室小结：";
        //                depSum1[1] = content;
        //                tableData.Add(depSum1);
        //                string[] depEmp = new string[2];
        //                depEmp[0] = "检查日期：" + cusInfo.CheckDate.ToShortDateString();
        //                if (emp != null)
        //                {
        //                    string employeeName = emp.EmployeeName != null ? emp.EmployeeName : "";
        //                    if (!isFQ)
        //                    {
        //                        depEmp[1] = "检查医生：" + employeeName;
        //                    }
        //                    else
        //                    {
        //                        depEmp[1] = "检查医生：";
        //                    }
        //                }
        //                else
        //                {
        //                    depEmp[1] = "检查医生：";
        //                }
        //                #region  Yjh  2013-04-30 86
        //                DataAcc.SystemSettings sethis = new SystemSettings();
        //                if (!sethis.GetSet_SysSetting("个人报告是否要检查时间和检查医生"))
        //                {

        //                }
        //                else
        //                {
        //                    tableData.Add(depEmp);
        //                }
        //                #endregion
        //                #endregion
        //            }
        //        }
        //        #endregion
        //    }
        //    catch (Exception ew)
        //    {
        //        // MessageBox.Show(ew.Message + "\r\n\r\n" + ew.StackTrace, "报告整理");
        //    }
        //    try
        //    {
        //        //if (CommonTools.GetSysSeting("GRBB0006") == "0")
        //        //{
        //        try
        //        {
        //            ExportAllItem(myWordDoc, tableData, oMissing);
        //            //Dictionary<string,List<stirng[]>> listDanDuDaYinFangSheKe=listDangSheKeShiJieGuo;
        //            //if (listDanDuDaYinFangSheKe != null && listDanDuDaYinFangSheKe.Count > 0)
        //            //{

        //            //}
        //        }
        //        catch (SystemException ex)
        //        {
        //            //throw ex;     //zhangsb，此处不能报错，程序容错性未调整。


        //        }
        //        //}
        //        //else
        //        //{
        //        //    ExportAllItemDouble(myWordDoc, tableData, oMissing);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        // MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "报告导出");
        //    }
        //    finally
        //    {
        //        if (path == "")//是否打印
        //        {
        //            //if (countPrintWord == 0)
        //            //{
        //            myWordDoc.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        //            object o = true;
        //            myWordDoc.Close(ref o, ref oMissing, ref oMissing);
        //            wordapp.Quit(ref oMissing, ref oMissing, ref oMissing);

        //            //Customerstate cusState = new Customerstate(cusInfo.CheckState);
        //            //cusState.Checkstate = 4;//报告输出                
        //            //m_BusiCheckPrepare.SetCustomerState(cusInfo.CustomerRegID, cusState.getCustomerState());
        //            //itm.SubItems[3].Text = "报告输出";
        //            //string logText = "打印报告" + cusInfo.ArchivesNum + "-" + cusInfo.Name + " 操作员：" + Globals.Emoloyee.EmployeeName;
        //            //CommonTools.AddSystemLog(Globals.Emoloyee.EmployeeID, "", logText);
        //            //}
        //        }
        //        else
        //        {
        //            //object o = true;
        //            //// myWordDoc.Close(ref o, ref oMissing, ref oMissing);
        //            //// wordapp.Quit(ref oMissing, ref oMissing, ref oMissing);
        //            //string logT = "导出报告" + cusInfo.ArchivesNum + "-" + cusInfo.Name + " 操作员：" + Globals.Emoloyee.EmployeeName;
        //            //CommonTools.AddSystemLog(Globals.Emoloyee.EmployeeID, "", logT);
        //            //Customerstate cusState = new Customerstate(cusInfo.CheckState);
        //            //cusState.Checkstate = 4;//报告输出                
        //            //m_BusiCheckPrepare.SetCustomerState(cusInfo.CustomerRegID, cusState.getCustomerState());
        //            //itm.SubItems[3].Text = "报告输出";
        //        }
        //    }




        //    System.Threading.Thread.Sleep(500);

        //}

        //public void ExportCusInfo(int number, string nd, string content, Microsoft.Office.Interop.Word.Document myWordDoc, Bookmark bk)
        //{

        //    object index = null;
        //    if (number == 1)
        //    {
        //        index = nd;
        //        bk = myWordDoc.Bookmarks.get_Item(ref index);
        //        bk.Range.Text = content;

        //    }

        //}
        //public void ExportCusPhoto(string filePath, Microsoft.Office.Interop.Word.Document myWordDoc)
        //{
        //    if (myWordDoc.Bookmarks.Exists("【cusPhoto】") == false) { return; }

        //    object oMissing = System.Reflection.Missing.Value;
        //    Microsoft.Office.Interop.Word.Table oTable;
        //    object index = "【cusPhoto】";
        //    Microsoft.Office.Interop.Word.Range wrdRng = myWordDoc.Bookmarks.get_Item(ref index).Range;

        //    oTable = myWordDoc.Tables.Add(wrdRng, 1, 1, ref oMissing, ref oMissing);
        //    oTable.Borders.Enable = 0;
        //    oTable.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
        //    oTable.Cell(1, 1).Select();
        //    object LinkToFile = false;
        //    object SaveWithDocument = true;
        //    object Anchor = myWordDoc.Application.Selection.Range;
        //    myWordDoc.Application.ActiveDocument.InlineShapes.AddPicture(filePath, ref LinkToFile, ref SaveWithDocument, ref Anchor);
        //    string str = CommonTools.GetSysSeting("sys019");
        //    string[] tp = str.Split(',');
        //    if (tp.Length >= 1)
        //    {

        //        try
        //        {

        //            myWordDoc.Application.ActiveDocument.InlineShapes[1].Width = int.Parse(tp[0]);//图片宽度
        //            myWordDoc.Application.ActiveDocument.InlineShapes[1].Height = int.Parse(tp[0]);//图片高度
        //        }
        //        catch (Exception)
        //        {


        //        }

        //    }
        //    else
        //    {
        //        myWordDoc.Application.ActiveDocument.InlineShapes[1].Width = 0f;//图片宽度
        //        myWordDoc.Application.ActiveDocument.InlineShapes[1].Height = 0f;//图片高度
        //    }
        //    //将图片设置为四周环绕型


        //    Microsoft.Office.Interop.Word.Shape s = myWordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
        //    s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;
        //    File.Delete(filePath);
        //}
        //public void ExportAllItem(Microsoft.Office.Interop.Word.Document myWordDoc, List<string[]> tableData, object oMissing)
        //{
        //    string RangBorderStyle = "0";

        //    Microsoft.Office.Interop.Word.Table oTable;
        //    object allItem = "【AllItem】";
        //    if (myWordDoc.Bookmarks.Exists("【AllItem】") == false) { return; }
        //    Microsoft.Office.Interop.Word.Range wrdRng = myWordDoc.Bookmarks.get_Item(ref allItem).Range;
        //    if (tableData.Count == 0) { return; }
        //    oTable = myWordDoc.Tables.Add(wrdRng, tableData.Count, 3, ref oMissing, ref oMissing);
        //    #region 边框样式设置 1:全边框


        //    // RangBorderStyle = CommonTools.GetSysSeting("GRBB0002");
        //    if (RangBorderStyle == "1")
        //    {
        //        oTable.Borders.Enable = 1;
        //    }
        //    else
        //    {
        //        oTable.Borders.Enable = 0;
        //    }
        //    #endregion
        //    #region
        //    myWordDoc.ActiveWindow.Visible = false; //= true;
        //    int count = tableData.Count;
        //    string strSign = "0";
        //    string KSLeiBie = "";
        //    int iDelrow = 0;
        //    //开始合并的行的号数   步长强  2013-2-20
        //    int heBingHangRowIndex = 0;
        //    bool isNewHeBing = false;
        //    //保存项目名称信息（图片）
        //    string itemNameImageFile = string.Empty;
        //    //保存放射科室
        //    string keShiMingCheng = string.Empty;
        //    string keShiLeiBie = string.Empty;
        //    for (int r = 1; r <= count;)
        //    {
        //        //if (r == 89)
        //        //{
        //        //    MessageBox.Show("test");
        //        //}
        //        string[] row = tableData[r - 1];
        //        #region 对放射科进行单独打印  步长强  2013-2-22
        //        //if (row.Length == 1)
        //        //{
        //        //    string[] keShiAndLeiXing = row[0].Split('|');
        //        //    keShiMingCheng = string.Empty;
        //        //    keShiLeiBie = string.Empty;
        //        //    if (keShiAndLeiXing[1].Contains("放射"))
        //        //    {
        //        //        keShiMingCheng = keShiAndLeiXing[0];
        //        //        keShiLeiBie = keShiAndLeiXing[1];
        //        //        r++;
        //        //        continue;
        //        //    }
        //        //} 
        //        #endregion
        //        //将放射科室，以及放射科室检查结果放到Dictionary表中
        //        if (row.Length > 1 && keShiLeiBie.Contains("放射"))
        //            //{
        //            //    if (!listDangSheKeShiJieGuo.ContainsKey(keShiMingCheng))
        //            //    {
        //            //        List<string[]> list = new List<string[]>();
        //            //        list.Add(row);
        //            //        listDangSheKeShiJieGuo.Add(keShiMingCheng, list);
        //            //    }
        //            //    else
        //            //    {
        //            //        List<string[]> list2 =listDangSheKeShiJieGuo[keShiMingCheng];
        //            //        list2.Add(row);
        //            //        listDangSheKeShiJieGuo.Remove(keShiMingCheng);
        //            //        listDangSheKeShiJieGuo.Add(keShiMingCheng, list2);
        //            //    }
        //            //    r++;
        //            //    continue;
        //            //}


        //            #region 判断是否项目中有图片信息   步长强  2013-2-21
        //            if (row[0].Contains("@"))
        //            {
        //                string tempString = row[0];
        //                row[0] = tempString.Split('@')[0];
        //                itemNameImageFile = tempString.Split('@')[1];
        //            }
        //        #endregion
        //        #region 合并行单元格，如果当前行与上一行单元格内容相同   步长强   2013-2-20
        //        string[] rowNext = tableData[r - 1];
        //        if (r > 1)
        //        {
        //            rowNext = tableData[r - 2];
        //        }

        //        #endregion

        //        if (row.Length == 1)
        //        {
        //            oTable.Cell(r - iDelrow, 1).Range.Font.Size = 12;
        //            oTable.Cell(r - iDelrow, 1).Range.Bold = 1;
        //            //oTable.Cell(r, 1).Range.Text = row[0];
        //            if (r == 1)
        //            {

        //                //oTable.Cell(r - iDelrow, 1).Range.Columns.Width = 195;
        //                //oTable.Cell(r - iDelrow, 2).Range.Columns.Width = 155;
        //                //oTable.Cell(r - iDelrow, 3).Range.Columns.Width = 120;

        //                #region 步长强- 调整表格单元格宽度 - 2013-1-29 12：13

        //                oTable.Cell(r - iDelrow, 1).Range.Columns.Width = 275;
        //                oTable.Cell(r - iDelrow, 2).Range.Columns.Width = 75;
        //                oTable.Cell(r - iDelrow, 3).Range.Columns.Width = 100;

        //                #endregion
        //            }
        //            if (row[0].IndexOf("|") > 1)
        //            {
        //                oTable.Cell(r - iDelrow, 3).Merge(oTable.Cell(r - iDelrow, 2));
        //                oTable.Cell(r - iDelrow, 2).Merge(oTable.Cell(r - iDelrow, 1));
        //                oTable.Cell(r - iDelrow, 1).Range.Text = row[0].Substring(0, row[0].IndexOf("|"));
        //                KSLeiBie = row[0].Replace(row[0].Substring(0, row[0].IndexOf("|")) + "|", "");
        //            }
        //            else
        //            {
        //                oTable.Cell(r - iDelrow, 1).Range.Text = row[0];
        //            }
        //            //项目阳性显示格式


        //            if (KSLeiBie == "检验")
        //            {
        //                strSign = CommonTools.GetSysSeting("GRBB0004"); //检验


        //            }
        //            else
        //            {
        //                strSign = CommonTools.GetSysSeting("GRBB0005"); //常规
        //            }

        //            //if (KSLeiBie == "放射")
        //            //{
        //            //    strSign = CommonTools.GetSysSeting("GRBB0005"); //检验


        //            //}
        //            #region 组合名增加底色


        //            //if (CommonTools.GetSysSeting("GRBB0001") == "1")
        //            //{
        //            //    oTable.Cell(r - iDelrow, 1).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //}
        //            #endregion
        //            //题头
        //            switch (KSLeiBie)
        //            {
        //                case "检查":
        //                case "放射":
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Bold = 1;
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Text = tableData[r][0];
        //                    //oTable.Cell(r + 1, 1).Range.Font.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //                    oTable.Cell(r + 1 - iDelrow, 3).Merge(oTable.Cell(r + 1 - iDelrow, 2));
        //                    oTable.Cell(r + 1 - iDelrow, 2).Range.Bold = 1;
        //                    oTable.Cell(r + 1 - iDelrow, 2).Range.Text = tableData[r][1];
        //                    break;
        //                default:
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Bold = 1;
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Text = tableData[r][0];
        //                    //oTable.Cell(r + 1, 1).Range.Font.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //                    oTable.Cell(r + 1 - iDelrow, 2).Range.Bold = 1;
        //                    oTable.Cell(r + 1 - iDelrow, 2).Range.Text = tableData[r][1];
        //                    oTable.Cell(r + 1 - iDelrow, 3).Range.Bold = 1;
        //                    oTable.Cell(r + 1 - iDelrow, 3).Range.Text = tableData[r][2];
        //                    break;
        //            }
        //            #region 题头加底色


        //            //if (CommonTools.GetSysSeting("GRBB0003") == "1")
        //            //{
        //            //    switch (KSLeiBie)
        //            //    {
        //            //        case "检查":
        //            //        case "放射":
        //            //            oTable.Cell(r + 1 - iDelrow, 1).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //            oTable.Cell(r + 1 - iDelrow, 2).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //            //oTable.Cell(r + 1 - iDelrow, 3).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //            break;
        //            //        default:
        //            //            oTable.Cell(r + 1 - iDelrow, 1).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //            oTable.Cell(r + 1 - iDelrow, 2).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //            oTable.Cell(r + 1 - iDelrow, 3).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //            //            break;
        //            //    }
        //            //}
        //            #endregion
        //            #region 绘画单元格边框


        //            switch (RangBorderStyle)
        //            {
        //                case "1":
        //                    break;
        //                case "2":
        //                    /*zhangsb,20130128报错注掉
        //                    oTable.Cell(r - iDelrow, 1).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    try
        //                    {
        //                        oTable.Cell(r - iDelrow, 2).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }
        //                     */
        //                    try
        //                    {
        //                        oTable.Cell(r - iDelrow, 1).Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
        //                        //oTable.Cell(r - iDelrow, 2).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }

        //                    catch (SystemException ex)
        //                    {
        //                        throw ex;
        //                    }
        //                    /*zhangsb,20130128报错注掉
        //                    try
        //                    {
        //                        oTable.Cell(r - iDelrow, 3).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }
        //                    catch (SystemException ex)
        //                    {
        //                        throw ex;
        //                    }
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    try
        //                    {
        //                        oTable.Cell(r + 1 - iDelrow, 2).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }
        //                    catch (SystemException ex)
        //                    {
        //                        throw ex;
        //                    }
        //                    try
        //                    {
        //                        oTable.Cell(r + 1 - iDelrow, 3).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }
        //                    catch (SystemException ex)
        //                    {
        //                        throw ex;
        //                    }
        //                     */
        //                    break;
        //                case "3":
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    oTable.Cell(r + 1 - iDelrow, 1).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    try
        //                    {
        //                        oTable.Cell(r + 1 - iDelrow, 2).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                        oTable.Cell(r + 1 - iDelrow, 2).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }
        //                    catch { }
        //                    try
        //                    {
        //                        oTable.Cell(r + 1 - iDelrow, 3).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                        oTable.Cell(r + 1 - iDelrow, 3).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                        oTable.Cell(r + 1 - iDelrow, 3).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        //                    }
        //                    catch (SystemException ex)
        //                    {
        //                        throw ex;
        //                    }
        //                    break;
        //            }
        //            #endregion
        //            r += 2;
        //        }
        //        else if (row.Length == 2)
        //        {
        //            #region 科室小结
        //            if (row[0] != null && row[0] != null)
        //            {
        //                switch (KSLeiBie)
        //                {
        //                    case "检验":
        //                        if (!row[0].Trim().Contains("科室小结"))
        //                        {
        //                            oTable.Cell(r - 1 - iDelrow, 3).Range.Text = row[1];
        //                            oTable.Cell(r - 1 - iDelrow, 2).Range.Text = row[0];
        //                            // oTable.Rows[r - iDelrow].Delete();
        //                            iDelrow++;
        //                        }
        //                        break;
        //                    default:
        //                        if (row[0].Trim().Contains("科室小结"))
        //                        {
        //                            oTable.Cell(r - iDelrow, 2).Merge(oTable.Cell(r - iDelrow, 1));
        //                            oTable.Cell(r - iDelrow, 2).Merge(oTable.Cell(r - iDelrow, 1));

        //                            //#region 暂时将图片信息打印在科室小结上面  步长强 2013-2-21
        //                            ////如果项目结果中包含图片信息就打印出来
        //                            //if (itemNameImageFile != string.Empty)
        //                            //{
        //                            //   string[] images=itemNameImageFile.Split(',');
        //                            //   for (int i = 0; i < images.Length; i++)
        //                            //   {
        //                            //       //插入图片
        //                            //       string downPath = System.Windows.Forms.Application.ExecutablePath;
        //                            //       string path = downPath.Substring(0, downPath.LastIndexOf("\\")) + "\\images";
        //                            //       string FileName = path+"\\"+images[i];//图片所在路径



        //                            //       object LinkToFile = false;
        //                            //       object SaveWithDocument = true;
        //                            //       object Anchor = oTable.Cell(r - iDelrow, 1).Range;
        //                            //       InlineShape sp=oTable.Cell(r - iDelrow, 1).Range.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
        //                            //      // sp.Application.ActiveDocument.InlineShapes[1].ConvertToShape().WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;
        //                            //       //myWordDoc.Application.ActiveDocument.InlineShapes[1].Width = 200f;//图片宽度
        //                            //       //myWordDoc.Application.ActiveDocument.InlineShapes[1].Height = 200f;//图片高度
        //                            //       //sp.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
        //                            //       myWordDoc.Application.ActiveDocument.InlineShapes[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
        //                            //       //Microsoft.Office.Interop.Word.Shape s = myWordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
        //                            //       ////s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapInline;
        //                            //       sp.Width = 300f;
        //                            //       sp.Height = 300f;
        //                            //   }
        //                            //}
        //                            //itemNameImageFile = string.Empty;
        //                            //#endregion

        //                            //oTable.Cell(r - iDelrow, 1).Range.InsertAfter("\r\n"+row[0] + row[1]);
        //                            oTable.Cell(r - iDelrow, 1).Range.Text = row[0] + row[1];
        //                        }
        //                        else
        //                        {
        //                            oTable.Cell(r - iDelrow, 2).Range.Text = row[0];
        //                            oTable.Cell(r - iDelrow, 3).Range.Text = row[1];
        //                        }
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                //oTable.Cell(r - iDelrow, 2).Range.Text = row[0];
        //                //oTable.Cell(r - iDelrow, 3).Range.Text = row[1];
        //                iDelrow = iDelrow + 1;
        //            }
        //            #endregion
        //            r++;
        //        }
        //        else if (row.Length == 5)
        //        {
        //            #region 图像输出
        //            object oRow = 1;
        //            object oCol = 2;
        //            oTable.Cell(r - iDelrow, 3).Merge(oTable.Cell(r - iDelrow, 1));
        //            //oTable.Cell(r - iDelrow, 2).Merge(oTable.Cell(r, 1));
        //            if (row[4] == "1")
        //            {
        //                oCol = 1;
        //            }
        //            oTable.Cell(r - iDelrow, 1).Split(ref oRow, ref oCol);

        //            for (int iPic = 0; iPic < int.Parse(oCol.ToString()); iPic++)
        //            {
        //                string path = row[2 + iPic];
        //                if (path == "") continue;

        //                oTable.Cell(r - iDelrow, 1 + iPic).Select();
        //                object LinkToFile = false;
        //                object SaveWithDocument = true;
        //                object Anchor = myWordDoc.Application.Selection.Range;

        //                try
        //                {
        //                    #region 暂时注释旧图片打印   步长强   2013-2-21
        //                    //myWordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref LinkToFile, ref SaveWithDocument, ref Anchor);

        //                    //if (int.Parse(oCol.ToString()) == 1)
        //                    //{
        //                    //    myWordDoc.Application.ActiveDocument.InlineShapes[1].Width = 300f;//图片宽度
        //                    //    myWordDoc.Application.ActiveDocument.InlineShapes[1].Height = 250f;//图片高度
        //                    //}
        //                    //else
        //                    //{
        //                    //    myWordDoc.Application.ActiveDocument.InlineShapes[1].Width = 220f;//图片宽度
        //                    //    myWordDoc.Application.ActiveDocument.InlineShapes[1].Height = 200f;//图片高度
        //                    //} 
        //                    #endregion
        //                }
        //                catch (Exception ep)
        //                {
        //                    //MessageBox.Show(ep.Message + "\r\n\r\n" + ep.StackTrace, "WORD取图异常");
        //                }
        //                try
        //                {
        //                    #region 暂时注释  步长强   2013-2-21
        //                    ////将图片设置为四周环绕型


        //                    //Microsoft.Office.Interop.Word.Shape s = myWordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
        //                    //if (int.Parse(oCol.ToString()) == 1)
        //                    //{
        //                    //    s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapThrough;
        //                    //    s.Left += (oTable.Cell(r - iDelrow, 1).Width - s.Width) / 2 - 5;
        //                    //}
        //                    //else
        //                    //{
        //                    //    s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapSquare;
        //                    //} 
        //                    #endregion
        //                }
        //                catch { }
        //            }
        //            #endregion
        //            r++;
        //        }
        //        else
        //        {
        //            //row.Length == 6
        //            if (row[0] != "")//组合，需要显示组合名称，并且合并单元格
        //            {
        //                object oRow = 1;
        //                object oCol = 3;


        //                #endregion

        //                oTable.Cell(r - iDelrow, 1).Split(ref oRow, ref oCol);
        //                oTable.Cell(r - iDelrow, 3).Merge(oTable.Cell(r - iDelrow, 2));

        //                string zuheName = row[0];
        //                #region 合并单元格  步长强   2013-2-20
        //                if (row[0] == rowNext[0])
        //                {
        //                    //如果单元格有重复，文字也要还原已经有的  
        //                    if (isNewHeBing == false)
        //                    {
        //                        heBingHangRowIndex = r - iDelrow;
        //                        isNewHeBing = true;
        //                    }
        //                    oTable.Cell(r - iDelrow, 0).Range.Text = row[0];
        //                    zuheName = "";
        //                }
        //                else
        //                {
        //                    isNewHeBing = false;
        //                }

        //                oTable.Cell(r - iDelrow, 1).Range.Text = zuheName;//组合名称
        //                oTable.Cell(r - iDelrow, 1).Range.Paragraphs.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //                oTable.Cell(r - iDelrow, 1).Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        //                oTable.Cell(r - iDelrow, 2).Range.Text = row[1];//项目名称



        //                //oTable.Cell(r, 3).Range.Shading.ForegroundPatternColor = WdColor.wdColorRed;

        //                bool isError = false;
        //                //if (row[2].Trim().Length > 0)
        //                //{
        //                //    if (char.IsNumber(row[2].Trim(), row[2].Length - 1))
        //                //    {
        //                //        //数值型的，标准的


        //                //    }
        //                //    else
        //                //    {
        //                //        //if ( row[2].IndexOf("↑") > 0 || row[2].IndexOf("↓") > 0)
        //                //        if (row[3].Trim() == "")
        //                //        {
        //                //            //没标准的不管
        //                //        }
        //                //        else
        //                //        {
        //                //            if (row[2].Trim() != row[3].Trim())
        //                //            {
        //                //                //标准不配的，或者数值内含有其它字符的（↑↓）


        //                //                isError = true;
        //                //            }
        //                //        }
        //                //    }
        //                //}
        //                try
        //                {
        //                    isError = bool.Parse(row[5].Trim());
        //                }
        //                catch { }
        //                oTable.Cell(r - iDelrow, 3).Range.Text = row[2];//结果
        //                switch (KSLeiBie)
        //                {
        //                    case "检验":
        //                        oTable.Cell(r - iDelrow, 4).Range.Text = row[3];//参考值


        //                        break;
        //                    case "检查":
        //                    case "放射":
        //                        oTable.Cell(r - iDelrow, 3).Merge(oTable.Cell(r - iDelrow, 4));
        //                        oTable.Cell(r - iDelrow, 3).Range.Text = row[2];//结果
        //                        break;
        //                    default:
        //                        oTable.Cell(r - iDelrow, 4).Range.Text = row[3];//参考值


        //                        break;
        //                }
        //                if (isError)
        //                {
        //                    switch (strSign)
        //                    {
        //                        case "0":
        //                            //正常格式
        //                            break;
        //                        case "1":
        //                            oTable.Cell(r - iDelrow, 3).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //                            break;
        //                        case "2":
        //                            oTable.Cell(r - iDelrow, 3).Range.Font.Color = WdColor.wdColorRed;
        //                            break;
        //                        case "3":
        //                            oTable.Cell(r - iDelrow, 3).Range.Font.Italic = 2;
        //                            break;
        //                        case "4":
        //                            oTable.Cell(r - iDelrow, 3).Range.Font.Bold = 1;
        //                            break;

        //                    }
        //                }
        //                #region 进行单元格的合并，上下行的合并   步长强  2013-2-20

        //                if (row[0] == rowNext[0])
        //                {
        //                    oTable.Cell(heBingHangRowIndex - 1, 1).Merge(oTable.Cell(r - iDelrow, 1));
        //                }
        //                #endregion
        //                //if (tableData[r - 2][0] == row[0])
        //                //{
        //                //    oTable.Cell(r - iDelrow, 1).Range.Text = "";
        //                //}
        //            }
        //            else//项目
        //            {
        //                oTable.Cell(r - iDelrow, 1).Range.Text = row[1];
        //                oTable.Cell(r - iDelrow, 2).Range.Text = row[2];
        //                //oTable.Cell(r, 3).Range.Font.Color = WdColor.wdColorBlue;
        //                //oTable.Cell(r, 3).Range.Shading.ForegroundPatternColor = WdColor.wdColorRed;
        //                if (KSLeiBie == "放射")
        //                {
        //                    oTable.Cell(r - iDelrow, 2).Merge(oTable.Cell(r - iDelrow, 3));
        //                }
        //                else
        //                {
        //                    oTable.Cell(r - iDelrow, 3).Range.Text = row[3];
        //                }

        //            }
        //            r++;
        //        }
        //    }




        //    #endregion
        //    for (int r = oTable.Rows.Count; r > 1; r--)
        //    {
        //        string strName = oTable.Cell(r, 1).Range.Text.Replace("\r", "").Replace("\a", "").Trim();
        //        if (oTable.Cell(r, 1).Range.Text.Replace("\r", "").Replace("\a", "") == oTable.Cell(r - 1, 1).Range.Text.Replace("\r", "").Replace("\a", "") && oTable.Cell(r, 1).Range.Text.Replace("\r", "").Replace("\a", "") != "" && strName != "")
        //        {

        //            // oTable.Cell(r, 1).Merge(oTable.Cell(r - 1, 1));
        //            oTable.Cell(r - 1, 1).Range.Text = strName;
        //            oTable.Cell(r - 1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //            oTable.Cell(r - 1, 1).Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        //        }

        //        try
        //        {
        //            if (oTable.Cell(r, 1).Range.Text == oTable.Cell(r, 2).Range.Text && oTable.Cell(r, 1).Range.Text == oTable.Cell(r, 3).Range.Text)
        //            {
        //                oTable.Cell(r, 3).Merge(oTable.Cell(r, 1));
        //                oTable.Cell(r - 1, 1).Range.Text = strName;
        //                #region 删除多余的空表格行  步长强  2013-2-21
        //                object shift = System.Reflection.Missing.Value;
        //                oTable.Cell(r, 1).Delete(ref shift);
        //                #endregion
        //            }
        //        }
        //        catch (SystemException ex)
        //        {

        //        }
        //    }

        //    //for (int r = tableData.Count - 1; r >= 0; r--)
        //    //{
        //    //    if (tableData[r].Length != 4 || tableData[r][0] == "")
        //    //        continue;
        //    //    if (tableData[r][0] == tableData[r - 1][0])
        //    //    {
        //    //        //oTable.Cell(r + 1, 1).Merge(oTable.Cell(r, 1));
        //    //        oTable.Cell(r - 10, 1).Merge(oTable.Cell(r - 9, 1));
        //    //    }
        //    //}
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}
        //}
        //public void ExportItemInfo(string content, string nd, Microsoft.Office.Interop.Word.Document myWordDoc, Bookmark bk, string pictures, CustomerRegDto cus, bool xsys, bool isItem, string KSLeiBie)
        //{
        //    try
        //    {
        //        object oMissing = System.Reflection.Missing.Value;
        //        object index = nd;

        //        bk = myWordDoc.Bookmarks.get_Item(ref index);
        //        #region zht 屏蔽 代码  20120817
        //        //if (!isItem)
        //        //{
        //        //    bk.Range.Text = content.Trim();
        //        //}
        //        #endregion
        //        string strSign = "0";
        //        if (xsys && !isItem)
        //        {
        //            //项目阳性显示格式                 

        //            object start = bk.Start;
        //            object end = bk.Start + content.Trim().Length;
        //            //myWordDoc.Application.ActiveDocument.Range(ref start, ref end).Font.Color = WdColor.wdColorRed;
        //            switch (strSign)
        //            {
        //                case "0":
        //                    //正常格式
        //                    break;
        //                case "1":
        //                    myWordDoc.Application.ActiveDocument.Range(ref start, ref end).Shading.BackgroundPatternColor = WdColor.wdColorGray15;
        //                    break;
        //                case "2":
        //                    myWordDoc.Application.ActiveDocument.Range(ref start, ref end).Font.Color = WdColor.wdColorRed;
        //                    break;
        //                case "3":
        //                    myWordDoc.Application.ActiveDocument.Range(ref start, ref end).Font.Italic = 2;
        //                    break;
        //                case "4":
        //                    myWordDoc.Application.ActiveDocument.Range(ref start, ref end).Font.Bold = 1;
        //                    break;
        //            }
        //        }
        //        if (pictures != "")
        //        {

        //            Microsoft.Office.Interop.Word.Table oTable;
        //            Microsoft.Office.Interop.Word.Range wrdRng = myWordDoc.Bookmarks.get_Item(ref index).Range;
        //            // List<string> picPaths = GetImagePaths(pictures, cus);

        //            bool isPIC = false;

        //            Bookmark bksj = null;

        //            object obsj = nd;
        //            bksj = myWordDoc.Bookmarks.get_Item(ref obsj);
        //            bksj.Range.Text = content;
        //            if (isPIC)
        //            {
        //                Bookmark bkPic = null;
        //                object LinkToFile = false;
        //                object SaveWithDocument = true;

        //                int iPic = 1;
        //                //foreach (string path in picPaths)
        //                //{
        //                //    index = nd.Replace("】", "") + "_PIC" + iPic.ToString() + "】";
        //                //    iPic++;
        //                //    try
        //                //    {
        //                //        bkPic = myWordDoc.Bookmarks.get_Item(ref index);
        //                //    }
        //                //    catch
        //                //    {
        //                //        continue;
        //                //    }
        //                //    object Anchor = bkPic.Range;
        //                //    bkPic.Range.InlineShapes.AddPicture(path, ref LinkToFile, ref SaveWithDocument, ref Anchor);
        //                //    //iPic++;
        //                //}
        //            }
        //            else
        //            {
        //                int oRow = 1;
        //                int oCol = 1;

        //                oTable = myWordDoc.Tables.Add(wrdRng, oRow, oCol, ref oMissing, ref oMissing);
        //                oTable.Borders.Enable = 0;
        //                oTable.Cell(1, 1).Range.Text = content;// +content + content;

        //                if (picPaths.Count > 1)
        //                {
        //                    double imgROW = (double)picPaths.Count / 2;
        //                    oRow = int.Parse(imgROW.ToString("0"));
        //                    oCol = 2;
        //                }

        //                oTable = myWordDoc.Tables.Add(wrdRng, oRow, oCol, ref oMissing, ref oMissing);
        //                oTable.Borders.Enable = 0;
        //                oTable.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;

        //                //oTable.Cell(1, 1).Range.InsertBefore(content + content + content);

        //                int i = 1;
        //                int intPic = 1;

        //                //if (picPaths.Count > 1)
        //                //{
        //                //    oTable.Range.Cells.Width = 320;
        //                //    oTable.Range.Cells.Height = 240;
        //                //}
        //                //else
        //                //{
        //                oTable.Range.Cells.Width = 440;
        //                oTable.Range.Cells.Height = 330;
        //                //}

        //                foreach (string path in picPaths)
        //                {
        //                    object rg;
        //                    if (i % 2 == 1)
        //                    {
        //                        if (picPaths.Count > 1)
        //                        {
        //                            oTable.Cell(intPic, 1).Width = 220;
        //                            oTable.Cell(intPic, 1).Height = 160;
        //                        }
        //                        else
        //                        {
        //                            oTable.Cell(intPic, 1).Width = 440;
        //                            oTable.Cell(intPic, 1).Height = 330;
        //                        }
        //                        oTable.Cell(intPic, 1).Select();
        //                        rg = oTable.Cell(intPic, 1).Range;

        //                    }
        //                    else
        //                    {
        //                        oTable.Cell(intPic, 2).Width = 220;
        //                        oTable.Cell(intPic, 2).Height = 160;
        //                        oTable.Cell(intPic, 2).Select();
        //                        rg = oTable.Cell(intPic, 2).Range;
        //                        intPic++;
        //                    }

        //                    object LinkToFile = false;
        //                    object SaveWithDocument = true;
        //                    object Anchor = myWordDoc.Application.Selection.Range;
        //                    //myWordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref LinkToFile, ref SaveWithDocument, ref Anchor);
        //                    //myWordDoc.Application.ActiveDocument.InlineShapes[1].Width = 370f;//图片宽度
        //                    //myWordDoc.Application.ActiveDocument.InlineShapes[1].Height = 280f;//图片高度

        //                    ((Microsoft.Office.Interop.Word.Range)rg).InlineShapes.AddPicture(path, ref LinkToFile, ref SaveWithDocument, ref rg);
        //                    Microsoft.Office.Interop.Word.Shape s = ((Microsoft.Office.Interop.Word.Range)rg).Application.ActiveDocument.InlineShapes[0].ConvertToShape();
        //                    ////将图片设置为四周环绕型


        //                    //Microsoft.Office.Interop.Word.Shape s = myWordDoc.Application.ActiveDocument.InlineShapes[0].ConvertToShape();
        //                    s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapThrough;
        //                    s.Top = 0;
        //                    i += 1;
        //                }
        //            }

        //        }
        //    }
        //    catch { return; }
        //}

        #endregion
    }
}
