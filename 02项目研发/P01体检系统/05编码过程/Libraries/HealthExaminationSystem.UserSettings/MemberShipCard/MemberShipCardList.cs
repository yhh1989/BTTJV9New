using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.MemberShipCard
{
    public partial class MemberShipCardList : UserBaseForm
    {
        private readonly IMemberShipCardAppService _MemberShipCardAppService;

        public MemberShipCardList()
        {
            InitializeComponent();
            _MemberShipCardAppService = new MemberShipCardAppService();
        }

        private void MemberShipCardList_Load(object sender, EventArgs e)
        {           
            SearchTbmCardTypeDto show = new SearchTbmCardTypeDto();
            var data = _MemberShipCardAppService.GetTbmCardTypes(show);
            gridControl1.DataSource = data;
            gridView1.Columns[gridColumn4.FieldName].DisplayFormat.FormatType = FormatType.Custom;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (var frm = new MemberShipCardEdit())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    simpleButton1.PerformClick();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchTbmCardTypeDto show = new SearchTbmCardTypeDto();
            if (!string.IsNullOrWhiteSpace(Convert.ToString(textEdit1.EditValue)))
            {
                show.CardNum = Convert.ToInt32(textEdit1.Text);
            }
            if (!string.IsNullOrWhiteSpace(textEdit2.Text))
            {
                show.CardName = textEdit2.Text;
            }
            if (dateEditStartTime.EditValue != null)
                show.StartCheckDate = dateEditStartTime.DateTime.Date;
            
            if (dateEditEndTime.EditValue != null)
                show.EndCheckDate = Convert.ToDateTime(dateEditEndTime.DateTime.ToString("yyyy-MM-dd") + " 23:59:59 ");
            if (show.StartCheckDate > show.EndCheckDate)
            {
                ShowMessageBoxError("开始时间大于结束时间，请重新选择时间。");
                return;
            }
            //if (show.StartCheckDate > show.EndCheckDate)
            //{
            //    dxErrorProvider.SetError(dateEditStartTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
            //    dxErrorProvider.SetError(dateEditEndTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
            //    return;
            //}
            var data = _MemberShipCardAppService.GetTbmCardTypes(show);
            gridControl1.DataSource = data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn7);
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
                    var dto = gridControl1.GetFocusedRowDto<ShowTbmCardTypeDto>();
                    AutoLoading(() =>
                    {
                        _MemberShipCardAppService.Del(new EntityDto<Guid>
                        {
                            Id = (Guid)id
                        });
                        gridControl1.RemoveDtoListItem(dto);
                    }, Variables.LoadingDelete);
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBoxError(exception.ToString());
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn7);
            //var old = gridView1.GetFocusedRow() as TbmCardTypeDto;
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
                using (var frm = new MemberShipCardEdit((Guid)id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ModelHelper.CustomMapTo(frm._Model);
                        simpleButton1.PerformClick();
                        gridControl1.RefreshDataSource();

                    }
                }
        }
    }
}
