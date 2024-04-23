using Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.OAApproval
{
    public partial class ZKApproval : UserBaseForm
    {
        private CreateClientXKSetDto createClientXKSetDto = new CreateClientXKSetDto();
        private IOAApprovalAppService oAApprovalAppService = new OAApprovalAppService();
        private readonly IClientInfoesAppService _ClientInfoesAppService;
        public ZKApproval()
        {
            _ClientInfoesAppService = new ClientInfoesAppService();
            InitializeComponent();
        }

        private void ZKApproval_Load(object sender, EventArgs e)
        {
            ChargeBM chargeBM = new ChargeBM();

            var clientPaaent = _ClientInfoesAppService.QueryClientName(chargeBM);
            createClientXKSetDto = oAApprovalAppService.getCreateClientXKSet();
            if (createClientXKSetDto == null)
            {
                MessageBox.Show("请设置单位折扣设置！");
            }
            txtClientRegID.Properties.DataSource = clientPaaent.ToList();
            repositoryItemLookUpEdit1.DataSource = clientPaaent.ToList();
            repositoryItemLookUpEdit2.DataSource = DefinedCacheHelper.GetComboUsers().ToList();
            repositoryItemLookUpEdit3.DataSource = DefinedCacheHelper.GetComboUsers().ToList();
            repositoryItemLookUpEdit4.DataSource= OAApStateHelper.getOAApState().ToList();
            repositoryItemLookUpEdit5.DataSource= DefinedCacheHelper.GetComboUsers().ToList();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AddZKApproval addZKApproval = new AddZKApproval();
            addZKApproval.ShowDialog();
            simpleButton1.PerformClick();

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var id = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, conId);
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
            {
                SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
                searchOAApproValcsDto.Id = (Guid)id;
                var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
                if (zk.Count > 0)
                {
                    if (zk[0].CreatorUserId != CurrentUser.Id)
                    {
                        MessageBox.Show("不能修改其他用户发起的审批！");
                        return;
                    }
                    
                }
                if (zk[0].AppliState != (int)OAApState.NoAp && (zk[0].DiscountRate < createClientXKSetDto.DiscountRate 
                    || zk[0].AddDiscountRate < createClientXKSetDto.DiscountRate))
                {
                    MessageBox.Show("未审核记录才能修改！");
                    return;
                }

                AddZKApproval addZKApproval = new AddZKApproval((Guid)id);
                addZKApproval.ShowDialog();
                var dto = gridControl1.GetFocusedRowDto<CreatOAApproValcsDto>();
                ModelHelper.CustomMapTo(addZKApproval.creatOAApproValcsDto, dto);
                gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
            if (txtClientRegID.EditValue != null)
            {
                searchOAApproValcsDto.ClientInfoId = (Guid)txtClientRegID.EditValue;

            }
            if (dtCStar.EditValue != null)
            {
                searchOAApproValcsDto.StarCreateTime =DateTime.Parse( dtCStar.DateTime.ToShortDateString());
            }
            if (dtCEnd.EditValue != null)
            {
                searchOAApproValcsDto.EndCreateTime = DateTime.Parse(dtCEnd.DateTime.AddDays(1).ToShortDateString());
            }
            if (dtAStar.EditValue != null)
            {
                searchOAApproValcsDto.StarAppliTime = DateTime.Parse(dtAStar.DateTime.ToShortDateString());
            }
            if (dtAEnd.EditValue != null)
            {
                searchOAApproValcsDto.EndAppliTime = DateTime.Parse(dtAEnd.DateTime.AddDays(1).ToShortDateString());
            }
            if (lookAppState.EditValue != null)
            {
                searchOAApproValcsDto.AppliState = (int)lookAppState.EditValue;
            }

            var applist = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
            if (checkEdit1.Checked == true)
            {
                applist = applist.Where(o=>o.CreatorUserId==CurrentUser.Id || o.ApprovalUserId==CurrentUser.Id).ToList();
            }
            gridControl1.DataSource = applist;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var id = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, conId);
            if (id == null)
            {
                ShowMessageBoxInformation("尚未选定任何项目！");
                return;
            }
            if (id != null)
            {
                SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
                searchOAApproValcsDto.Id = (Guid)id;
                var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
                if (zk.Count > 0)
                {
                    if (zk[0].ApprovalUserId != CurrentUser.Id)
                    {
                        MessageBox.Show("不能修改他人的审批！");
                        return;
                    }
                }
                SPApproval sPApproval = new SPApproval((Guid)id);
                sPApproval.ShowDialog();
                var dto = gridControl1.GetFocusedRowDto<CreatOAApproValcsDto>();
                ModelHelper.CustomMapTo(sPApproval.creatOAApproValcsDto, dto);
                gridControl1.RefreshDataSource();
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var selectRow = gridView2.GetSelectedRows()[0];
            var id = gridView2.GetRowCellValue(selectRow, "Id").ToString();
            if (id != null)
            {
               
                SPApproval sPApproval = new SPApproval(Guid.Parse(id),false);
                sPApproval.ShowDialog();
            }
        }
    }
}
