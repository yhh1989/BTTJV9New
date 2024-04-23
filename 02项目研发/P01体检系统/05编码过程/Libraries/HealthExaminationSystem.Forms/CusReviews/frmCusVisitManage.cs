using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.CusReviews
{
    public partial class frmCusVisitManage : UserBaseForm
    {
        public readonly IOccReviewAppService _IOccReviewAppService; 
       
        // 总检
        private readonly IInspectionTotalAppService _inspectionTotalService;

        private Guid cusRegId = new Guid();
        public frmCusVisitManage()
        {
            _IOccReviewAppService = new OccReviewAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            InitializeComponent();
        }

        private void frmCusVisitManage_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetSummarizeAdvices();
            lookUpEditVisitState.Properties.DataSource = VisiteStateHelper.GetSelectList();
            repositoryItemLookUpEdit2.DataSource = DefinedCacheHelper.GetComboUsers();
            repositoryItemLookUpEdit3.DataSource= DefinedCacheHelper.GetComboUsers();

            //checkRegDt.Checked = true;
            dateEditStart.EditValue = System.DateTime.Now;
            dateEditEnd.EditValue= System.DateTime.Now;

            gridViewCus.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCus.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
            gridViewCus.Columns[conVisitSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCus.Columns[conVisitSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(VisiteStateHelper.VisiteStateFormatter);

            gridViewCus.Columns[conVisitType.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCus.Columns[conVisitType.FieldName].DisplayFormat.Format =
                new CustomFormatter(VisitTypeHelper.VisitTypeFormatter);

            gridViewCus.Columns[conVisitMessageState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridViewCus.Columns[conVisitMessageState.FieldName].DisplayFormat.Format = new CustomFormatter(ShortMessageStateHelper.ShortMessageStateFormatter);
           var  sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
            gridLookUpSex.Properties.DataSource = sexList;//性别
        }

        private void butSeach_Click(object sender, EventArgs e)
        {
            searchVisitDto searchVisitDto = new searchVisitDto();
            if (!string.IsNullOrEmpty(textEditName.EditValue?.ToString()))
            {
                searchVisitDto.Name = textEditName.EditValue?.ToString();
            }
            if (!string.IsNullOrEmpty(textEditNum.EditValue?.ToString()))
            {
                if (comboBoxEdit1.Text.Contains("体检号"))
                {
                    searchVisitDto.ArchivesNum = textEditNum.EditValue?.ToString();
                }
                else if (comboBoxEdit1.Text.Contains("身份证号"))
                {
                    searchVisitDto.IDCardNo = textEditNum.EditValue?.ToString();
                }
                else if (comboBoxEdit1.Text.Contains("手机号"))
                {
                    searchVisitDto.Mobile = textEditNum.EditValue?.ToString();
                }
                
            }
            if (!string.IsNullOrEmpty(gridLookUpSex.EditValue?.ToString()))
            {
                searchVisitDto.Sex = int.Parse( gridLookUpSex.EditValue?.ToString());
            }
            if (checkRegDt.Checked == true && !string.IsNullOrEmpty(dateEditStart.EditValue?.ToString())
                 && !string.IsNullOrEmpty(dateEditEnd.EditValue?.ToString()))
            {
                searchVisitDto.LoginDateStar = dateEditStart.DateTime;
                searchVisitDto.LoginDateEnd = dateEditEnd.DateTime;
            }
            //复查
            if (checkEditReView.Checked == true && !string.IsNullOrEmpty(dateEditRStar.EditValue?.ToString())
                && !string.IsNullOrEmpty(dateEditREnd.EditValue?.ToString()))
            {
                searchVisitDto.ReDateStar = dateEditRStar.DateTime;
                searchVisitDto.ReDateEnd = dateEditREnd.DateTime;
            }
            //补检
            if (checkEditB.Checked == true && !string.IsNullOrEmpty(dateEditBStar.EditValue?.ToString())
                && !string.IsNullOrEmpty(dateEditBEnd.EditValue?.ToString()))
            {
                searchVisitDto.BDateStar = dateEditBStar.DateTime;
                searchVisitDto.BDateEnd = dateEditBEnd.DateTime;
            }

            if (CheckVisit.Checked == true && !string.IsNullOrEmpty(dateEditVisitStar.EditValue?.ToString())
               && !string.IsNullOrEmpty(dateEditVisitEnd.EditValue?.ToString()))
            {
                searchVisitDto.VisitTimeStart = dateEditVisitStar.DateTime;
                searchVisitDto.VisitTimeEnd = dateEditVisitEnd.DateTime;
            }
            searchVisitDto.MinAge =int.Parse(seO.EditValue?.ToString());
            searchVisitDto.MaxAge = int.Parse(seT.EditValue?.ToString());
            if (!string.IsNullOrEmpty(lookUpEditVisitState.EditValue?.ToString()) &&
                lookUpEditVisitState.EditValue?.ToString() !="3")
            {
                searchVisitDto.VisitSate = int.Parse(lookUpEditVisitState.EditValue?.ToString());
            }
            if (!string.IsNullOrEmpty(comVisituser.EditValue?.ToString()))
            {
                searchVisitDto.VisitEmployeeId = int.Parse(comVisituser.EditValue?.ToString());
            }
            List <VisiteCusRegDto> Outlist = _IOccReviewAppService.SearchCusRegVisit(searchVisitDto);
            gridControlCus.DataSource = Outlist;
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var scusRegId = gridViewCus.GetFocusedRowCellValue(conId) ;
            if (scusRegId != null)
            {
                richTextRemart.Text = "";
                richTextRemart.Tag = null;
                //复查信息
                cusRegId = Guid.Parse(scusRegId.ToString());
                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = cusRegId;
                var review = _inspectionTotalService.GetCusReViewDto(input);
                gridReView.DataSource = review;
                //补检信息
                var reGive = _inspectionTotalService.GetCusGiveDto(input);
                gridControlRevie.DataSource = reGive;
                //回访信息
                CusVisitDto cusVisitDto = new CusVisitDto();
                cusVisitDto.CustomerRegID = cusRegId;
               var cusVisitManage= _IOccReviewAppService.SearchCusVisitManage(cusVisitDto);
                gridControlVisite.DataSource = cusVisitManage;


            }
        }

        private void gridViewVisit_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dto= gridViewVisit.GetFocusedRow() as  SaveCusVisitManageDto;
            if (dto != null)
            {
                richTextRemart.Text = dto.remarks;
                richTextRemart.Tag = dto.Id.ToString();
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (cusRegId == null)
            {
                XtraMessageBox.Show("请选择需要随访的体检人！");
                return;
            }
            if (cusRegId == Guid.Empty)
            {
                XtraMessageBox.Show("请选择需要随访的体检人！");
                return;
            }
            SaveCusVisitManageDto input = new SaveCusVisitManageDto();
            if (!string.IsNullOrEmpty(richTextRemart.Tag?.ToString()))
            {
                input.Id =Guid.Parse( richTextRemart.Tag.ToString());
            }
            input.remarks = richTextRemart.Text;
            input.CustomerRegID = cusRegId;
            input.VisitState = 1;
            input.VisitType = radioGroup1.EditValue?.ToString();
            input.VisitEmployeeId = CurrentUser.Id;
            var newVisit = _IOccReviewAppService.SaveCusVisitManage(input);
            MessageBox.Show("保存成功！");
            //回访信息
            CusVisitDto cusVisitDto = new CusVisitDto();
            cusVisitDto.CustomerRegID = cusRegId;
            var cusVisitManage = _IOccReviewAppService.SearchCusVisitManage(cusVisitDto);
            gridControlVisite.DataSource = cusVisitManage;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            richTextRemart.Text ="";
            richTextRemart.Tag = null;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextRemart.Tag?.ToString()))
            {
                XtraMessageBox.Show("请选择需要删除的回访数据！");
                return;
            }
                DialogResult dr = XtraMessageBox.Show("是否删除该回访信息？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {

                SaveCusVisitManageDto input = new SaveCusVisitManageDto();
                if (!string.IsNullOrEmpty(richTextRemart.Tag?.ToString()))
                {
                    input.Id = Guid.Parse(richTextRemart.Tag.ToString());
                }
                _IOccReviewAppService.DelCusVisitManage(input);
                MessageBox.Show("删除成功！");

                //回访信息
                CusVisitDto cusVisitDto = new CusVisitDto();
                cusVisitDto.CustomerRegID = cusRegId;
                var cusVisitManage = _IOccReviewAppService.SearchCusVisitManage(cusVisitDto);
                gridControlVisite.DataSource = cusVisitManage;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            #region 发送短信
            IClientRegAppService _clientRegAppService = new ClientRegAppService(); // 预约仓储
            var MessageModle = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ShortModle.ToString() && o.Value == 3)?.Remarks;
            if (string.IsNullOrEmpty(MessageModle))
            {
                XtraMessageBox.Show("请在字典设置“短信模板”，编码为3中设置复查短信模板");
                return;
            }
            var MessageModlebj = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ShortModle.ToString() && o.Value == 5)?.Remarks;
            if (string.IsNullOrEmpty(MessageModlebj))
            {
                XtraMessageBox.Show("请在字典设置“短信模板”，编码为5中设置补检短信模板");
                return;
            }
            var rowList = gridViewCus.GetSelectedRows();
            if (rowList.Count() <= 0)
            {
                MessageBox.Show("请选中人员！");
                return;
            }
            int num = 0;
            foreach (var item in rowList)
            { 
                if (gridViewCus.GetRow(item) is VisiteCusRegDto row)
                {
                    if (!string.IsNullOrEmpty(row.Customer.Mobile))
                    {
                        ShortMessageDto input = new ShortMessageDto();
                        input.Age = row.Customer.Age;
                        //input.CustomerId = row.CustomerId;
                        input.CustomerRegId = row.Id;
                        var shortMes = MessageModle.Replace("【姓名】", row.Customer.Name);
                        if (row.Customer.Sex == 1)
                        {
                            shortMes = shortMes.Replace("【性别】", "先生");
                        }
                        else if (row.Customer.Sex == 2)
                        {
                            shortMes = shortMes.Replace("【性别】", "女士");
                        }
                        shortMes = shortMes.Replace("【体检号】", row.CustomerBM);

                       //复查信息                     
                        EntityDto<Guid> reinput = new EntityDto<Guid>();
                        reinput.Id = row.Id;
                        var review = _inspectionTotalService.GetCusReViewDto(reinput);
                        if (review.Count > 0)
                        {
                            var reName = review.Select(p => p.ItemGroupNames + ",复查日期:" + p.ReviewDate).ToList();
                            string strReName = string.Join(";", reName);
                            shortMes = shortMes.Replace("【复查信息】", strReName);

                            input.Message = shortMes;
                            input.CustomerBM = row.CustomerBM;
                            input.MessageType = 4;
                            input.Mobile = row.Customer.Mobile;
                            input.Name = row.Customer.Name;
                            input.SendState = 0;
                            input.Sex = row.Customer.Sex;
                            _clientRegAppService.SaveMessage(input);
                            num = num + 1;
                        }
                        var groupResult= gridControlVisite.DataSource as List<CusGiveUpShowDto>;
                        if (groupResult != null && groupResult.Count > 0)
                        {
                            //  //补检信息                     
                            var GroupIdlist = groupResult.Select(p => p.GroupName).ToList();
                            
                            var GroupName = string.Join(",", GroupIdlist);
                            shortMes = shortMes.Replace("【补检项目】", GroupName);
                            shortMes = shortMes.Replace("【补检日期】", groupResult.FirstOrDefault().stayDate.Value.ToString("yy年MM月dd日"));
                            input.Message = shortMes;
                            input.CustomerBM = row.CustomerBM;
                            input.MessageType = 5;
                            input.Mobile = row.Customer.Mobile;
                            input.Name = row.Customer.Name;
                            input.SendState = 0;
                            input.Sex = row.Customer.Sex;
                            _clientRegAppService.SaveMessage(input);
                            num = num + 1;
                        }
                    }
                }

            }
            if (num > 0)
            {
                MessageBox.Show("成功存入" + num +"条短信！");

            }
            #endregion
        }

        private void gridControlCus_Click(object sender, EventArgs e)
        {

        }
    }
}
