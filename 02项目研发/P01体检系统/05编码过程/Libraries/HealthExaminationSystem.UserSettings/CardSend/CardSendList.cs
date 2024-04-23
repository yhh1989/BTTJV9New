using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CarSend;
using Sw.Hospital.HealthExaminationSystem.Application.CarSend.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
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
using static Sw.Hospital.HealthExaminationSystem.UserSettings.CardSend.CardSendEdit;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CardSend
{
    public partial class CardSendList : UserBaseForm
    {
        public readonly CardSendAppService cardSendAppService;
        public CardSendList()
        {
            cardSendAppService = new CardSendAppService();
            InitializeComponent();
        }

        private void CardSendList_Load(object sender, EventArgs e)
        {
            //卡名称
            var cardTypelist = cardSendAppService.getSimpCardTypeList();
            comCardType.Properties.DataSource = cardTypelist;
            var userlist = DefinedCacheHelper.GetComboUsers();
            comCreatUser.Properties.DataSource = userlist;
            comSellUser.Properties.DataSource = userlist;
            var sellstate=  SellTypeStateHelper.GetSellTypeStates();
            comSellType.Properties.DataSource = sellstate;
            comSellType.EditValue = 3;

            SearchCardDto searchCardDto = new SearchCardDto();
            searchCardDto.SendType = 3;
            var reseult = cardSendAppService.getSimpCardList(searchCardDto);
            gridControl1.DataSource = reseult;
            txtClientRegID.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                SearchCardDto searchCardDto = new SearchCardDto();
                if (!string.IsNullOrEmpty(txtCarBM.Text))
                {
                    searchCardDto.CardNo = txtCarBM.Text;
                }
                if (comCardType.EditValue != null && comCardType.EditValue.ToString() != "")
                {
                    searchCardDto.CardType = (Guid)comCardType.EditValue;
                }
                if (comCreatUser.EditValue != null && comCreatUser.EditValue.ToString() != "")
                {
                    searchCardDto.CreatUser = (long)comCreatUser.EditValue;
                }
                if (comSellUser.EditValue != null && comSellUser.EditValue.ToString() != "")
                {
                    searchCardDto.SendUser = (long)comSellUser.EditValue;
                }
                if (comSellType.EditValue != null && comSellType.EditValue.ToString() != "")
                {
                    searchCardDto.SendType = (int)comSellType.EditValue;
                }
                if (!string.IsNullOrEmpty(comboType.Text) && !comboType.Text.Contains("全部"))
                {
                    searchCardDto.CardCategory = comboType.Text;
                }
                if (!string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString()))
                {
                    searchCardDto.ClientRegId = (Guid)txtClientRegID.EditValue;
                }
                var reseult = cardSendAppService.getSimpCardList(searchCardDto);
                
                    gridControl1.DataSource = reseult;
            });
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("确定需要注销么？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                var selectIndexes = gridView1.GetSelectedRows();
                var result = gridControl1.DataSource as List<SimpCardListDto>;
                if (selectIndexes.Length != 0)
                {
                    foreach (var index in selectIndexes)
                    {
                        var id = (Guid)gridView1.GetRowCellValue(index, conId);
                        EntityDto<Guid> input = new EntityDto<Guid>();
                        input.Id = id;
                        cardSendAppService.OffCard(input);
                    }
                    MessageBox.Show("注销成功！");
                    simpleButton1.PerformClick();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CardSendEdit frm = new CardSendEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                simpleButton1.PerformClick();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            var result= gridControl1.DataSource as List<SimpCardListDto>;
            if (selectIndexes.Length != 0)
            {
                foreach (var index in selectIndexes)
                {
                    var id = (Guid)gridView1.GetRowCellValue(index, conId);
                    var currre = result.FirstOrDefault(o=>o.Id== id);
                    printList(currre);
                }
            }
        }
        private void printList(SimpCardListDto saveTbmCardDto)
        {
            var reportJson = new ReportJson();
            reportJson.Detail = new List<Master>();
            var master = new Master();
            master.CardNo = saveTbmCardDto.CardNo;
            master.CardTypeName = saveTbmCardDto.CardTypeName;
            master.CreateTime = saveTbmCardDto.CreateTime;
            master.EndTime = saveTbmCardDto.EndTime;
            master.FaceValue = saveTbmCardDto.FaceValue;

            reportJson.Detail.Add(master);
            var gridppUrl = GridppHelper.GetTemplate("体检卡.grf");
            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);
            var StrPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 100)?.Remarks;
            if (!string.IsNullOrEmpty(StrPrintName))
            {
                //report.Print.StrPrintName = StrPrintName;
                report.Printer.PrinterName = StrPrintName;
            }
            report.Print(false);
        }
    }
}
