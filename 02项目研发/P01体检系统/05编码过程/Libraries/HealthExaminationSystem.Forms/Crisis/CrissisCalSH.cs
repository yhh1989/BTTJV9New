using Abp.Application.Services.Dto;
using DevExpress.ExpressApp;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Critical;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Critical;
using Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public partial class CrissisCalSH : UserBaseForm
    {
        private ICriticalAppService criticalAppService = new CriticalAppService();
        private readonly IInspectionTotalAppService _inspectionTotalService;
        private readonly ICustomerAppService _customerAppServicep;
        public CrissisCalSH()
        {
            
            InitializeComponent();
            _inspectionTotalService = new InspectionTotalAppService();
            _customerAppServicep = new CustomerAppService();
        }

        private void CrissisCalSH_Load(object sender, EventArgs e)
        {
            searchLookUpEditClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            lookUpEditSHState.Properties.DataSource = CrisisVisitSateHelper.GetSHList();
            lookUpEditCriType.Properties.DataSource = CriticalTypeStateHelper.GetList();
            searchLookUpEditDepart.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            lookUpEditShortState.Properties.DataSource = MessageStateHelper.GetList();
            comClient.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            comDepart.DataSource = DefinedCacheHelper.GetDepartments();
            comDoct.DataSource = DefinedCacheHelper.GetComboUsers();
            comMessUser.DataSource = DefinedCacheHelper.GetComboUsers();
            LookUpEditportUser.DataSource= DefinedCacheHelper.GetComboUsers();

            #region 加载输入
            //gridView1.Columns["Sex"].DisplayFormat.FormatType = FormatType.Custom;
            //gridView1.Columns["Sex"].DisplayFormat.Format =
            //    new CustomFormatter(SexHelper.CustomSexFormatter);

            gridView1.Columns["CrisisLever"].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns["CrisisLever"].DisplayFormat.Format =
                new CustomFormatter(CriticalTypeStateHelper.CriticalTypeStateFormatter);


            gridView3.Columns["CrisisLever"].DisplayFormat.FormatType = FormatType.Custom;
            gridView3.Columns["CrisisLever"].DisplayFormat.Format =
                new CustomFormatter(CriticalTypeStateHelper.CriticalTypeStateFormatter);

            gridView1.Columns["CrisisVisitSate"].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns["CrisisVisitSate"].DisplayFormat.Format =
                new CustomFormatter(CrisisVisitSateHelper.CriticalTypeStateFormatter);

            gridView1.Columns["MessageState"].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns["MessageState"].DisplayFormat.Format =
                new CustomFormatter(MessageStateHelper.CriticalTypeStateFormatter);
            #endregion

            gridView1.Columns[conCrissMessageState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[conCrissMessageState.FieldName].DisplayFormat.Format = new CustomFormatter(ShortMessageStateHelper.ShortMessageStateFormatter);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchtCrisisMessageDto searchtCrisisMessageDto = new SearchtCrisisMessageDto();
            if (!string.IsNullOrEmpty(textEditCustomerBM.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.CustomerBM = textEditCustomerBM.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(textEditName.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.Name = textEditName.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(dateEditReportStar.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.CheckDateStart = dateEditReportStar.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEditReportEnd.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.CheckDateEnd = dateEditReportEnd.DateTime.AddDays(1);
            }
            if (!string.IsNullOrEmpty(searchLookUpEditClient.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.ClientRegId = (Guid)searchLookUpEditClient.EditValue;
            }
           

            if (!string.IsNullOrEmpty(dateEditShortStar.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.MessageTimeStar = dateEditShortStar.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEditShortEnd.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.MessageTimeEnd = dateEditShortEnd.DateTime.AddDays(1);
            }
            if (!string.IsNullOrEmpty(lookUpEditSHState.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.CrisisVisitSate = (int)lookUpEditSHState.EditValue;
            }
            if (!string.IsNullOrEmpty(lookUpEditCriType.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.CrisisLever = (int)lookUpEditCriType.EditValue;
            }
            if (!string.IsNullOrEmpty(searchLookUpEditDepart.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.DepartmentId = (Guid)searchLookUpEditDepart.EditValue;
            }
            if (!string.IsNullOrEmpty(lookUpEditShortState.EditValue?.ToString()))
            {
                searchtCrisisMessageDto.MessageState = (int)lookUpEditShortState.EditValue;
            }
            var critical=  criticalAppService.getCrisisMessageDto(searchtCrisisMessageDto);
            var cash = critical.Where(p => p.Isovertime==1).ToList();
            var wcs = critical.Where(p => p.Isovertime != 1).ToList();
            List<CrisisMessageDto> crisisMessageDtos = new List<CrisisMessageDto>();
            if (cash.Count > 0)
            {
                crisisMessageDtos.AddRange(cash);

            }
            if (wcs.Count > 0)
            {
                crisisMessageDtos.AddRange(wcs);

            }

            var crisisCusInfo = crisisMessageDtos.GroupBy(p => new
            {
                p.Age,
                p.BookingDate,
               // p.CheckEmployeeBMId,
                p.CustomerBM,
                p.cusRegId,
                p.Name,
                p.Sex,
                // p.Age,
                p.Mobile,
                //p.BookingDate,
                p.LoginDate,
                p.ClientInfoId,
                p.ClientName,
                p.IDCard,
                p.department
                 
            }).Select(p => new CrisisCusInfoDto
            {
                重要异常明细 = p.Select(o => new CrisisItemDto
                {
                    CheckEmployeeBMId = o.CheckEmployeeBMId,
                    CrisiChar = o.CrisiChar,
                    CrisisLever = o.CrisisLever,
                    CrisisVisitSate = o.CrisisVisitSate,
                    CrissMessageState = o.CrissMessageState,
                    ExamineTime = o.ExamineTime,
                    ExamineUserId = o.ExamineUserId,
                    FirstDateTime = o.FirstDateTime,
                    Isovertime = o.Isovertime,
                    ItemName = o.ItemName,
                    ItemResultChar = o.ItemResultChar,
                    MessageState = o.MessageState,
                    MessageTime = o.MessageTime,
                    MessageUserId = o.MessageUserId,
                     CrisiContent=o.CrisiContent,
                     Id=o.Id,
                      Unit=o.Unit
                }).ToList(),
                Age = p.FirstOrDefault().Age,
                BookingDate = p.FirstOrDefault().BookingDate,
                ClientInfoId = p.Key.ClientInfoId,
                cusRegId = p.Key.cusRegId,
                CustomerBM = p.Key.CustomerBM,
                LoginDate = p.Key.LoginDate,
                Mobile = p.Key.Mobile,
                Name = p.Key.Name,
                Sex = p.Key.Sex,
                 ClientName=p.Key.ClientName,
                  department=p.Key.department,
                   IDCard=p.Key.IDCard,
                 ExamineTime=p.FirstOrDefault().ExamineTime,
                  ExamineUserId=p.FirstOrDefault().ExamineUserId,
                   MessageState=p.FirstOrDefault().MessageState,
                    MessageTime=p.FirstOrDefault().MessageTime,
                     MessageUserId=p.FirstOrDefault().MessageUserId,
                      Id=p.FirstOrDefault().cusRegId,
                       CrisisVisitSate=p.FirstOrDefault().CrisisVisitSate
                        
                       
                      

            }).ToList();

            gridControl1.DataSource = crisisCusInfo;
            var all = crisisCusInfo.Count;
            var sh = crisisCusInfo.Where(p => p.重要异常明细.Any(o=>o.CrisisVisitSate == (int)CrisisVisitSate.Examine)).Count();
            var wsh = crisisCusInfo.Where(p=> p.重要异常明细.Any(o=>o.CrisisVisitSate !=(int)CrisisVisitSate.Examine &&o.CrisisVisitSate != (int)CrisisVisitSate.Concel)).Count();
            var Mes = crisisCusInfo.Where(p => p.重要异常明细.Any(o=> o.MessageState == (int)MessageState.HasMessage)).Count();
            var NoMes = crisisCusInfo.Where(p => p.重要异常明细.Any(o=> o.MessageState != (int)MessageState.HasMessage)).Count();
            var qxh = crisisCusInfo.Where(p => p.重要异常明细.Any(o=> o.CrisisVisitSate == (int)CrisisVisitSate.Concel)).Count();
            var stts = string.Format(@"合计：{0}人  已审核：{1}人  未审核：{2}人 已取消：{5}  已通知：{3}人  未通知：{4}人",
                all, sh, wsh, Mes, NoMes, qxh
                );
            simpleLabeTS.Text = stts;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
      
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var id = gridView1.GetFocusedRowCellValue("Id");
            var dto = gridView1.GetFocusedRow() as CrisisCusInfoDto;
            if (dto == null)
            {
                XtraMessageBox.Show(this, "请选择行", "危急值审核", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var cusName = gridView1.GetFocusedRowCellValue("Name");
            var Value = gridView1.GetFocusedRowCellValue("ItemResultChar");
            if (e.Button.Caption == "确定")
            {
                if (layoutControlItemOk.Visibility == LayoutVisibility.Never)
                {
                    MessageBox.Show("您没有确认权限！");
                    return;
                }
                if (dto.重要异常明细 != null && dto.重要异常明细.Count > 0)
                {
                    foreach (var dr in dto.重要异常明细)
                    {
                        var id = dr.Id;
                        UpCricalStateDto upCricalStateDto = new UpCricalStateDto();
                        upCricalStateDto.Id = Guid.Parse(id.ToString());
                        upCricalStateDto.Sate = (int)CrisisVisitSate.Examine;
                        criticalAppService.UpCrisisSate(upCricalStateDto);
                    }
                
               var   message="审核通过，“"+ cusName + "”的体检项目中的“"+ Value + "”为危急值。";
                XtraMessageBox.Show(this, message, "危急值审核", MessageBoxButtons.OK, MessageBoxIcon.Information);
                simpleButton1.PerformClick();
                }
            }
            else if (e.Button.Caption == "取消")
            {
                if (layoutControlItemOk.Visibility == LayoutVisibility.Never)
                {
                    MessageBox.Show("您没有取消权限！");
                    return;
                }
                if (dto.重要异常明细 != null && dto.重要异常明细.Count > 0)
                {
                    foreach (var dr in dto.重要异常明细)
                    {
                        var id = dr.Id;
                        UpCricalStateDto upCricalStateDto = new UpCricalStateDto();
                        upCricalStateDto.Id = Guid.Parse(id.ToString());
                        upCricalStateDto.Sate = (int)CrisisVisitSate.Concel;
                        criticalAppService.UpCrisisSate(upCricalStateDto);
                    }
                }
                var message ="”" +  cusName + "“的体检项目中的“" + Value + "”取消设置危急值！";
                XtraMessageBox.Show(this, message, "危急值审核", MessageBoxButtons.OK, MessageBoxIcon.Information);
                simpleButton1.PerformClick();
            }
            else if (e.Button.Caption == "详情")
            {


               
     

                var Regid = gridView1.GetFocusedRowCellValue("cusRegId")?.ToString();
                var seac = new QueryCustomerRegDto();
                    var queryCustomerDto = new QueryCustomerDto();
                    seac.Id = Guid.Parse(Regid);
                seac.Customer = new QueryCustomerDto();
                    var output = _customerAppServicep.QueryAll(new PageInputDto<QueryCustomerRegDto>
                    { TotalPages = TotalPages, CurentPage = CurrentPage, Input = seac });

                if (output.Result.Count>0)
                {
                    var CustomerReg = output.Result.First();
                    using (var frm = new FrmInspectionTotalShow(CustomerReg))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                            return;
                    }
                }

            }
            else if (e.Button.Caption == "通知")
            {
                if (layoutControlItemOk.Visibility == LayoutVisibility.Never)
                {
                    MessageBox.Show("您没有通知权限！");
                    return;
                }
                if (dto.重要异常明细 != null && dto.重要异常明细.Count > 0)
                {
                    foreach (var dr in dto.重要异常明细)
                    {
                        var id = dr.Id;
                        UpCricalStateDto upCricalStateDto = new UpCricalStateDto();
                        upCricalStateDto.Id = Guid.Parse(id.ToString());
                        upCricalStateDto.Sate = (int)MessageState.HasMessage;
                        criticalAppService.UpMessSate(upCricalStateDto);
                    }
                }
                var message = "已通知“张三”本次体检中的“重要异常结果”";
                XtraMessageBox.Show(this, message, "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                simpleButton1.PerformClick();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            
            foreach (var index in selectIndexes)
            {
                //var id = (Guid)gridView1.GetRowCellValue(index, conId);
                var dto = gridView1.GetRow(index) as CrisisCusInfoDto;
                foreach (var item in dto.重要异常明细)
                {
                    var id = item.Id;
                    UpCricalStateDto upCricalStateDto = new UpCricalStateDto();
                    upCricalStateDto.Id = id;
                    upCricalStateDto.Sate = (int)CrisisVisitSate.Examine;
                    criticalAppService.UpCrisisSate(upCricalStateDto);
                }
               
            }
            var message = "审核通过，“"+ selectIndexes.Length + "人”的体检项目中的“重要异常结果”为危急值。";
            XtraMessageBox.Show(this, message, "危急值审核", MessageBoxButtons.OK, MessageBoxIcon.Information);
            simpleButton1.PerformClick();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();

            foreach (var index in selectIndexes)
            {
                //var id = (Guid)gridView1.GetRowCellValue(index, conId);
                var dto = gridView1.GetRow(index) as CrisisCusInfoDto;
                foreach (var item in dto.重要异常明细)
                {
                    var id = item.Id;
                    UpCricalStateDto upCricalStateDto = new UpCricalStateDto();
                    upCricalStateDto.Id = id;
                    upCricalStateDto.Sate = (int)MessageState.HasMessage;
                    criticalAppService.UpMessSate(upCricalStateDto);
                }

            }
            var message = "已通知“"+ selectIndexes.Length + "人”本次体检中的“重要异常结果”";
            XtraMessageBox.Show(this, message, "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
            simpleButton1.PerformClick();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "危急值报告";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            gridControl1.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            
            #region 发送短信
            IClientRegAppService _clientRegAppService = new ClientRegAppService(); // 预约仓储
            var MessageModle = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ShortModle.ToString() && o.Value == 4)?.Remarks;
            if (string.IsNullOrEmpty(MessageModle))
            {
                XtraMessageBox.Show("请在字典设置“短信模板”，编码为4中设置危急值短信模板");
                return;
            }
            var rowList = gridView1.GetSelectedRows();
            foreach (var item in rowList)
            {
                if (gridView1.GetRow(item) is CrisisMessageDto row)
                {
                    if (!string.IsNullOrEmpty(row.Mobile))
                    {
                        ShortMessageDto input = new ShortMessageDto();
                        input.Age = row.Age;
                        //input.CustomerId = row.CustomerId;
                        input.CustomerRegId = row.cusRegId;
                        var shortMes = MessageModle.Replace("【姓名】", row.Name);
                        if (row.Sex == 1)
                        {
                            shortMes = shortMes.Replace("【性别】", "先生");
                        }
                        else if (row.Sex == 2)
                        {
                            shortMes = shortMes.Replace("【性别】", "女士");
                        }
                        shortMes = shortMes.Replace("【体检号】", row.CustomerBM);
                        shortMes = shortMes.Replace("【项目名称】", row.ItemName);
                        shortMes = shortMes.Replace("【项目结果】", row.ItemResultChar);
                        input.Message = shortMes;
                        input.CustomerBM = row.CustomerBM;
                        input.MessageType = 4;
                        input.Mobile = row.Mobile;
                        input.Name = row.Name;
                        input.SendState = 0;
                        input.Sex = row.Sex;
                        _clientRegAppService.SaveMessage(input);
                    }
                }
            }
        
            #endregion
        }
    }
}
