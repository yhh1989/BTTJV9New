using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo
{
    public partial class frmHCITems : UserBaseForm
    {
       public  List<ItemInfoSimpleDto> selectItems = new List<ItemInfoSimpleDto>();
        public frmHCITems()
        {
            InitializeComponent();
        }

        private void frmHCITems_Load(object sender, EventArgs e)
        {
            if (selectItems != null && selectItems.Count>0)
            {
                gcSelected.DataSource = selectItems;
            }
            LoadWaitData();

        }
        private void LoadWaitData(bool isReload = false)
        {

            var list = DefinedCacheHelper.GetItemInfos();           
            gcWait.DataSource = list;
        }

        private void sbAddSingle_Click(object sender, EventArgs e)
        {
            Add();
        }
        public void Add()
        {         
            var dtos = gcWait.GetSelectedRowDtos<ItemInfoSimpleDto>();
            if (dtos.Count == 0) return;
            gcSelected.AddDtoListItem(dtos, true);
        }
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<ItemInfoSimpleDto>();
            if (dtos.Count == 0) return;
            gcSelected.RemoveDtoListItem(dtos);
        }

        private void gvItemInfoWait_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Add();
            }
        }

        private void gvItemInfoSelected_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Del();
            }
        }

        private void sbDelSingle_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            selectItems = (List<ItemInfoSimpleDto>)gcSelected.DataSource;
        }
    }
}
