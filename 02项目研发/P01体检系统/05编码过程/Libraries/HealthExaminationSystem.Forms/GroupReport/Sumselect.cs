using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.GroupReport
{
    public partial class Sumselect : UserBaseForm
    {
        public List<GroupSumBMDto> SelectedSumBMDtos = new List<GroupSumBMDto>();
        public List<GroupSumBMDto> AllSumBMDtos = new List<GroupSumBMDto>();
        public List<GroupSumBMDto> OutSelectedSumBMDtos = new List<GroupSumBMDto>();
        public Sumselect()
        {
            InitializeComponent();
        }

        private void Sumselect_Load(object sender, EventArgs e)
        {
            gcSelected.DataSource = AllSumBMDtos;

        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

            //var selectIndexes = gridView1.GetSelectedRows();
            //var dtos = gcSelected.GetSelectedRowDtos<GroupSumBMDto>();

            //if (dtos.Count == 0) return;
          //  gcWait.AddDtoListItem(dtos, true);
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            SelectedSumBMDtos = gcSelected.GetSelectedRowDtos<GroupSumBMDto>();
            //if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            //{
            //    var dtos = gcWait.GetSelectedRowDtos<GroupSumBMDto>();
            //    if (dtos.Count > 0)
            //    {
            //        var reout = gcWait.DataSource as List<GroupSumBMDto>;
            //        reout.Remove(dtos[0]);
            //    }
            //}
        }

       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SelectedSumBMDtos = gcSelected.GetSelectedRowDtos<GroupSumBMDto>();
            OutSelectedSumBMDtos = SelectedSumBMDtos;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OutSelectedSumBMDtos = null;
            this.Close();
        }

        private void gvItemInfoSelected_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
    }
}
