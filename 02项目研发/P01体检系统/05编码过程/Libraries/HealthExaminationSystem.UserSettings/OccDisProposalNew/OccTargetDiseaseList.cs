using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposalNew
{
    public partial class OccTargetDiseaseList : UserBaseForm
    {
        private readonly IOccTargetDiseaseAppService _OccTargetDiseaseAppServicee;
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;
        private IWorkbook _workbook;
        public OccTargetDiseaseList()
        {
            InitializeComponent();
            _OccTargetDiseaseAppServicee = new OccTargetDiseaseAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OccTargetDiseaseList_Load(object sender, EventArgs e)
        {
            SeachOccTargetDiseaseDto show = new SeachOccTargetDiseaseDto();
            show.IsOk=3;
            var data = _OccTargetDiseaseAppServicee.ShowOccHazardFactor(show);
            gridControl1.DataSource = data.OrderBy(o=>o.OccHazardFactors?.Text).ToList(); 
            gridView1.Columns[gridColumn8.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[gridColumn8.FieldName].DisplayFormat.Format = new CustomFormatter(FormatEnable);
            //检查类型
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
            var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            comboBoxEdit1.Properties.DataSource = dl;

            //危害因素
            OutOccHazardFactorDto search = new OutOccHazardFactorDto();
            search.IsActive = 3;
            var data1 = _OccHazardFactorAppService.ShowOccHazardFactor(search);
            searchLookUpEdit1.Properties.DataSource = data1;

            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 0, Name = "启用" });
            list.Add(new EnumModel { Id = 1, Name = "停用" });
            editActive.Properties.DataSource = list;
        }
        private string FormatEnable(object arg)
        {
            try
            {
                if ((int)arg == 0)
                {
                    return "启用";
                }
                else
                {
                    return "停用";
                }
            }
            catch
            {
                return "停用";
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (var frm = new OccTargetDiseaseEdit())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    simpleButton1.PerformClick();
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var showTbmOccTargetDisease = new SeachOccTargetDiseaseDto();          
            if (!string.IsNullOrWhiteSpace(comboBoxEdit1.Text.Trim()))
                showTbmOccTargetDisease.CheckType = comboBoxEdit1.Text.Trim();
            if (searchLookUpEdit1.EditValue !=null )
                showTbmOccTargetDisease.OccHazardFactorsId = (Guid)searchLookUpEdit1.EditValue;
            if (!string.IsNullOrWhiteSpace(textEdit2.Text.Trim()))
                showTbmOccTargetDisease.OccDisName = textEdit2.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textEdit3.Text.Trim()))
                showTbmOccTargetDisease.ConTrName = textEdit3.Text.Trim();
            if (editActive.EditValue != null)
               showTbmOccTargetDisease.IsOk = (int)editActive.EditValue;
            try
            {
                //indexls.Clear();
                var result = _OccTargetDiseaseAppServicee.ShowOccHazardFactor(showTbmOccTargetDisease);
                gridControl1.DataSource = null;
                gridControl1.DataSource = result.OrderBy(o=>o.OccHazardFactors?.Text).ToList();



            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1);
            if (id == null)
            {
                ShowMessageBoxError("尚未选定任何项目！");
                return;
            }
            var name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn2);

            if (XtraMessageBox.Show($"确定要删除项目 {name} 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                try
                {

                    // simpleButtonQuery.PerformClick();
                    var dto = gridControl1.GetFocusedRowDto<OutTbmOccTargetDiseaseDto>();
                    AutoLoading(() =>
                    {
                        _OccTargetDiseaseAppServicee.Del(new EntityDto<Guid>
                        {
                            Id = (Guid)id
                        });

                        gridControl1.RemoveDtoListItem(dto);

                       // var ss = (List<OutTbmOccTargetDiseaseDto>)gridControl1.DataSource;
                       //ss.Remove(dto);
                       // gridControl1.RefreshDataSource();

                    }, Variables.LoadingDelete);
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBoxError(exception.ToString());
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1);
            var old = gridView1.GetFocusedRow() as OutTbmOccTargetDiseaseDto;
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
                using (var frm = new OccTargetDiseaseEdit((Guid)id, old))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {                  
                        ModelHelper.CustomMapTo(frm._Model, old);
                        gridControl1.RefreshDataSource();

                    }
                }
        }

        private void editActive_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;


            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //SaveFileDialog fileDialog = new SaveFileDialog();
            ////对话框初始路径
            //fileDialog.FileName = "电测听人员名单.xls";//
            ////默认保存的文件名
            //fileDialog.FilterIndex = 2;//默认选择文本文件
            //fileDialog.DefaultExt = ".xls";
            ////默认保存类型，如果过滤条件选所有文件且没写后缀名，则默认补上该默认值
            //fileDialog.DereferenceLinks = false;
            ////返回快捷方式的路径而不是快捷方式映射的文件的路径
            //fileDialog.Title = "李药师的保存文件对话框";
            //fileDialog.RestoreDirectory = true;//没感觉每次都打开都回到了初始路径，你可以试一下
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string strfilename = fileDialog.FileName;
            //    gridView1.ExportToXls(strfilename);
            //}
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var datas = ExcelToDtoList(openFileDialog.FileName, "Sheet", true);                     
            }
                
           
        }
        //导入excel
        private List<OutTbmOccTargetDiseaseDto> ExcelToDtoList(string _fileName, string sheetName, bool isFirstRowColumn)
        {
            var result = new List<OutTbmOccTargetDiseaseDto>();
            
            FileStream _fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);       
            // 2007版本
            _workbook = new XSSFWorkbook(_fs);
           

            ISheet sheet;
            if (sheetName != null)
            {
                // 如果没有找到指定的 SheetName 对应的 Sheet，则尝试获取第一个 Sheet
                sheet = _workbook.GetSheet(sheetName) ?? _workbook.GetSheetAt(0);
            }
            else
            {
                sheet = _workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                var firstRow = sheet.GetRow(0);
                // 一行最后一个 Cell 的编号，即总的列数
                int cellCount = firstRow.LastCellNum;
               
                // 最后一列的标号
                var rowCount = sheet.LastRowNum;
                var _commonAppService = new CommonAppService();
                var rsl = new List<OutOccTargetDiseaseExcel>();
                string y1 = "";
                string y2 = "";
                string jjz = "";
                for (var i = 1; i <= rowCount; ++i)
                {
                    string comname = "";
                    var row = sheet.GetRow(i);
                    // 没有数据的行默认是 NULL
                    if (row == null)
                        continue;
                  

                    var rs = new OutOccTargetDiseaseExcel();
                  
                    //string y3 = "";
                    //string y4 = "";
                    //string y5 = "";
                    //string y6 = "";
                    //string y7 = "";
                   
                    //string y8 = "";
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (sheet.GetRow(0).GetCell(j) == null)
                        {
                            continue;
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "有害因素")
                        {
                            rs.OccHazardFactorsName = row.GetCell(j).ToString();
                            rs.OccHazardFactorsName = rs.OccHazardFactorsName.Replace("（", "(").Replace("）", ")");
                            rs.OccHazardFactorsName = Regex.Replace(rs.OccHazardFactorsName.Replace("（", "(").Replace("）", ")"), @"\([^\(]*\)", "");                            
                            if (rs.OccHazardFactorsName.ToString() != "")
                            {
                                y1 = rs.OccHazardFactorsName;
                            }
                            else
                            {
                                
                                rs.OccHazardFactorsName = y1;
                            }
                            if (string.IsNullOrWhiteSpace(rs.OccHazardFactorsName))
                                rs.OccHazardFactorsName = "";
                        }                     
                        if (sheet.GetRow(0).GetCell(j).ToString() == "检查类型")
                        {
                            rs.CheckType = row.GetCell(j).ToString();
                            if (rs.CheckType.Substring(0, 1).ToString() != "C")
                            {
                                y2 = rs.CheckType;
                            }
                            else
                            {
                                comname = "B";
                                rs.CheckType = y2;
                            }
                            if (string.IsNullOrWhiteSpace(rs.CheckType))
                                rs.CheckType="";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "职业健康")
                        {
                            rs.OccDiseases = row.GetCell(j).ToString();
                            //if (rs.OccDiseases.Substring(0, 1).ToString() != "A")
                            //{
                            //    y3 = rs.OccDiseases;
                            //}
                            //else
                            //{
                            //    rs.OccDiseases = y3;
                            //}
                            if (string.IsNullOrWhiteSpace(rs.OccDiseases))
                                rs.OccDiseases="";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "症状询问")
                        {
                            rs.InquiryTips = row.GetCell(j).ToString();
                            //if (rs.InquiryTips.Substring(0, 1).ToString() != "A")
                            //{
                            //    y4 = rs.InquiryTips;
                            //}
                            //else
                            //{
                            //    rs.InquiryTips = y4;
                            //}
                            if (string.IsNullOrWhiteSpace(rs.InquiryTips))
                                rs.InquiryTips = "";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "必检项目")
                        {
                            rs.MustIemGroups = row.GetCell(j).ToString();
                            //if (rs.InquiryTips.Substring(0, 1).ToString() != "A")
                            //{
                            //    y5 = rs.MustIemGroups;
                            //}
                            //else
                            //{
                            //    rs.MustIemGroups = y5;
                            //}
                            if (string.IsNullOrWhiteSpace(rs.MustIemGroups))
                                rs.MustIemGroups = "";

                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "选检项目")
                        {
                            rs.MayIemGroups = row.GetCell(j).ToString();
                            //if (rs.MayIemGroups.Substring(0, 1).ToString() != "A")
                            //{
                            //    y6 = rs.MayIemGroups;
                            //}
                            //else
                            //{
                            //    rs.MayIemGroups = y6;
                            //}
                            if (string.IsNullOrWhiteSpace(rs.MayIemGroups))
                                rs.MayIemGroups = "";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "症状大类")
                        {
                            rs.Symptoms = row.GetCell(j).ToString();
                            //if (rs.Symptoms!="" &&  rs.Symptoms.Substring(0, 1).ToString() != "A")
                            //{
                            //    y7 = rs.Symptoms;
                            //}
                            //else
                            //{
                            //    rs.Symptoms = y7;
                            //}
                            if (string.IsNullOrWhiteSpace(rs.Symptoms))
                                rs.Symptoms = "";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "职业禁忌证")
                        {
                            rs.Contraindications = row.GetCell(j).ToString();
                            if (comname != "B")
                            {
                                jjz = rs.Contraindications;
                            }
                            else
                            {
                                jjz = jjz +"、" + rs.Contraindications;
                                rs.Contraindications =jjz;
                                //var riskls = rsl.Where(o => o.OccHazardFactorsName == y1 && o.CheckType == y2);
                                //foreach (var risk in riskls)
                                //{
                                //    risk.Contraindications = rs.Contraindications;

                                //}
                            }
                            if (string.IsNullOrWhiteSpace(rs.Contraindications))
                                rs.Contraindications = "";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "检查对象")
                        {
                            rs.Crowd = row.GetCell(j).ToString();
                            //if (rs.Crowd.Substring(0, 1).ToString() != "A")
                            //{
                            //    y8 = rs.Crowd;
                            //}
                            //else
                            //{
                            //    rs.Crowd = y8;
                            //}
                            if (string.IsNullOrWhiteSpace(rs.Crowd))
                                rs.Crowd = "";
                        }
                      
                    }
                    rsl.Add(rs);
                  
                }
                var lls = rsl.GroupBy(o => new { o.OccHazardFactorsName, o.CheckType }).Select(o => new OutOccTargetDiseaseExcel
                {
                    CheckType = o.Key.CheckType,
                    OccHazardFactorsName = o.Key.OccHazardFactorsName,
                    Crowd = o.First().Crowd,
                    InquiryTips = o.First().InquiryTips,
                    MayIemGroups = o.First().MayIemGroups,
                    MustIemGroups = o.First().MustIemGroups,
                    OccDiseases = o.First().OccDiseases,
                    Symptoms = o.First().Symptoms,
                    Contraindications = o.Last().Contraindications
                }).ToList();
                //var lls = rsl.GroupBy(o => new { o.OccHazardFactorsName, o.CheckType }).Select(o => new OutOccTargetDiseaseExcel
                //{
                //    CheckType = o.Key.CheckType,
                //    OccHazardFactorsName = o.Key.OccHazardFactorsName,
                //    Crowd = o.FirstOrDefault(r=>r.OccHazardFactorsName==o.Key.OccHazardFactorsName && r.CheckType==o.Key.CheckType).Crowd,
                //    InquiryTips = o.FirstOrDefault(r => r.OccHazardFactorsName == o.Key.OccHazardFactorsName && r.CheckType == o.Key.CheckType).InquiryTips,
                //    MayIemGroups = o.FirstOrDefault(r => r.OccHazardFactorsName == o.Key.OccHazardFactorsName && r.CheckType == o.Key.CheckType).MayIemGroups,
                //    MustIemGroups = o.FirstOrDefault(r => r.OccHazardFactorsName == o.Key.OccHazardFactorsName && r.CheckType == o.Key.CheckType).MustIemGroups,
                //    OccDiseases = o.FirstOrDefault(r => r.OccHazardFactorsName == o.Key.OccHazardFactorsName && r.CheckType == o.Key.CheckType).OccDiseases,
                //    Symptoms = o.FirstOrDefault(r => r.OccHazardFactorsName == o.Key.OccHazardFactorsName && r.CheckType == o.Key.CheckType).Symptoms,
                //    Contraindications = o.LastOrDefault(r => r.OccHazardFactorsName == o.Key.OccHazardFactorsName && r.CheckType == o.Key.CheckType).Contraindications
                //}).ToList();
                result = _OccTargetDiseaseAppServicee.AddExcel(lls);
            }
            
            XtraMessageBox.Show("导入:" + result.Count + "人;");
            return result;
        }
    }
}
