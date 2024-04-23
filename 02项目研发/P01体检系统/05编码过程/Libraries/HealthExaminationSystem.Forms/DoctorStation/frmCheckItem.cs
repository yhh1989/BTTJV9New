using Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmCheckItem : UserBaseForm
    {
        public List<GroupCusGroupDto> cusGroupDtos = new List<GroupCusGroupDto>();
        public List<GroupCusGroupDto> cusGroupOut = new List<GroupCusGroupDto>();
        public CommonAppService CommonAppSrv =new CommonAppService();
        public string groups = "";
        public string isadd = "";
        private readonly CrisisAppService crisisAppService = new CrisisAppService();
        public CusReViewDto reviewSetOut = new CusReViewDto();
        public frmCheckItem()
        {
            InitializeComponent();
        }

        private void frmCheckItem_Load(object sender, EventArgs e)
        {
            var allGroups= DefinedCacheHelper.GetItemGroups();
            gcWait.DataSource = allGroups.ToList();
            if (isadd == "1")
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtIllName.Visible = true;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtReviewTime.Visible = true;
                var recvir = crisisAppService.getAllReview();
                txtIllName.Properties.DataSource = recvir;
            }
            else
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtIllName.Visible = false;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtReviewTime.Visible = false;
                List<Guid> itemlis = new List<Guid>();
                foreach (var groupid in cusGroupDtos)
                {
                    itemlis.Add(groupid.Id);
                }
                if (itemlis.Count > 0)
                {
                    var groupsckd = allGroups.Where(o => itemlis.Contains(o.Id)).ToList();
                    gcSelected.DataSource = groupsckd;
                }
            }
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        public void Add()
        {
            var dtos = gcWait.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (dtos.Count == 0) return;
            var itemSuitItemGroups = dtos;

            var list = gcSelected.GetDtoListDataSource<SimpleItemGroupDto>();
            if (list == null)
            {
                list = new List<SimpleItemGroupDto>();
                gcSelected.DataSource = list;
            }
            itemSuitItemGroups.RemoveAll(m => list.Any(s => s.Id == m.Id));
            list.AddRange(itemSuitItemGroups);
            gcSelected.RefreshDataSource();
            gcWait.GetDtoListDataSource<SimpleItemGroupDto>()?.RemoveAll(m => dtos.Any(i => i.Id == m.Id));
            gcWait.RefreshDataSource();
        }
        /// <summary>
        /// 移除选择
        /// </summary>
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (dtos.Count == 0) return;
            //gcSelected.RemoveDtoListItem(dtos);
            gcSelected.GetDtoListDataSource<SimpleItemGroupDto>()?.RemoveAll(m => dtos.Any(i => i.Id == m.Id));
            gcSelected.RefreshDataSource();

            var itemSuitItemGroups = dtos;

            var list = gcWait.GetDtoListDataSource<SimpleItemGroupDto>();
            if (list == null)
            {
                list = new List<SimpleItemGroupDto>();
                gcWait.DataSource = list;
            }
            itemSuitItemGroups.RemoveAll(m => list.Any(s => s.Id == m.Id));
            list.AddRange(itemSuitItemGroups);
            gcWait.RefreshDataSource();
        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Add();
        }

        private void gcSelected_DoubleClick(object sender, EventArgs e)
        {
            Del();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // 组合列表
            var ItemSuitItemGroups = gcSelected.GetDtoListDataSource<SimpleItemGroupDto>();
            if (gridView1.RowCount == 0)
            {
                ShowMessageBoxWarning("未选择项目组合");
            }          
            
            foreach (var group in ItemSuitItemGroups)
            {
                GroupCusGroupDto groupCusGroupDto = new GroupCusGroupDto();
                groupCusGroupDto.Id = group.Id;
                groupCusGroupDto.ItemGroupName = group.ItemGroupName;
                cusGroupOut.Add(groupCusGroupDto);           
            }
            if (isadd == "1" )
            {
               
                if (txtIllName.EditValue != null)
                {
                    var review = txtIllName.GetSelectedDataRow() as ReviewSetDto;
                    reviewSetOut.Remart = review.Remark; reviewSetOut.SummarizeAdviceId = Guid.Parse(txtIllName.EditValue.ToString());
                }                
                reviewSetOut.ItemGroup = cusGroupOut;
                reviewSetOut.ReviewDay = int.Parse(txtReviewTime.Text);
                reviewSetOut.ReviewDate = CommonAppSrv.GetDateTimeNow().Now.Date.AddDays(int.Parse(txtReviewTime.Text));
            }
            DialogResult = DialogResult.OK;
        }

        private void txtIllName_EditValueChanged(object sender, EventArgs e)
        {
            if (txtIllName.EditValue != null)
            {
                var review = txtIllName.GetSelectedDataRow() as   ReviewSetDto;
                txtReviewTime.Text = review.Checkday.ToString();             
                gcSelected.DataSource = review.ItemGroupBM;
               
            }

        }
    }
}
