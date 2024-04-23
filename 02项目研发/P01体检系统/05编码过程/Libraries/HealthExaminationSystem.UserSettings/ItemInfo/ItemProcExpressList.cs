using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo
{
    public partial class ItemProcExpressList : UserBaseForm
    {
        public ItemProcExpressList()
        {
            _doctorStation = new DoctorStationAppService();
            InitializeComponent();
        }
        private readonly IDoctorStationAppService _doctorStation;
        private List<ItemProcExpressDto> _itemProcExpressDtos;
        ItemInfoAppService _itemInfoAppService = new ItemInfoAppService();
        private void ItemProcExpressList_Load(object sender, EventArgs e)
        {
            _itemProcExpressDtos = _doctorStation.getItemProcExpress();
            gridControlProcpress.DataSource = _itemProcExpressDtos;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
            searchLookItem.EditValue = null;

        }

        private void simpleButtonCreate_Click(object sender, EventArgs e)
        {
            frmProcExpress frmProcExpress = new frmProcExpress();
            if (frmProcExpress.ShowDialog() == DialogResult.OK)
            {
                simpleButtonQuery.PerformClick();
            }
        }

        private void simpleEditEdit_Click(object sender, EventArgs e)
        {
            var dto = gridControlProcpress.GetFocusedRowDto<ItemProcExpressDto>();
           
            if (dto == null)
            {
                ShowMessageBoxInformation("尚未选定数据！");
                return;
            }
            if (dto != null)

                using (var frm = new frmProcExpress(dto))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        simpleButtonQuery.PerformClick();
                    }


                }
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
           var  itemProcExpress = _doctorStation.getItemProcExpress();
            if (!string.IsNullOrEmpty(searchLookItem.Text))
            {
                var id = Guid.Parse(searchLookItem.EditValue.ToString());
                itemProcExpress = itemProcExpress.Where(o=>o.ItemId== id).ToList(); }
            gridControlProcpress.DataSource = itemProcExpress;
        }

        private void simpleDelete_Click(object sender, EventArgs e)
        {
            var id = gridViewProcpress.GetRowCellValue(gridViewProcpress.FocusedRowHandle, Id);
            if (id == null)
            {
                ShowMessageBoxError("尚未选定数据！");
                return;
            }
            var name = gridViewProcpress.GetRowCellValue(gridViewProcpress.FocusedRowHandle, Id);

            if (XtraMessageBox.Show($"确定要删除项目 {name} 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                try
                {
                    _itemInfoAppService.DelteItemExpress(new EntityDto<Guid>
                    {
                        Id = (Guid)id
                    });
                    simpleButtonQuery.PerformClick();
                }
                catch (UserFriendlyException exception)
                {
                    ShowMessageBoxError(exception.ToString());
                }
            }
        }

        private void gridViewProcpress_DoubleClick(object sender, EventArgs e)
        {
            simpleEditEdit_Click(sender, e);
        }
    }
}
