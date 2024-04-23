using Sw.Hospital.HealthExaminationSystem.ApiProxy.CarSend;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CardSend
{
    public partial class CardSeach : UserBaseForm
    {
        private readonly ICardSendAppService cardSendAppService;
        public CardSeach()
        {
            cardSendAppService = new CardSendAppService();
            InitializeComponent();
        }

        private void CardSeach_Load(object sender, EventArgs e)
        {
            //卡名称
            var cardTypelist = cardSendAppService.getSimpCardTypeList();
            comCardType.Properties.DataSource = cardTypelist;
            var userlist = DefinedCacheHelper.GetComboUsers();
            comCreatUser.Properties.DataSource = userlist;
            comSellUser.Properties.DataSource = userlist;
            var sellstate = SellTypeStateHelper.GetSellTypeStates();
            comSellType.Properties.DataSource = sellstate;

            var UsedState = UsedStateHelper.GetUsedStateList();
            comUserstate.Properties.DataSource = UsedState;

            var enableItems = InvoiceStateHelper.GetAllInvoiceStateModels();
            comActic.Properties.DataSource = enableItems;
            comActic.EditValue = (int)InvoiceState.Enable;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                InCardSearchDto searchCardDto = new InCardSearchDto();
                if (!string.IsNullOrEmpty(txtCarBM.Text))
                {
                    searchCardDto.CardNo = txtCarBM.Text;
                }
                if (comCardType.EditValue != null && comCardType.EditValue.ToString() != "")
                {
                    searchCardDto.CardTypeId = (Guid)comCardType.EditValue;
                }
                if (comCreatUser.EditValue != null && comCreatUser.EditValue.ToString() != "")
                {
                    searchCardDto.CreateCardUserId = (long)comCreatUser.EditValue;
                }
                if (comSellUser.EditValue != null && comSellUser.EditValue.ToString() != "")
                {
                    searchCardDto.SellCardUserId = (long)comSellUser.EditValue;
                }
                if (comSellType.EditValue != null && comSellType.EditValue.ToString() != "")
                {
                    searchCardDto.PayType = (int)comSellType.EditValue;
                }
                if (comUserstate.EditValue != null && comUserstate.EditValue.ToString() != "")
                {
                    searchCardDto.HasUse = (int)comUserstate.EditValue;
                }
                if (comActic.EditValue != null && comActic.EditValue.ToString() != "")
                {
                    searchCardDto.Available = (int)comActic.EditValue;
                }
                if (dtCstar.EditValue != null && dtCend.EditValue != null)
                {
                    searchCardDto.CstarTime =DateTime.Parse( dtCstar.DateTime.ToShortDateString());
                    searchCardDto.CendTime = DateTime.Parse(dtCend.DateTime.AddDays(1).ToShortDateString());
                }
                if (dtUstar.EditValue !=null && dtUend.EditValue !=null)
                {
                    searchCardDto.UsestarTime = DateTime.Parse(dtUstar.DateTime.ToShortDateString());
                    searchCardDto.UsesendTime = DateTime.Parse(dtUend.DateTime.AddDays(1).ToShortDateString());
                }

                var reseult = cardSendAppService.getSeachCardList(searchCardDto);
                gridControl1.DataSource = reseult;
            });
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Export();
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "体检卡";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gridControl1.ExportToXls(saveFileDialog.FileName);
        }
    }
}
