using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    public partial class SetMealChoice : UserBaseForm
    {
        private readonly IItemSuitAppService _itemSuitAppService; //套餐接口
        public List<SimpleItemSuitDto> Dto { get; set; }  //选择套餐Id
        public List<SimpleItemSuitDto> list { get; set; }  //选择套餐Id
        public SetMealChoice(List<SimpleItemSuitDto> dto)
        {
            InitializeComponent();
            _itemSuitAppService = new ItemSuitAppService();
            Dto = dto;
        }
        private void SetMealChoice_Load(object sender, EventArgs e)
        {
            gridView1.GroupPanelText = "已选套餐";
            gridView2.GroupPanelText = "可选套餐";
            //var list = _itemSuitAppService.QuerySimples(new SearchItemSuitDto { });
            var list = Common.UserCache.DefinedCacheHelper.GetItemSuit();
            grcTC.DataSource = list;
            this.list = list;
            searchLookUpEditTc.Properties.DataSource = list;
            if (Dto != null)
            {
                if (Dto.Count != 0)
                {
                    grcYX.DataSource = Dto;
                }
            }
        }
        private void sbSAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void sbsDel_Click(object sender, EventArgs e)
        {
            Del();
        }
        #region 左移右移
        public void Add()
        {
            var dtos = grcTC.GetSelectedRowDtos<SimpleItemSuitDto>();
            if (dtos.Count() == 0) return;
            grcYX.AddDtoListItem(dtos, true);

        }
        public void Del()
        {
            var dtos = grcYX.GetSelectedRowDtos<SimpleItemSuitDto>();
            if (dtos.Count() == 0) return;
            grcYX.RemoveDtoListItem(dtos);
        }
        #endregion

        private void sbconfirm_Click(object sender, EventArgs e)
        {
            var dto = grcYX.GetDtoListDataSource<SimpleItemSuitDto>().ToList();
            Dto = dto;
            DialogResult = DialogResult.OK;
        }

        private void sbCX_Click(object sender, EventArgs e)
        {
            if (searchLookUpEditTc.EditValue != null)
            {
                var list = _itemSuitAppService.QuerySimples(new SearchItemSuitDto { Id = Guid.Parse(searchLookUpEditTc.EditValue.ToString()) });
                grcTC.DataSource = list;
            }
            else
                grcTC.DataSource = list;
        }

        private void searchLookUpEditTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (searchLookUpEditTc.EditValue != null)
            {
                var list = _itemSuitAppService.QuerySimples(new SearchItemSuitDto { Id = Guid.Parse(searchLookUpEditTc.EditValue.ToString()) });
                grcTC.DataSource = list;
            }
        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    Add();
                }
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    Del();
                }
            }
        }

        private void searchLookUpEditTc_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            searchLookUpEditTc.EditValue = null;
        }
    }
}