using Abp.Application.Services.Dto;
using DevExpress.Utils;
using NPOI.SS.UserModel;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.GroupReport
{
    public partial class frmClientCusList : UserBaseForm
    {
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService;
        public List<Guid> clientregID;
        public List<Guid> CusId;
        public DateTime? star;
        public DateTime? end;
        //单位人员信息
        private List<SimGroupClientCusDto> ClientCusDtos = new List<SimGroupClientCusDto>();
        private IGroupReportAppService GReportAppService = new GroupReportAppService();
        private ICustomerAppService customerSvr;//体检预约
        private IWorkbook _workbook;
        public frmClientCusList()
        {
            customerSvr = new CustomerAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            InitializeComponent();

           

            // repositoryItemLookUpEditSummSate.DataSource = SummSateHelper.GetSelectList();
            // repositoryItemLookUpEditPhysicalType.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();


        }

        private void frmClientCusList_Load(object sender, EventArgs e)
        {
            gridViewCustomerReg.Columns[conSummSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCustomerReg.Columns[conSummSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(SummSateHelper.SummSateFormatter);
            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();

            //检查类型
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
            var lis1 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            checkType.Properties.DataSource = lis1;
            checkType.Properties.DisplayMember = "Text";
            checkType.Properties.ValueMember = "Text";

            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            BasicDictionaryDto all = new BasicDictionaryDto();
            all.Value = -1;
            all.Text = "全部";
            Examination.Add(all);
            lookUpEditExaminationCategories.Properties.DataSource = Examination;
            List<ClientTeamInfoDto> list = new List<ClientTeamInfoDto>();
            foreach (var cRegid in clientregID)
            {
                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = cRegid;
                var   nowlist = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = cRegid });
                list.AddRange(nowlist);
            }
            txtTeamID.EditValue = null;
             txtTeamID.Properties.DataSource = list;
            ClientRegIdDto clientRegIdDto = new ClientRegIdDto();             
            clientRegIdDto.Idlist = clientregID;
             ClientCusDtos = GReportAppService.GRClientRegCusSmp(clientRegIdDto).ToList();
          
            gridControlCustomerReg.DataSource = ClientCusDtos;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var cuslist = ClientCusDtos.ToList();
            if (dateEditStart.EditValue != null && dateEditEnd.EditValue != null)
            {
                var  StartDate = dateEditStart.DateTime;
                var EndtDate = dateEditEnd.DateTime.AddDays(1);

                if (comTimeType.Text.Contains("登记时间"))
                {
                    cuslist = cuslist.Where(o => o.LoginDate >= StartDate && o.LoginDate < EndtDate).ToList();
                }
                else
                {
                    cuslist = cuslist.Where(o => o.BookingDate >= StartDate && o.LoginDate < EndtDate).ToList();
                }
            }
            
            if (lookUpEditExaminationCategories.EditValue != null && lookUpEditExaminationCategories.Text != "全部")
            {
                cuslist = cuslist.Where(o => o.PhysicalType == (int)lookUpEditExaminationCategories.EditValue).ToList();
            }
            if (lookUpEditSumStatus.EditValue != null && lookUpEditSumStatus.Text != "全部")
            {
                cuslist = cuslist.Where(o => o.SummSate == (int)lookUpEditSumStatus.EditValue).ToList();
            }
            if (!string.IsNullOrEmpty(checkType.Text))
            {
               // List<string> departmentIds = new List<string>();
               // string[] arrIds = checkType.Properties.GetCheckedItems().ToString().Split(',');
                //if (arrIds.Length > 0 && !string.IsNullOrEmpty(arrIds[0]))
                //{
                //    foreach (string item in arrIds)
                //    {
                //        departmentIds.Add(item);
                //    }
                //}
                string post= checkType.Properties.GetCheckedItems().ToString();
                cuslist = cuslist.Where(o => o.PostState !=null && post.Contains(o.PostState)).ToList();

            }
            if (txtTeamID.EditValue != null)
            {
                cuslist = cuslist.Where(o => o.ClientTeamInfoId == (Guid)txtTeamID.EditValue).ToList();

            }

            gridControlCustomerReg.DataSource = cuslist;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

           var selectIndexes = gridControlCustomerReg.GetSelectedRowDtos<SimGroupClientCusDto>();
            if (selectIndexes == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (selectIndexes.Count == 0)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            CusId = selectIndexes.Select(o=>o.Id).ToList();
            if (dateEditStart.EditValue != null && dateEditEnd.EditValue != null)
            {
                star = dateEditStart.DateTime;
                end = dateEditEnd.DateTime;
            }
                this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butInExcel_Click(object sender, EventArgs e)
        {
            var Bmlist=   Import();

            if (Bmlist != null && Bmlist.Count > 0)
            {
                CusRegBMDto bMDto = new CusRegBMDto();
                bMDto.BMlist = Bmlist;
                var ClientCusDtos = GReportAppService.GRClientRegCusBM(bMDto).ToList();
                gridControlCustomerReg.DataSource = ClientCusDtos;
            }
            else
            {
                MessageBox.Show("无体检数据");
            }
        }
        /// <summary>
        /// 选择文件导入
        /// </summary>
        public List<string> Import()
        {
            openFileDialog.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();
                AutoLoading(() =>
                {
                    dt = ExcelToDataTable(openFileDialog.FileName, "Sheet", true);
                });
                string Err = "";
                if (!dt.Columns.Contains("体检号"))
                { Err = "模板中缺少'体检号'列'\r\n"; }
                List<string> BM = new List<string>();
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    var cusBM = dt.Rows[row]["体检号"].ToString();
                    BM.Add(dt.Rows[row]["体检号"].ToString());
                }
                return BM;
            }
            return null;
        }

        /// <summary>
        /// 将 Excel 中的数据导入到 DataTable 中
        /// </summary>
        /// <param name="sheetName">Excel 工作薄 Sheet 的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是 DataTable 的列名</param>
        /// <returns>返回的 DataTable</returns>
        public DataTable ExcelToDataTable(string _fileName, string sheetName, bool isFirstRowColumn)
        {
            var data = new DataTable();

            FileStream _fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            //_workbook = new HSSFWorkbook(_fs);      
            _workbook = WorkbookFactory.Create(_fs);//使用接口，自动识别excel2003/2007格式
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
                int startRow;
                if (isFirstRowColumn)
                {
                    List<string> conName = new List<string>();

                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        var cell = firstRow.GetCell(i);
                        var cellValue = cell?.StringCellValue;

                        if (cellValue != null)
                        {

                            var column = new DataColumn(cellValue);
                            if (!conName.Contains(cellValue))
                            {
                                data.Columns.Add(column);
                                conName.Add(cellValue);
                            }
                            else
                            {
                                data.Columns.Add(column + i.ToString());
                                conName.Add(cellValue);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }

                // 最后一列的标号
                var rowCount = sheet.LastRowNum;
                for (var i = startRow; i <= rowCount; ++i)
                {
                    var row = sheet.GetRow(i);

                    // 没有数据的行默认是 NULL       
                    if (row == null)
                        continue;
                    //if (row.GetCell(0) == null)
                    //{
                    //    MessageBox.Show("体检号不能为空！");
                    //    return new DataTable();
                    //}
                    //if (row.GetCell(0) == null)
                    //{
                    //    MessageBox.Show("体检号不能为空！");
                    //    return new DataTable();
                    //}
                    List<string> namels = new List<string>();
                    var dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        //同理，没有数据的单元格都默认是 NULL
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                            //namels.Add();
                        }
                    }

                    //判断增加分组信息
                    //编码为空时判断                   
                    data.Rows.Add(dataRow);
                }


            }

            return data;

        }

    }
}
