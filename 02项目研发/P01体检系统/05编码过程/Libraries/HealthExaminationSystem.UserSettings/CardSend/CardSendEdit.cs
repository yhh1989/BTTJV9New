using DevExpress.XtraLayout.Utils;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CarSend;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
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

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CardSend
{
    public partial class CardSendEdit : UserBaseForm
    {
        private Guid cardId;
        public readonly CardSendAppService cardSendAppService;
        List<SimpCardTypeDto> simpCardTypeDtos = new List<SimpCardTypeDto>();
        private ICustomerAppService customerSvr;//体检预约
        public CardSendEdit()
        {
            cardSendAppService = new CardSendAppService();
            InitializeComponent();
        }
        public CardSendEdit(Guid _CardId) : this()
        {
           
            cardId = _CardId;
        }

        private void CardSendEdit_Load(object sender, EventArgs e)
        {
            customerSvr = new CustomerAppService();
            //卡名称
            simpCardTypeDtos = cardSendAppService.getSimpCardTypeList();
            comCardType.Properties.DataSource = simpCardTypeDtos;
            var userlist = DefinedCacheHelper.GetComboUsers();

            CreateUser.Properties.DataSource = userlist;
            SellUser.Properties.DataSource = userlist;
            CreateUser.EditValue = CurrentUser.Id;
            SellUser.EditValue= CurrentUser.Id;
            layoutControlItem2.Visibility = LayoutVisibility.Never;
            layoutControlItem13.Visibility = LayoutVisibility.Never;

            txtClientRegID.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            comboBoxEdit1.Text = "套餐卡";
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ee = radioGroup2.EditValue;
            if (ee.ToString() =="1")
            {
                layoutControlItem2.Visibility = LayoutVisibility.Never;
                layoutControlItem13.Visibility = LayoutVisibility.Never;


            }
            else if (ee.ToString() == "2")
            {
                layoutControlItem2.Visibility = LayoutVisibility.Always;
                layoutControlItem13.Visibility = LayoutVisibility.Always;
            }
        }
        private void getEndTime()
        {
            if (int.TryParse(txtNum.Text, out int Num) && int.TryParse(txtCardNo.Text, out int star) && Num>0)
            {
                txtCardEnd.Text = (star + Num-1).ToString();
            }
        }

        private void txtNum_Leave(object sender, EventArgs e)
        {
            getEndTime();
        }

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            getEndTime();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ess = radioGroup1.EditValue;
            if (ess.ToString()=="1")
            {
                layoutControlItem14.Visibility = LayoutVisibility.Always;


            }
            else if (ess.ToString() == "2")
            {
                layoutControlItem14.Visibility = LayoutVisibility.Never;
                
               
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                if (string.IsNullOrWhiteSpace(txtCardNo.Text))
                {
                    dxErrorProvider.SetError(txtCardNo, string.Format(Variables.MandatoryTips, "体检卡号"));
                    txtCardNo.Focus();
                    return;
                }
                if (comboBoxEdit1.Text == "套餐卡" && string.IsNullOrWhiteSpace(comCardType.Text))
                {
                    dxErrorProvider.SetError(comCardType, string.Format(Variables.MandatoryTips, "体检卡名"));
                    comCardType.Focus();
                    return;
                }
                if (comboBoxEdit1.Text.Contains("单位") 
                && string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString() ))
                {
                    dxErrorProvider.SetError(txtClientRegID, string.Format(Variables.MandatoryTips, "单位名称"));
                    comCardType.Focus();
                    return;
                }
                if (comboBoxEdit1.Text.Contains("单位")
              && string.IsNullOrEmpty(txtTeamID.EditValue?.ToString()))
                {
                    dxErrorProvider.SetError(txtTeamID, string.Format(Variables.MandatoryTips, "单位分组"));
                    comCardType.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(CreateUser.Text))
                {
                    dxErrorProvider.SetError(CreateUser, string.Format(Variables.MandatoryTips, "开卡人"));
                    CreateUser.Focus();
                    return;
                }            if (string.IsNullOrWhiteSpace(SellUser.Text))
                {
                    dxErrorProvider.SetError(SellUser, string.Format(Variables.MandatoryTips, "销售员"));
                    SellUser.Focus();
                    return;
                }
            if (radioGroup1.EditValue.Equals(1) && payMoney.EditValue == null)
            {
                dxErrorProvider.SetError(payMoney, string.Format(Variables.MandatoryTips, "结算金额"));
                payMoney.Focus();
                return;

            }
            var ess = radioGroup2.EditValue;
            var HasCard = "";
            if (ess.ToString()=="1")
            {
                SearchCardDto searchCardDto = new SearchCardDto();
                searchCardDto.CardNo = txtCardNo.Text;
               var old= cardSendAppService.getSimpCardList(searchCardDto);
                if (old.Count > 0)
                {
                    HasCard += "卡号：" + txtCardNo.Text + "已存在，发卡失败！";
                    MessageBox.Show(HasCard);
                    return;
                }
                SaveCard(txtCardNo.Text);
                MessageBox.Show("发卡成功！");
                this.DialogResult = DialogResult.OK;
            }
            else if (ess.ToString() == "2")
            {

                    if (!int.TryParse(txtCardNo.Text, out int star))
                    {
                        MessageBox.Show("卡号必须为数值！才能批量发卡");
                        return;
                    }
                    if (int.Parse(txtNum.Text)<=0)
                    {
                        MessageBox.Show("卡数量为小于1，没有可发的卡");
                        return;
                    }
                    if (txtNum.Text == "1")
                    {
                        txtCardEnd.Text = txtCardNo.Text;
                    }
                var end = int.Parse(txtCardEnd.Text);
                int cardcount = 0;
                int cardErr = 0;
                for (int num= star; num <= end; num++)
                {
                    SearchCardDto searchCardDto = new SearchCardDto();
                    searchCardDto.CardNo = num.ToString();
                    var old = cardSendAppService.getSimpCardList(searchCardDto);
                    if (old.Count > 0)
                    {
                        HasCard += "卡号：" + num + "已存在,发卡失败！";
                        cardErr = cardErr + 1;
                        continue;
                    }
                    SaveCard(num.ToString());
                    cardcount = cardcount + 1;
                }
                if (HasCard != "")
                {
                    MessageBox.Show("成功发卡：" + cardcount + "条,失败发卡：" + cardErr + "条。" + HasCard + "");
                }
                else
                {
                    MessageBox.Show("成功发卡：" + cardcount + "条");
                    this.DialogResult = DialogResult.OK;
                }
            }

            });
        }
        private bool SaveCard(string CardNum)
        {
            SaveTbmCardDto saveTbmCardDto = new SaveTbmCardDto();
            saveTbmCardDto.CardNo = CardNum;
            if (!string.IsNullOrEmpty(comCardType.EditValue?.ToString()))
            {
                saveTbmCardDto.CardTypeId = (Guid)comCardType.EditValue;
            }
            saveTbmCardDto.Available = 1;
            saveTbmCardDto.CreateCardUserId = (long)CreateUser.EditValue;
            if (dtEnd.EditValue != null)
            {
                saveTbmCardDto.EndTime = dtEnd.DateTime;
            }
           
            saveTbmCardDto.FaceValue = decimal.Parse(txtfaceMoney.EditValue.ToString()); 
            saveTbmCardDto.PayType =int.Parse( radioGroup1.EditValue.ToString());
            if (saveTbmCardDto.PayType == 1)
            {
                saveTbmCardDto.SellMoney = decimal.Parse(payMoney.EditValue.ToString());
            }
            else
            {
                saveTbmCardDto.SellMoney = saveTbmCardDto.FaceValue;
            }
            saveTbmCardDto.SellCardUserId= (long)SellUser.EditValue;
            saveTbmCardDto.CardCategory = comboBoxEdit1.Text;
            if (!string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString()))
            {
                saveTbmCardDto.ClientRegId = (Guid)txtClientRegID.EditValue;
            }
            if (!string.IsNullOrEmpty(txtTeamID.EditValue?.ToString()))
            {
                saveTbmCardDto.ClientTeamInfoId = (Guid)txtTeamID.EditValue;
            }
            if (!string.IsNullOrEmpty(txtDiscount.EditValue?.ToString()))
            {
                saveTbmCardDto.DiscountRate = decimal.Parse(txtDiscount.EditValue.ToString());
            }
            var CardDto= cardSendAppService.SaveTbmCard(saveTbmCardDto);
            cardId = CardDto.Id;
            if (checkEdit1.Checked==true)
            {
                printList(saveTbmCardDto);
            }
            return true;
        }

        private void comCardType_EditValueChanged(object sender, EventArgs e)
        {
            if (comCardType.EditValue != null)
            {
             var cardtype  = simpCardTypeDtos.FirstOrDefault(o => o.Id == Guid.Parse(comCardType.EditValue.ToString()));
                txtfaceMoney.EditValue = cardtype.FaceValue;
                if (cardtype.Term == 1)
                {
                    dtEnd.Enabled = true;
                    dtEnd.EditValue = System.DateTime.Now.AddMonths(cardtype.Months.Value).ToShortDateString();
                }
                else
                {
                    dtEnd.Enabled = false;
                    dtEnd.EditValue = null;
                }
            }
        }
        private void printList(SaveTbmCardDto saveTbmCardDto)
        {
            var reportJson = new ReportJson();
            reportJson.Detail = new List<Master>();
            var master = new Master();
            master.CardNo = saveTbmCardDto.CardNo;
            master.CardTypeName = comCardType.Text;
            master.CreateTime = System.DateTime.Now;
            master.EndTime = saveTbmCardDto.EndTime;
            master.FaceValue = saveTbmCardDto.FaceValue;
           
            reportJson.Detail.Add(master);
            var gridppUrl = GridppHelper.GetTemplate("体检卡.grf");
            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);
            report.Print(false);
        }

        /// <summary>
        /// 报表
        /// </summary>
        public class ReportJson
        {
            /// <summary>
            /// 参数
            /// </summary>
            public List<Master> Detail { get; set; }

         
        }

        /// <summary>
        /// 报表参数
        /// </summary>
        public class Master
        {
            /// <summary>
            /// 卡号
            /// </summary>
            public string CardNo { get; set; }
            /// <summary>
            /// 卡名称
            /// </summary>
            public string CardTypeName { get; set; }

            /// <summary>
            /// 结束日期
            /// </summary>
            public DateTime? EndTime { get; set; }

            /// <summary>
            /// 面值
            /// </summary>
            public decimal FaceValue { get; set; }
            /// <summary>
            /// 开卡时间
            /// </summary>
            public DateTime? CreateTime { get; set; }   

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClientRegID_EditValueChanged(object sender, EventArgs e)
        {
            //显示单位预约信息
            var clietreg = txtClientRegID.GetSelectedDataRow() as ClientRegNameComDto;
         
            var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = Guid.Parse(txtClientRegID.EditValue?.ToString()) });
            
            
            txtTeamID.EditValue = null;
            txtTeamID.Properties.DataSource = list;
       
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "套餐卡")
            {
                layTC.Visibility = LayoutVisibility.Always;
                layTC1.Visibility = LayoutVisibility.Always;
                labClientRegID.Visibility = LayoutVisibility.Never;
                labTeamID.Visibility= LayoutVisibility.Never;
            }
            else  
            {
                layTC.Visibility = LayoutVisibility.Never;
                layTC1.Visibility = LayoutVisibility.Never;
                labClientRegID.Visibility = LayoutVisibility.Always;
                labTeamID.Visibility = LayoutVisibility.Always;
            }
           

        }
    }
}
